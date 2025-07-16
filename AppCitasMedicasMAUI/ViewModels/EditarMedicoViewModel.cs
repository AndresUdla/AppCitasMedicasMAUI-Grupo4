using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using System.Windows.Input;

namespace AppCitasMedicasMAUI.ViewModels
{
    [QueryProperty(nameof(Medico), "Medico")]
    public class EditarMedicoViewModel : BaseViewModel
    {
        private readonly MedicoApiService _medicoService;
        private readonly LogService _logService;

        private Medico _medico;

        public Medico Medico
        {
            get => _medico;
            set
            {
                _medico = value;
                OnPropertyChanged(nameof(Nombre));
                OnPropertyChanged(nameof(Especialidad));
                OnPropertyChanged(nameof(Telefono));
                OnPropertyChanged(nameof(UbicacionConsultorio));
            }
        }

        public string Nombre
        {
            get => Medico?.Nombre;
            set
            {
                if (Medico != null)
                {
                    Medico.Nombre = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Especialidad
        {
            get => Medico?.Especialidad;
            set
            {
                if (Medico != null)
                {
                    Medico.Especialidad = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Telefono
        {
            get => Medico?.Telefono;
            set
            {
                if (Medico != null)
                {
                    Medico.Telefono = value;
                    OnPropertyChanged();
                }
            }
        }

        public string UbicacionConsultorio
        {
            get => Medico?.UbicacionConsultorio;
            set
            {
                if (Medico != null)
                {
                    Medico.UbicacionConsultorio = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand GuardarCommand { get; }

        public EditarMedicoViewModel(MedicoApiService medicoService, LogService logService)
        {
            _medicoService = medicoService;
            _logService = logService;
            GuardarCommand = new Command(async () => await GuardarAsync());
        }

        private async Task GuardarAsync()
        {
            if (IsBusy || Medico == null) return;
            IsBusy = true;
            MensajeError = string.Empty;

            try
            {
                bool resultado = await _medicoService.UpdateAsync(Medico.MedicoId, Medico);

                if (resultado)
                {
                    await _logService.RegistrarAccionAsync($"Editó médico: {Medico.Nombre}");
                    await Shell.Current.DisplayAlert("Éxito", "Médico actualizado correctamente.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    MensajeError = "No se pudo actualizar el médico.";
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
