using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Service;
using AppCitasMedicasMAUI.Services;
using System.Collections.Generic;
using System.Linq;

namespace AppCitasMedicasMAUI.Views
{
    public partial class CrearPacientePage : ContentPage
    {
        private readonly UsuarioService _usuarioService;
        private readonly PacienteService _pacienteService;
        private List<Usuario> _usuariosPaciente;

        public CrearPacientePage()
        {
            InitializeComponent();
            _usuarioService = new UsuarioService(new HttpClient { BaseAddress = new Uri("https://localhost:7062/") });
            _pacienteService = new PacienteService(new HttpClient { BaseAddress = new Uri("https://localhost:7062/") });
            CargarUsuariosPaciente();
        }

        private async void CargarUsuariosPaciente()
        {
            try
            {
                var usuarios = await _usuarioService.GetUsuariosAsync();
                var usuariosPaciente = usuarios.Where(u => u.Rol == RolUsuario.Paciente).ToList();

                UsuarioPicker.ItemsSource = usuariosPaciente;
                UsuarioPicker.ItemDisplayBinding = new Binding("Correo");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al cargar usuarios: {ex.Message}", "OK");
            }
        }


        private async void OnCrearClicked(object sender, EventArgs e)
        {
            if (UsuarioPicker.SelectedItem == null)
            {
                await DisplayAlert("Validación", "Debe seleccionar un usuario.", "OK");
                return;
            }

            var usuarioSeleccionado = (Usuario)UsuarioPicker.SelectedItem;

            var nuevoPaciente = new Paciente
            {
                Nombres = NombresEntry.Text,
                Apellidos = ApellidosEntry.Text,
                Cedula = CedulaEntry.Text,
                Edad = int.TryParse(EdadEntry.Text, out int edad) ? edad : 0,
                Altura = double.TryParse(AlturaEntry.Text, out double altura) ? altura : (double?)null,
                Peso = double.TryParse(PesoEntry.Text, out double peso) ? peso : (double?)null,
                Direccion = DireccionEntry.Text,
                Telefono = TelefonoEntry.Text,
                UsuarioId = usuarioSeleccionado.UsuarioId
            };

            var resultado = await _pacienteService.CreatePacienteAsync(nuevoPaciente);

            if (resultado)
            {
                await DisplayAlert("Éxito", "Paciente creado correctamente.", "OK");
                await Navigation.PopAsync(); // Regresa a la página anterior (PacientesPage)
            }
            else
            {
                await DisplayAlert("Error", "No se pudo crear el paciente.", "OK");
            }
        }
    }
}
