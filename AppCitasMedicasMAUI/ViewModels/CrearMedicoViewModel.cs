using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

public class CrearMedicoViewModel : BaseViewModel
{
    private readonly MedicoApiService _medicoService;
    private readonly UsuarioApiService _usuarioService;
    private readonly LogService _logService;

    public ObservableCollection<Usuario> UsuariosMedico { get; } = new();

    public ICommand CrearCommand { get; }
    public ICommand CancelarCommand { get; }

    private Usuario _usuarioSeleccionado;
    public Usuario UsuarioSeleccionado
    {
        get => _usuarioSeleccionado;
        set => SetProperty(ref _usuarioSeleccionado, value);
    }

    public string Nombre { get; set; }
    public string Especialidad { get; set; }
    public string Telefono { get; set; }
    public string UbicacionConsultorio { get; set; }

    public CrearMedicoViewModel(MedicoApiService medicoService, UsuarioApiService usuarioService, LogService logService)
    {
        _medicoService = medicoService;
        _usuarioService = usuarioService;
        _logService = logService;

        CrearCommand = new Command(async () => await CrearMedicoAsync());
        CancelarCommand = new Command(async () => await Shell.Current.GoToAsync("//MedicosPage"));
    }

    public async Task InicializarAsync()
    {
        try
        {
            var usuarios = await _usuarioService.GetAllAsync();
            var medicos = usuarios.Where(u => u.Rol == RolUsuario.Medico);

            UsuariosMedico.Clear();
            foreach (var u in medicos)
                UsuariosMedico.Add(u);
        }
        catch (Exception ex)
        {
            MensajeError = $"Error al cargar usuarios: {ex.Message}";
        }
    }

    private async Task CrearMedicoAsync()
    {
        if (UsuarioSeleccionado == null)
        {
            await Shell.Current.DisplayAlert("Validación", "Seleccione un usuario.", "OK");
            return;
        }

        var nuevo = new Medico
        {
            UsuarioId = UsuarioSeleccionado.UsuarioId,
            Nombre = Nombre,
            Especialidad = Especialidad,
            Telefono = Telefono,
            UbicacionConsultorio = UbicacionConsultorio
        };

        var exito = await _medicoService.CreateAsync(nuevo);

        if (exito != null)
        {
            await Shell.Current.DisplayAlert("Éxito", "Médico creado.", "OK");
            await _logService.RegistrarAccionAsync($"Creó médico: {nuevo.Nombre}");
            await Shell.Current.GoToAsync("//MedicosPage");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo crear el médico.", "OK");
        }
    }
}
