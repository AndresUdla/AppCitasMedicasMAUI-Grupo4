using SQLite;
using System;

namespace AppCitasMedicasMAUI.Models
{
    [Table("Horarios")]
    public class Horario
    {
        [PrimaryKey, AutoIncrement]
        public int HorarioId { get; set; }

        [NotNull]
        public DayOfWeek Dia { get; set; }

        [NotNull]
        public TimeSpan HoraInicio { get; set; }

        [NotNull]
        public TimeSpan HoraFin { get; set; }

        [NotNull]
        public int MedicoId { get; set; }
    }
}
