using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using AppCitasMedicasMAUI.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

public class PacientesViewModel : BaseViewModel
{
    private readonly PacienteApiService _pacienteService;
    private readonly UsuarioApiService _usuarioService;
    private readonly LogService _logService;

    public ObservableCollection<Paciente> Pacientes { get; } = new();

    public ICommand CargarCommand { get; }
    public ICommand EliminarCommand { get; }
    public ICommand CrearPacienteCommand { get; }
    public ICommand EditarCommand { get; }

    public PacientesViewModel(PacienteApiService pacienteService, UsuarioApiService usuarioService, LogService logService)
    {
        _pacienteService = pacienteService;
        _usuarioService = usuarioService;
        _logService = logService;

        CargarCommand = new Command(async () => await CargarPacientesAsync());
        EliminarCommand = new Command<Paciente>(async (paciente) => await EliminarPacienteAsync(paciente));
        CrearPacienteCommand = new Command(async () => await IrACrearPacienteAsync());
        EditarCommand = new Command<Paciente>(async (paciente) => await IrAEditarPacienteAsync(paciente));
    }

    public async Task CargarPacientesAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            Pacientes.Clear();
            var lista = await _pacienteService.GetAllAsync();
            foreach (var paciente in lista)
                Pacientes.Add(paciente);
        }
        catch (Exception ex)
        {
            MensajeError = $"Error al cargar pacientes: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task EliminarPacienteAsync(Paciente paciente)
    {
        if (paciente == null) return;

        var confirmado = await Shell.Current.DisplayAlert(
            "Confirmar", $"¿Eliminar a {paciente.Nombres}?", "Sí", "No");

        if (!confirmado) return;

        var resultado = await _pacienteService.DeleteAsync(paciente.PacienteId);

        if (resultado)
        {
            Pacientes.Remove(paciente);
            await Shell.Current.DisplayAlert("Éxito", "Paciente eliminado", "OK");
            await _logService.RegistrarAccionAsync($"Eliminó paciente: {paciente.Nombres} {paciente.Apellidos}");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo eliminar el paciente", "OK");
        }
    }

    private async Task IrACrearPacienteAsync() => await Shell.Current.GoToAsync("//CrearPacientePage");

    private async Task IrAEditarPacienteAsync(Paciente paciente)
    {
        if (paciente == null) return;

        var navParam = new Dictionary<string, object>
        {
            { "Paciente", paciente }
        };

        await Shell.Current.GoToAsync("//EditarPacientePage", navParam);
    }
}
