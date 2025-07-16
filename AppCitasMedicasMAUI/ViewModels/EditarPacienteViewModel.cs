using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;
using System.Windows.Input;

public class EditarPacienteViewModel : BaseViewModel
{
    private readonly PacienteApiService _pacienteService;
    private readonly LogService _logService;
    private Paciente _pacienteOriginal;

    public ICommand GuardarCommand { get; }
    public ICommand CancelarCommand { get; }

    public EditarPacienteViewModel(PacienteApiService pacienteService, LogService logService)
    {
        _pacienteService = pacienteService;
        _logService = logService;
        GuardarCommand = new Command(async () => await GuardarCambiosAsync());
        CancelarCommand = new Command(async () => await Shell.Current.GoToAsync("//PacientesPage"));
    }

    public void Inicializar(Paciente paciente)
    {
        _pacienteOriginal = paciente;

        Nombres = paciente.Nombres;
        Apellidos = paciente.Apellidos;
        Cedula = paciente.Cedula;
        Edad = paciente.Edad.ToString();
        Altura = paciente.Altura?.ToString();
        Peso = paciente.Peso?.ToString();
        Direccion = paciente.Direccion;
        Telefono = paciente.Telefono;

        OnPropertyChanged(nameof(Nombres));
        OnPropertyChanged(nameof(Apellidos));
        OnPropertyChanged(nameof(Cedula));
        OnPropertyChanged(nameof(Edad));
        OnPropertyChanged(nameof(Altura));
        OnPropertyChanged(nameof(Peso));
        OnPropertyChanged(nameof(Direccion));
        OnPropertyChanged(nameof(Telefono));
    }

    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Cedula { get; set; }
    public string Edad { get; set; }
    public string Altura { get; set; }
    public string Peso { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }

    private async Task GuardarCambiosAsync()
    {
        if (_pacienteOriginal == null) return;

        _pacienteOriginal.Nombres = Nombres;
        _pacienteOriginal.Apellidos = Apellidos;
        _pacienteOriginal.Cedula = Cedula;
        _pacienteOriginal.Edad = int.TryParse(Edad, out var edad) ? edad : 0;
        _pacienteOriginal.Altura = double.TryParse(Altura, out var altura) ? altura : null;
        _pacienteOriginal.Peso = double.TryParse(Peso, out var peso) ? peso : null;
        _pacienteOriginal.Direccion = Direccion;
        _pacienteOriginal.Telefono = Telefono;

        var exito = await _pacienteService.UpdateAsync(_pacienteOriginal.PacienteId, _pacienteOriginal);

        if (exito)
        {
            await Shell.Current.DisplayAlert("Éxito", "Paciente actualizado.", "OK");
            await _logService.RegistrarAccionAsync($"Editó paciente: {Nombres} {Apellidos}");
            await Shell.Current.GoToAsync("//PacientesPage");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo actualizar el paciente.", "OK");
        }
    }
}
