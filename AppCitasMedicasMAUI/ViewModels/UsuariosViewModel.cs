using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppCitasMedicasMAUI.ViewModels
{
    public class UsuariosViewModel : BaseViewModel
    {
        private readonly UsuarioApiService _usuarioService;
        private readonly LogService _logService;

        public ObservableCollection<Usuario> Usuarios { get; } = new();

        public ICommand CargarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand CrearUsuarioCommand { get; }
        public ICommand EditarCommand { get; }

        public UsuariosViewModel(UsuarioApiService usuarioService, LogService logService)
        {
            _usuarioService = usuarioService;
            _logService = logService;

            CargarCommand = new Command(async () => await CargarUsuariosAsync());
            EliminarCommand = new Command<Usuario>(async (usuario) => await EliminarUsuarioAsync(usuario));
            CrearUsuarioCommand = new Command(async () => await IrACrearUsuarioAsync());
            EditarCommand = new Command<Usuario>(async (usuario) => await IrAEditarUsuarioAsync(usuario));
        }

        public async Task CargarUsuariosAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                Usuarios.Clear();
                var lista = await _usuarioService.GetAllAsync();
                foreach (var usuario in lista)
                    Usuarios.Add(usuario);
            }
            catch (Exception ex)
            {
                MensajeError = $"Error al cargar usuarios: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task EliminarUsuarioAsync(Usuario usuario)
        {
            if (usuario == null) return;

            bool confirmado = await Shell.Current.DisplayAlert(
                "Confirmar", $"¿Eliminar usuario {usuario.Correo}?", "Sí", "No");

            if (!confirmado) return;

            bool resultado = await _usuarioService.DeleteAsync(usuario.UsuarioId);

            if (resultado)
            {
                Usuarios.Remove(usuario);
                await _logService.RegistrarAccionAsync($"Eliminó usuario: {usuario.Correo}");
                await Shell.Current.DisplayAlert("Éxito", "Usuario eliminado.", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo eliminar el usuario.", "OK");
            }
        }

        private async Task IrACrearUsuarioAsync()
        {
            await Shell.Current.GoToAsync("//CrearUsuarioPage");
        }

        private async Task IrAEditarUsuarioAsync(Usuario usuario)
        {
            if (usuario == null) return;

            var navParam = new Dictionary<string, object>
            {
                { "Usuario", usuario }
            };

            await Shell.Current.GoToAsync("//EditarUsuarioPage", navParam);
        }
    }
}
