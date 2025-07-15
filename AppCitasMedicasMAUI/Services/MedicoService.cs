using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Repositories;

namespace AppCitasMedicasMAUI.Services
{
    public class MedicoService
    {
        private readonly MedicoRepository _medicoRepository;

        public MedicoService(MedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public Task<List<Medico>> ObtenerTodosAsync()
        {
            return _medicoRepository.GetAllAsync();
        }

        public Task<Medico?> ObtenerPorIdAsync(int id)
        {
            return _medicoRepository.GetByIdAsync(id);
        }

        public Task<int> CrearAsync(Medico medico)
        {
            return _medicoRepository.InsertAsync(medico);
        }

        public Task<int> ActualizarAsync(Medico medico)
        {
            return _medicoRepository.UpdateAsync(medico);
        }

        public Task<int> EliminarAsync(Medico medico)
        {
            return _medicoRepository.DeleteAsync(medico);
        }
    }
}
