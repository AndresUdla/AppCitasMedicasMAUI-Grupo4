using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AppCitasMedicasMAUI.ViewModels
{
    public class MedicosViewModel : BaseViewModel
    {
        private readonly MedicoApiService _medicoService;
        private readonly LogService _logService;

        public ObservableCollection<Medico> Medicos { get; } = new();

        public ICommand CargarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand CrearMedicoCommand { get; }
        public ICommand EditarCommand { get; }

        public MedicosViewModel(MedicoApiService medicoService, LogService logService)
        {
            _medicoService = medicoService;
            _logService = logService;

            CargarCommand = new Command(async () => await CargarMedicosAsync());
            EliminarCommand = new Command<Medico>(async (medico) => await EliminarMedicoAsync(medico));
            CrearMedicoCommand = new Command(async () => await IrACrearMedicoAsync());
            EditarCommand = new Command<Medico>(async (medico) => await IrAEditarMedicoAsync(medico));
        }

        public async Task CargarMedicosAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                Medicos.Clear();
                var lista = await _medicoService.GetAllAsync();
                foreach (var medico in lista)
                    Medicos.Add(medico);
            }
            catch (Exception ex)
            {
                MensajeError = $"Error al cargar médicos: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task EliminarMedicoAsync(Medico medico)
        {
            if (medico == null) return;

            bool confirmado = await Shell.Current.DisplayAlert(
                "Confirmar", $"¿Eliminar al Dr. {medico.Nombre}?", "Sí", "No");

            if (!confirmado) return;

            bool resultado = await _medicoService.DeleteAsync(medico.MedicoId);

            if (resultado)
            {
                Medicos.Remove(medico);
                await _logService.RegistrarAccionAsync($"Eliminó médico: {medico.Nombre}");
                await Shell.Current.DisplayAlert("Éxito", "Médico eliminado.", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo eliminar el médico.", "OK");
            }
        }

        private async Task IrACrearMedicoAsync()
        {
            await Shell.Current.GoToAsync("//CrearMedicoPage");
        }

        private async Task IrAEditarMedicoAsync(Medico medico)
        {
            if (medico == null) return;

            var navParam = new Dictionary<string, object>
            {
                { "Medico", medico }
            };

            await Shell.Current.GoToAsync("//EditarMedicoPage", navParam);
        }
    }
}
