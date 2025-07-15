using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Repositories;
using SQLite;
using System.IO;
using System.Threading.Tasks;

namespace AppCitasMedicasMAUI.Services
{
    public class DatabaseService
    {
        private static SQLiteAsyncConnection _database;
        private static UsuarioRepository _usuarioRepository;

        public static async Task InicializarAsync()
        {
            if (_database != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "CitasMedicas.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            // Inicializar las tablas necesarias
            await _database.CreateTableAsync<Usuario>();

            // Inicializar los repositorios
            _usuarioRepository = new UsuarioRepository(_database);
        }

        public static UsuarioRepository UsuarioRepo
        {
            get
            {
                return _usuarioRepository;
            }
        }
    }
}
