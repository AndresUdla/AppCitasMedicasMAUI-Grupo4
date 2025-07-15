using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Repositories;

namespace AppCitasMedicasMAUI.Services
{
    public class PacienteService
    {
        private readonly PacienteRepository _pacienteRepository;

        public PacienteService(PacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public Task<List<Paciente>> ObtenerTodosAsync()
        {
            return _pacienteRepository.GetAllAsync();
        }

        public Task<Paciente?> ObtenerPorIdAsync(int id)
        {
            return _pacienteRepository.GetByIdAsync(id);
        }

        public Task<int> CrearAsync(Paciente paciente)
        {
            return _pacienteRepository.InsertAsync(paciente);
        }

        public Task<int> ActualizarAsync(Paciente paciente)
        {
            return _pacienteRepository.UpdateAsync(paciente);
        }

        public Task<int> EliminarAsync(Paciente paciente)
        {
            return _pacienteRepository.DeleteAsync(paciente);
        }
    }
}
