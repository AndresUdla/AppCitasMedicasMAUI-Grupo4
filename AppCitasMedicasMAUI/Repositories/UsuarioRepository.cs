using AppCitasMedicasMAUI.Models;
using SQLite;

namespace AppCitasMedicasMAUI.Repositories
{
    public class UsuarioRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public UsuarioRepository(SQLiteAsyncConnection db)
        {
            _db = db;
            _db.CreateTableAsync<Usuario>().Wait();
        }

        public Task<List<Usuario>> GetAllAsync()
        {
            return _db.Table<Usuario>().ToListAsync();
        }

        public Task<Usuario?> GetByIdAsync(int id)
        {
            return _db.Table<Usuario>().Where(u => u.UsuarioId == id).FirstOrDefaultAsync();
        }

        public Task<Usuario?> GetByCorreoYContrasenaAsync(string correo, string contrasena)
        {
            return _db.Table<Usuario>()
                .Where(u => u.Correo == correo && u.Contrasena == contrasena)
                .FirstOrDefaultAsync();
        }

        public Task<int> InsertAsync(Usuario usuario)
        {
            return _db.InsertAsync(usuario);
        }

        public Task<int> UpdateAsync(Usuario usuario)
        {
            return _db.UpdateAsync(usuario);
        }

        public Task<int> DeleteAsync(Usuario usuario)
        {
            return _db.DeleteAsync(usuario);
        }
    }
}
