using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;
using System.Windows.Input;

public class EditarMedicoViewModel : BaseViewModel
{
    private readonly MedicoApiService _medicoService;
    private readonly LogService _logService;

    private Medico _medicoOriginal;

    public ICommand GuardarCommand { get; }
    public ICommand CancelarCommand { get; }

    public EditarMedicoViewModel(MedicoApiService medicoService, LogService logService)
    {
        _medicoService = medicoService;
        _logService = logService;

        GuardarCommand = new Command(async () => await GuardarCambiosAsync());
        CancelarCommand = new Command(async () => await Shell.Current.GoToAsync("//MedicosPage"));
    }

    // Propiedades editables
    public string Nombre { get; set; }
    public string Especialidad { get; set; }
    public string Telefono { get; set; }
    public string UbicacionConsultorio { get; set; }

    public void Inicializar(Medico medico)
    {
        _medicoOriginal = medico;

        Nombre = medico.Nombre;
        Especialidad = medico.Especialidad;
        Telefono = medico.Telefono;
        UbicacionConsultorio = medico.UbicacionConsultorio;

        OnPropertyChanged(nameof(Nombre));
        OnPropertyChanged(nameof(Especialidad));
        OnPropertyChanged(nameof(Telefono));
        OnPropertyChanged(nameof(UbicacionConsultorio));
    }

    private async Task GuardarCambiosAsync()
    {
        if (_medicoOriginal == null) return;

        _medicoOriginal.Nombre = Nombre;
        _medicoOriginal.Especialidad = Especialidad;
        _medicoOriginal.Telefono = Telefono;
        _medicoOriginal.UbicacionConsultorio = UbicacionConsultorio;

        var exito = await _medicoService.UpdateAsync(_medicoOriginal.MedicoId, _medicoOriginal);

        if (exito)
        {
            await Shell.Current.DisplayAlert("Éxito", "Médico actualizado correctamente.", "OK");
            await _logService.RegistrarAccionAsync($"Editó médico: {Nombre}");
            await Shell.Current.GoToAsync("//MedicosPage");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo actualizar el médico.", "OK");
        }
    }
}
