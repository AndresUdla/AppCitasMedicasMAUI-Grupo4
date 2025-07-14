using SQLite;

namespace AppCitasMedicasMAUI.Models
{
    public enum RolUsuario
    {
        Administrador,
        Medico,
        Paciente
    }

    [Table("Usuarios")]
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int UsuarioId { get; set; }

        [MaxLength(100), NotNull]
        public string Correo { get; set; }

        [MaxLength(100), NotNull]
        public string Contrasena { get; set; }

        [NotNull]
        public RolUsuario Rol { get; set; }
    }
}
