using SQLite;

namespace AppCitasMedicasMAUI.Models
{
    [Table("Administradores")]
    public class Administrador
    {
        [PrimaryKey, AutoIncrement]
        public int AdministradorId { get; set; }

        [MaxLength(100), NotNull]
        public string Nombre { get; set; }

        [MaxLength(15), NotNull]
        public string Telefono { get; set; }

        [NotNull]
        public int UsuarioId { get; set; }
    }
}
