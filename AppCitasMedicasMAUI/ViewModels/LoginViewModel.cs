using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using System.Windows.Input;
using System.Diagnostics;

namespace AppCitasMedicasMAUI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly UsuarioApiService _usuarioApiService;

        public LoginViewModel(UsuarioApiService usuarioApiService)
        {
            _usuarioApiService = usuarioApiService;
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

            Debug.WriteLine($"Intentando login con {Correo}");

            try
            {
                var usuario = await _usuarioApiService.LoginAsync(Correo, Contrasena);
                if (usuario != null)
                {
                    Debug.WriteLine($"Login exitoso para {usuario.Correo}, navegando...");
                    await Shell.Current.GoToAsync("//AdminTabs");  
                }
                else
                {
                    MensajeError = "Correo o contraseña incorrectos.";
                }
            }
            catch (Exception ex)
            {
                MensajeError = $"Error al iniciar sesión: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }
    }
}
