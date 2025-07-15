using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Repositories;

namespace AppCitasMedicasMAUI.Services
{
    public class CitaService
    {
        private readonly CitaRepository _citaRepository;

        public CitaService(CitaRepository citaRepository)
        {
            _citaRepository = citaRepository;
        }

        public Task<List<Cita>> ObtenerTodasAsync()
        {
            return _citaRepository.GetAllAsync();
        }

        public Task<Cita?> ObtenerPorIdAsync(int id)
        {
            return _citaRepository.GetByIdAsync(id);
        }

        public Task<int> CrearAsync(Cita cita)
        {
            return _citaRepository.InsertAsync(cita);
        }

        public Task<int> ActualizarAsync(Cita cita)
        {
            return _citaRepository.UpdateAsync(cita);
        }

        public Task<int> EliminarAsync(Cita cita)
        {
            return _citaRepository.DeleteAsync(cita);
        }
    }
}
