using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using System.Windows.Input;
using System.Diagnostics;
using Microsoft.Maui.Storage; 

namespace AppCitasMedicasMAUI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly UsuarioApiService _usuarioApiService;
        private readonly LogService _logService;

        public LoginViewModel(UsuarioApiService usuarioApiService, LogService logService)
        {
            _usuarioApiService = usuarioApiService;
            _logService = logService;

            LoginCommand = new Command(async () => await LoginAsync(), () => !IsBusy);
        }

        private string correo;
        public string Correo
        {
            get => correo;
            set => SetProperty(ref correo, value);
        }

        private string contrasena;
        public string Contrasena
        {
            get => contrasena;
            set => SetProperty(ref contrasena, value);
        }

        private string mensajeError;
        public string MensajeError
        {
            get => mensajeError;
            set
            {
                SetProperty(ref mensajeError, value);
                IsErrorVisible = !string.IsNullOrEmpty(value);
            }
        }

        private bool isErrorVisible;
        public bool IsErrorVisible
        {
            get => isErrorVisible;
            set => SetProperty(ref isErrorVisible, value);
        }

        public ICommand LoginCommand { get; }

        private async Task LoginAsync()
        {
            if (IsBusy) return;

            IsBusy = true;
            ((Command)LoginCommand).ChangeCanExecute();
            MensajeError = string.Empty;

            try
            {
                var usuario = await _usuarioApiService.LoginAsync(Correo, Contrasena);
                if (usuario != null)
                {
                    // Guardar ID y Rol en Preferences
                    Preferences.Set("UsuarioId", usuario.UsuarioId);
                    Preferences.Set("RolUsuario", usuario.Rol.ToString());

                    await _logService.RegistrarAccionAsync($"Inicio de sesión exitoso: {usuario.Correo}");

                    // Navegar según el rol del usuario
                    switch (usuario.Rol)
                    {
                        case RolUsuario.Administrador:
                            await Shell.Current.GoToAsync("//AdminTabs");
                            break;

                        case RolUsuario.Medico:
                            await Shell.Current.GoToAsync("//MedicoTabs");
                            break;

                        case RolUsuario.Paciente:
                            await Shell.Current.GoToAsync("//PacienteTabs"); // <-- si más adelante creas este
                            break;

                        default:
                            MensajeError = "Rol no reconocido.";
                            break;
                    }
                }
                else
                {
                    MensajeError = "Correo o contraseña incorrectos.";
                    await _logService.RegistrarAccionAsync($"Intento fallido de inicio de sesión con: {Correo}");
                }
            }
            catch (Exception ex)
            {
                MensajeError = $"Error al iniciar sesión: {ex.Message}";
                await _logService.RegistrarAccionAsync($"Error inesperado al iniciar sesión con: {Correo} - {ex.Message}");
            }
            finally
            {
                IsBusy = false;
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }
    }
}
