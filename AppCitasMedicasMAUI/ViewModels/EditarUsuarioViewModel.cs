using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using System.Windows.Input;

namespace AppCitasMedicasMAUI.ViewModels
{
    public class EditarUsuarioViewModel : BaseViewModel
    {
        private readonly UsuarioApiService _usuarioService;
        private readonly LogService _logService;

        private Usuario _usuarioOriginal;

        public ICommand GuardarCommand { get; }
        public ICommand CancelarCommand { get; }

        public EditarUsuarioViewModel(UsuarioApiService usuarioService, LogService logService)
        {
            _usuarioService = usuarioService;
            _logService = logService;

            GuardarCommand = new Command(async () => await GuardarCambiosAsync());
            CancelarCommand = new Command(async () => await Shell.Current.GoToAsync("//UsuariosPage"));
        }

        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public RolUsuario RolSeleccionado { get; set; }

        public List<RolUsuario> Roles => Enum.GetValues(typeof(RolUsuario)).Cast<RolUsuario>().ToList();

        public void Inicializar(Usuario usuario)
        {
            _usuarioOriginal = usuario;

            Correo = usuario.Correo;
            Contrasena = usuario.Contrasena;
            RolSeleccionado = usuario.Rol;

            OnPropertyChanged(nameof(Correo));
            OnPropertyChanged(nameof(Contrasena));
            OnPropertyChanged(nameof(RolSeleccionado));
        }

        private async Task GuardarCambiosAsync()
        {
            if (_usuarioOriginal == null) return;

            _usuarioOriginal.Correo = Correo;
            _usuarioOriginal.Contrasena = Contrasena;
            _usuarioOriginal.Rol = RolSeleccionado;

            var exito = await _usuarioService.UpdateAsync(_usuarioOriginal.UsuarioId, _usuarioOriginal);

            if (exito)
            {
                await _logService.RegistrarAccionAsync($"Editó usuario: {Correo}");
                await Shell.Current.DisplayAlert("Éxito", "Usuario actualizado.", "OK");
                await Shell.Current.GoToAsync("//UsuariosPage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo actualizar el usuario.", "OK");
            }
        }
    }
}
