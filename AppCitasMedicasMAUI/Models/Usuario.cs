using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCitasMedicasMAUI.Models
{
    public enum RolUsuario
    {
        Administrador,
        Medico,
        Paciente
    }

    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public RolUsuario Rol { get; set; }
    }
}
