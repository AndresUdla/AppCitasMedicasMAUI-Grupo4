using AppCitasMedicasMAUI.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCitasMedicasMAUI.Repositories
{
    public class UsuarioRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public UsuarioRepository(SQLiteAsyncConnection database)
        {
            _database = database;
            _database.CreateTableAsync<Usuario>().Wait();
        }

        public Task<List<Usuario>> ObtenerTodosAsync()
        {
            return _database.Table<Usuario>().ToListAsync();
        }

        public Task<Usuario> ObtenerPorIdAsync(int id)
        {
            return _database.Table<Usuario>()
                            .Where(u => u.UsuarioId == id)
                            .FirstOrDefaultAsync();
        }

        public Task<Usuario> IniciarSesionAsync(string correo, string contrasena)
        {
            return _database.Table<Usuario>()
                            .Where(u => u.Correo == correo && u.Contrasena == contrasena)
                            .FirstOrDefaultAsync();
        }

        public Task<int> InsertarAsync(Usuario usuario)
        {
            return _database.InsertAsync(usuario);
        }

        public Task<int> ActualizarAsync(Usuario usuario)
        {
            return _database.UpdateAsync(usuario);
        }

        public Task<int> EliminarAsync(Usuario usuario)
        {
            return _database.DeleteAsync(usuario);
        }
    }
}
