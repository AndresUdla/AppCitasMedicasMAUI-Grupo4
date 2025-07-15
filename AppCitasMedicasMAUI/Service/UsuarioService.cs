using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCitasMedicasMAUI.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService()
        {
            _usuarioRepository = DatabaseService.UsuarioRepo;
        }

        public Task<Usuario> LoginAsync(string correo, string contrasena)
        {
            return _usuarioRepository.IniciarSesionAsync(correo, contrasena);
        }

        public Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            return _usuarioRepository.ObtenerTodosAsync();
        }

        public Task<int> InsertarUsuarioAsync(Usuario usuario)
        {
            return _usuarioRepository.InsertarAsync(usuario);
        }

        public Task<int> ActualizarUsuarioAsync(Usuario usuario)
        {
            return _usuarioRepository.ActualizarAsync(usuario);
        }

        public Task<int> EliminarUsuarioAsync(Usuario usuario)
        {
            return _usuarioRepository.EliminarAsync(usuario);
        }
    }
}
