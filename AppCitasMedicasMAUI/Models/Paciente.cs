using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCitasMedicasMAUI.Models
{
    public class Paciente
    {
        public int PacienteId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public int Edad { get; set; }
        public double? Altura { get; set; }
        public double? Peso { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int UsuarioId { get; set; }
    }
}
