using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Service;
using AppCitasMedicasMAUI.Views;
using System;

namespace AppCitasMedicasMAUI.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly UsuarioService _usuarioService;

        public LoginPage()
        {
            InitializeComponent();
            _usuarioService = new UsuarioService(new HttpClient { BaseAddress = new Uri(ApiConstants.BaseUrl) });
        }


        private async void LogginButton_Clicked(object sender, EventArgs e)
        {
            MensajeError.IsVisible = false;

            var correo = CorreoEntry.Text?.Trim();
            var contrasena = ContrasenaEntry.Text;

            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            {
                MensajeError.Text = "Por favor, llena todos los campos.";
                MensajeError.IsVisible = true;
                return;
            }

            try
            {
                var usuario = await _usuarioService.LoginAsync(correo, contrasena);

                if (usuario != null)
                {
                    if (usuario.Rol == RolUsuario.Administrador)
                    {

                        await Shell.Current.GoToAsync("//AdminTabs");
                    }
                    else
                    {
                        MensajeError.Text = "Acceso denegado. Solo administradores pueden ingresar.";
                        MensajeError.IsVisible = true;
                    }
                }
                else
                {
                    MensajeError.Text = "Correo o contraseña incorrectos.";
                    MensajeError.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                MensajeError.Text = "Error al conectar con el servidor.";
                MensajeError.IsVisible = true;
                Console.WriteLine($"Error de login: {ex.Message}");
            }
        }
    }
}
