using SQLite;
using System;

namespace AppCitasMedicasMAUI.Models
{
    [Table("Citas")]
    public class Cita
    {
        [PrimaryKey, AutoIncrement]
        public int CitaId { get; set; }

        [NotNull]
        public DateTime FechaCita { get; set; }

        [NotNull]
        public int PacienteId { get; set; }

        [NotNull]
        public int MedicoId { get; set; }

        [NotNull]
        public int HorarioId { get; set; }

        [NotNull]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
