using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using System.Windows.Input;

namespace AppCitasMedicasMAUI.ViewModels
{
    public class CrearMedicoViewModel : BaseViewModel
    {
        private readonly MedicoApiService _medicoService;
        private readonly LogService _logService;

        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public string Telefono { get; set; }
        public string UbicacionConsultorio { get; set; }

        public ICommand GuardarCommand { get; }

        public CrearMedicoViewModel(MedicoApiService medicoService, LogService logService)
        {
            _medicoService = medicoService;
            _logService = logService;
            GuardarCommand = new Command(async () => await GuardarAsync());
        }

        private async Task GuardarAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            MensajeError = string.Empty;

            try
            {
                var nuevoMedico = new Medico
                {
                    Nombre = this.Nombre,
                    Especialidad = this.Especialidad,
                    Telefono = this.Telefono,
                    UbicacionConsultorio = this.UbicacionConsultorio,
                    UsuarioId = 1 // o el ID del usuario relacionado si se maneja
                };

                var resultado = await _medicoService.CreateAsync(nuevoMedico);

                if (resultado != null)
                {
                    await _logService.RegistrarAccionAsync($"Creó médico: {resultado.Nombre}");
                    await Shell.Current.DisplayAlert("Éxito", "Médico creado correctamente.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    MensajeError = "No se pudo crear el médico.";
                }
            }
            catch (Exception ex)
            {
                MensajeError = $"Error: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
