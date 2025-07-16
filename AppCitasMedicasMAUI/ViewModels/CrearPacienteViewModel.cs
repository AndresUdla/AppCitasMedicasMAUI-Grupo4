using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppCitasMedicasMAUI.ViewModels
{
    public class CrearPacienteViewModel : BaseViewModel
    {
        private readonly PacienteApiService _pacienteService;
        private readonly UsuarioApiService _usuarioService;

        public ObservableCollection<Usuario> UsuariosPaciente { get; } = new();

        public ICommand CrearCommand { get; }
        public ICommand CancelarCommand { get; }

        private Usuario _usuarioSeleccionado;
        public Usuario UsuarioSeleccionado
        {
            get => _usuarioSeleccionado;
            set => SetProperty(ref _usuarioSeleccionado, value);
        }

        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public string Edad { get; set; }
        public string Altura { get; set; }
        public string Peso { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public CrearPacienteViewModel(PacienteApiService pacienteService, UsuarioApiService usuarioService)
        {
            _pacienteService = pacienteService;
            _usuarioService = usuarioService;

            CrearCommand = new Command(async () => await CrearPacienteAsync());
            CancelarCommand = new Command(async () => await Shell.Current.GoToAsync("//PacientesPage"));
        }

        public async Task InicializarAsync()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllAsync();
                var pacientes = usuarios.Where(u => u.Rol == RolUsuario.Paciente);

                UsuariosPaciente.Clear();
                foreach (var u in pacientes)
                    UsuariosPaciente.Add(u);
            }
            catch (Exception ex)
            {
                MensajeError = $"Error al cargar usuarios: {ex.Message}";
            }
        }

        private async Task CrearPacienteAsync()
        {
            if (UsuarioSeleccionado == null)
            {
                await Shell.Current.DisplayAlert("Validación", "Seleccione un usuario.", "OK");
                return;
            }

            var nuevo = new Paciente
            {
                UsuarioId = UsuarioSeleccionado.UsuarioId,
                Nombres = Nombres,
                Apellidos = Apellidos,
                Cedula = Cedula,
                Edad = int.TryParse(Edad, out var edad) ? edad : 0,
                Altura = double.TryParse(Altura, out var altura) ? altura : null,
                Peso = double.TryParse(Peso, out var peso) ? peso : null,
                Direccion = Direccion,
                Telefono = Telefono
            };

            var exito = await _pacienteService.CreateAsync(nuevo);

            if (exito != null)
            {
                await Shell.Current.DisplayAlert("Éxito", "Paciente creado.", "OK");
                await Shell.Current.GoToAsync("//PacientesPage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo crear el paciente.", "OK");
            }
        }
    }
}
