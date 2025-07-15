using SQLite;

namespace AppCitasMedicasMAUI.Models
{
    [Table("Pacientes")]
    public class Paciente
    {
        [PrimaryKey, AutoIncrement]
        public int PacienteId { get; set; }

        [MaxLength(100), NotNull]
        public string Nombres { get; set; }

        [MaxLength(100), NotNull]
        public string Apellidos { get; set; }

        [MaxLength(10), NotNull]
        public string Cedula { get; set; }

        [NotNull]
        public int Edad { get; set; }

        public double? Altura { get; set; }

        public double? Peso { get; set; }

        [MaxLength(200)]
        public string? Direccion { get; set; }

        [MaxLength(15)]
        public string? Telefono { get; set; }

        [NotNull]
        public int UsuarioId { get; set; }
    }
}
