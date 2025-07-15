using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace AppCitasMedicasMAUI.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string correo;
        private string contrasena;
        private string mensajeError;
        private bool isErrorVisible;

        private readonly UsuarioService _usuarioService;

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
            _usuarioService = new UsuarioService();
            LoginCommand = new Command(async () => await LoginAsync());
        }

        public string Correo
        {
            get => correo;
            set
            {
                correo = value;
                OnPropertyChanged();
            }
        }

        public string Contrasena
        {
            get => contrasena;
            set
            {
                contrasena = value;
                OnPropertyChanged();
            }
        }

        public string MensajeError
        {
            get => mensajeError;
            set
            {
                mensajeError = value;
                OnPropertyChanged();
            }
        }

        public bool IsErrorVisible
        {
            get => isErrorVisible;
            set
            {
                isErrorVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        private async Task LoginAsync()
        {
            IsErrorVisible = false;

            if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Contrasena))
            {
                MensajeError = "Por favor, llena todos los campos.";
                IsErrorVisible = true;
                return;
            }

            try
            {
                var usuario = await _usuarioService.LoginAsync(Correo.Trim(), Contrasena);

                if (usuario != null)
                {
                    if (usuario.Rol == RolUsuario.Administrador)
                    {
                        // Navegar a la vista AdminTabs usando Shell
                        await Shell.Current.GoToAsync("//AdminTabs");
                    }
                    else
                    {
                        MensajeError = "Acceso denegado. Solo administradores pueden ingresar.";
                        IsErrorVisible = true;
                    }
                }
                else
                {
                    MensajeError = "Correo o contraseña incorrectos.";
                    IsErrorVisible = true;
                }
            }
            catch
            {
                MensajeError = "Error al conectar con el servidor.";
                IsErrorVisible = true;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
