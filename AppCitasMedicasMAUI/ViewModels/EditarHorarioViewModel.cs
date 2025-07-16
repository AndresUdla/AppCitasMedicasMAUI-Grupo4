using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace AppCitasMedicasMAUI.ViewModels
{
    public class EditarHorarioViewModel : BaseViewModel
    {
        private readonly HorarioApiService _horarioService;
        private readonly LogService _logService;

        public List<DayOfWeek> DiasSemana { get; } = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().ToList();

        private int _horarioId;
        private int _medicoId;
        private DayOfWeek _dia;
        private TimeSpan _horaInicio;
        private TimeSpan _horaFin;

        public DayOfWeek Dia
        {
            get => _dia;
            set => SetProperty(ref _dia, value);
        }

        public TimeSpan HoraInicio
        {
            get => _horaInicio;
            set => SetProperty(ref _horaInicio, value);
        }

        public TimeSpan HoraFin
        {
            get => _horaFin;
            set => SetProperty(ref _horaFin, value);
        }

        public ICommand GuardarCommand { get; }
        public ICommand CancelarCommand { get; }

        public EditarHorarioViewModel(HorarioApiService horarioService, LogService logService)
        {
            _horarioService = horarioService;
            _logService = logService;

            GuardarCommand = new Command(async () => await GuardarCambiosAsync());
            CancelarCommand = new Command(async () => await Shell.Current.GoToAsync("//HorariosMedicoPage"));
        }

        public void InicializarConHorario(Horario horario)
        {
            _horarioId = horario.HorarioId;
            _medicoId = horario.MedicoId; // <-- IMPORTANTE
            Dia = horario.Dia;
            HoraInicio = horario.HoraInicio;
            HoraFin = horario.HoraFin;
        }

        private async Task GuardarCambiosAsync()
        {
            if (HoraInicio >= HoraFin)
            {
                await Shell.Current.DisplayAlert("Error", "La hora de inicio debe ser menor a la hora de fin.", "OK");
                return;
            }

            var horarioActualizado = new Horario
            {
                HorarioId = _horarioId,
                MedicoId = _medicoId, // <-- IMPORTANTE
                Dia = Dia,
                HoraInicio = HoraInicio,
                HoraFin = HoraFin
            };

            var resultado = await _horarioService.UpdateAsync(_horarioId, horarioActualizado);

            if (resultado)
            {
                await _logService.RegistrarAccionAsync($"Horario actualizado: {Dia} de {HoraInicio} a {HoraFin}");
                await Shell.Current.DisplayAlert("Éxito", "Horario actualizado correctamente.", "OK");
                await Shell.Current.GoToAsync("//HorariosMedicoPage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo actualizar el horario.", "OK");
            }
        }
    }
}
