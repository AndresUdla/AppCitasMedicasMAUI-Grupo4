using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Storage;

namespace AppCitasMedicasMAUI.ViewModels
{
    public class HorariosMedicoViewModel : BaseViewModel
    {
        private readonly HorarioApiService _horarioService;
        private readonly MedicoApiService _medicoService;
        private readonly LogService _logService;

        public ObservableCollection<Horario> Horarios { get; } = new();

        public ICommand CargarCommand { get; }
        public ICommand CrearCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand EliminarCommand { get; }

        private int _medicoId;

        public HorariosMedicoViewModel(
            HorarioApiService horarioService,
            MedicoApiService medicoService,
            LogService logService)
        {
            _horarioService = horarioService;
            _medicoService = medicoService;
            _logService = logService;

            CargarCommand = new Command(async () => await CargarHorariosAsync());
            CrearCommand = new Command(async () => await Shell.Current.GoToAsync("//CrearHorarioPage"));
            EditarCommand = new Command<Horario>(async (h) => await IrAEditarHorarioAsync(h));
            EliminarCommand = new Command<Horario>(async (h) => await EliminarHorarioAsync(h));
        }

        public async Task InicializarAsync()
        {
            int usuarioId = Preferences.Get("UsuarioId", 0);
            var medico = await _medicoService.GetAllAsync();
            var medicoActual = medico.FirstOrDefault(m => m.UsuarioId == usuarioId);

            if (medicoActual != null)
            {
                _medicoId = medicoActual.MedicoId;
                await CargarHorariosAsync();
            }
            else
            {
                MensajeError = "No se pudo encontrar el médico asociado al usuario.";
            }
        }

        private async Task CargarHorariosAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                Horarios.Clear();
                var lista = await _horarioService.GetAllAsync();
                var horariosMedico = lista.Where(h => h.MedicoId == _medicoId);

                foreach (var h in horariosMedico)
                    Horarios.Add(h);
            }
            catch (Exception ex)
            {
                MensajeError = $"Error al cargar horarios: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task IrAEditarHorarioAsync(Horario horario)
        {
            if (horario == null) return;

            var navParam = new Dictionary<string, object> { { "Horario", horario } };
            await Shell.Current.GoToAsync("//EditarHorarioPage", navParam);
        }

        private async Task EliminarHorarioAsync(Horario horario)
        {
            if (horario == null) return;

            bool confirmado = await Shell.Current.DisplayAlert(
                "Confirmar", $"¿Eliminar horario del {horario.Dia}?", "Sí", "No");

            if (!confirmado) return;

            bool resultado = await _horarioService.DeleteAsync(horario.HorarioId);

            if (resultado)
            {
                Horarios.Remove(horario);
                await Shell.Current.DisplayAlert("Éxito", "Horario eliminado", "OK");
                await _logService.RegistrarAccionAsync($"Eliminó horario del {horario.Dia}");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo eliminar el horario", "OK");
            }
        }
    }
}
