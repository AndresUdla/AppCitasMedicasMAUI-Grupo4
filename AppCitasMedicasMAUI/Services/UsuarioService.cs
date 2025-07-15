using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Repositories;

namespace AppCitasMedicasMAUI.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Task<List<Usuario>> ObtenerTodosAsync()
        {
            return _usuarioRepository.GetAllAsync();
        }

        public Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            return _usuarioRepository.GetByIdAsync(id);
        }

        public Task<Usuario?> IniciarSesionAsync(string correo, string contrasena)
        {
            return _usuarioRepository.GetByCorreoYContrasenaAsync(correo, contrasena);
        }

        public Task<int> CrearAsync(Usuario usuario)
        {
            return _usuarioRepository.InsertAsync(usuario);
        }

        public Task<int> ActualizarAsync(Usuario usuario)
        {
            return _usuarioRepository.UpdateAsync(usuario);
        }

        public Task<int> EliminarAsync(Usuario usuario)
        {
            return _usuarioRepository.DeleteAsync(usuario);
        }
    }
}
