using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using Microsoft.Maui.Storage;
using System.Windows.Input;

namespace AppCitasMedicasMAUI.ViewModels
{
    public class CrearHorarioViewModel : BaseViewModel
    {
        private readonly HorarioApiService _horarioService;
        private readonly MedicoApiService _medicoService;
        private readonly LogService _logService;

        public List<DayOfWeek> DiasSemana { get; } = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().ToList();

        private DayOfWeek _dia;
        public DayOfWeek Dia
        {
            get => _dia;
            set => SetProperty(ref _dia, value);
        }

        public TimeSpan HoraInicio { get; set; } = new(8, 0, 0);
        public TimeSpan HoraFin { get; set; } = new(12, 0, 0);

        public ICommand CrearCommand { get; }
        public ICommand CancelarCommand { get; }

        private int _medicoId;

        public CrearHorarioViewModel(HorarioApiService horarioService, MedicoApiService medicoService, LogService logService)
        {
            _horarioService = horarioService;
            _medicoService = medicoService;
            _logService = logService;

            CrearCommand = new Command(async () => await CrearHorarioAsync());
            CancelarCommand = new Command(async () => await Shell.Current.GoToAsync("//HorariosMedicoPage"));
        }

        public async Task InicializarAsync()
        {
            int usuarioId = Preferences.Get("UsuarioId", 0);
            var medicos = await _medicoService.GetAllAsync();
            var medicoActual = medicos.FirstOrDefault(m => m.UsuarioId == usuarioId);

            if (medicoActual != null)
            {
                _medicoId = medicoActual.MedicoId;
            }
            else
            {
                MensajeError = "No se encontró al médico actual.";
            }
        }

        private async Task CrearHorarioAsync()
        {
            if (HoraInicio >= HoraFin)
            {
                await Shell.Current.DisplayAlert("Error", "La hora de inicio debe ser menor a la hora de fin.", "OK");
                return;
            }

            var nuevoHorario = new Horario
            {
                Dia = Dia,
                HoraInicio = HoraInicio,
                HoraFin = HoraFin,
                MedicoId = _medicoId
            };

            var creado = await _horarioService.CreateAsync(nuevoHorario);

            if (creado != null)
            {
                await _logService.RegistrarAccionAsync($"Horario creado: {Dia} de {HoraInicio} a {HoraFin}");
                await Shell.Current.DisplayAlert("Éxito", "Horario creado exitosamente.", "OK");
                await Shell.Current.GoToAsync("//HorariosMedicoPage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo crear el horario.", "OK");
            }
        }
    }
}
