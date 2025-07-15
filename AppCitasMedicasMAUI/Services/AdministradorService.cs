using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Repositories;

namespace AppCitasMedicasMAUI.Services
{
    public class AdministradorService
    {
        private readonly AdministradorRepository _administradorRepository;

        public AdministradorService(AdministradorRepository administradorRepository)
        {
            _administradorRepository = administradorRepository;
        }

        public Task<List<Administrador>> ObtenerTodosAsync()
        {
            return _administradorRepository.GetAllAsync();
        }

        public Task<Administrador?> ObtenerPorIdAsync(int id)
        {
            return _administradorRepository.GetByIdAsync(id);
        }

        public Task<int> CrearAsync(Administrador administrador)
        {
            return _administradorRepository.InsertAsync(administrador);
        }

        public Task<int> ActualizarAsync(Administrador administrador)
        {
            return _administradorRepository.UpdateAsync(administrador);
        }

        public Task<int> EliminarAsync(Administrador administrador)
        {
            return _administradorRepository.DeleteAsync(administrador);
        }
    }
}
