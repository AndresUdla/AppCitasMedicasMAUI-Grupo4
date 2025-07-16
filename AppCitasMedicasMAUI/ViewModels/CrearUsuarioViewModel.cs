using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using System.Windows.Input;

namespace AppCitasMedicasMAUI.ViewModels
{
    public class CrearUsuarioViewModel : BaseViewModel
    {
        private readonly UsuarioApiService _usuarioService;
        private readonly LogService _logService;

        public ICommand CrearCommand { get; }
        public ICommand CancelarCommand { get; }

        public CrearUsuarioViewModel(UsuarioApiService usuarioService, LogService logService)
        {
            _usuarioService = usuarioService;
            _logService = logService;

            CrearCommand = new Command(async () => await CrearUsuarioAsync());
            CancelarCommand = new Command(async () => await Shell.Current.GoToAsync("//UsuariosPage"));
        }

        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public RolUsuario RolSeleccionado { get; set; } = RolUsuario.Paciente;

        public List<RolUsuario> Roles => Enum.GetValues(typeof(RolUsuario)).Cast<RolUsuario>().ToList();

        private async Task CrearUsuarioAsync()
        {
            if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Contrasena))
            {
                await Shell.Current.DisplayAlert("Validación", "Debe llenar todos los campos.", "OK");
                return;
            }

            var nuevo = new Usuario
            {
                Correo = Correo,
                Contrasena = Contrasena,
                Rol = RolSeleccionado
            };

            var creado = await _usuarioService.CreateAsync(nuevo);
            if (creado != null)
            {
                await _logService.RegistrarAccionAsync($"Creó usuario: {Correo}");
                await Shell.Current.DisplayAlert("Éxito", "Usuario creado correctamente.", "OK");
                await Shell.Current.GoToAsync("//UsuariosPage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo crear el usuario.", "OK");
            }
        }
    }
}
