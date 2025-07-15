using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Repositories;

namespace AppCitasMedicasMAUI.Services
{
    public class HorarioService
    {
        private readonly HorarioRepository _horarioRepository;

        public HorarioService(HorarioRepository horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        public Task<List<Horario>> ObtenerTodosAsync()
        {
            return _horarioRepository.GetAllAsync();
        }

        public Task<Horario?> ObtenerPorIdAsync(int id)
        {
            return _horarioRepository.GetByIdAsync(id);
        }

        public Task<int> CrearAsync(Horario horario)
        {
            return _horarioRepository.InsertAsync(horario);
        }

        public Task<int> ActualizarAsync(Horario horario)
        {
            return _horarioRepository.UpdateAsync(horario);
        }

        public Task<int> EliminarAsync(Horario horario)
        {
            return _horarioRepository.DeleteAsync(horario);
        }
    }
}
