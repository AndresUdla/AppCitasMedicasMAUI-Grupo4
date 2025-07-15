using SQLite;

namespace AppCitasMedicasMAUI.Models
{
    [Table("Medicos")]
    public class Medico
    {
        [PrimaryKey, AutoIncrement]
        public int MedicoId { get; set; }

        [MaxLength(100), NotNull]
        public string Nombre { get; set; }

        [MaxLength(100), NotNull]
        public string Especialidad { get; set; }

        [MaxLength(15), NotNull]
        public string Telefono { get; set; }

        [MaxLength(200)]
        public string? UbicacionConsultorio { get; set; }

        [NotNull]
        public int UsuarioId { get; set; }
    }
}
