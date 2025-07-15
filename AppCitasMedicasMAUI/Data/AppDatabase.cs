using AppCitasMedicasMAUI.Models;
using AppCitasMedicasMAUI.Repositories;
using SQLite;

namespace AppCitasMedicasMAUI.Data
{
    public class AppDatabase
    {
        private static SQLiteAsyncConnection _database;

        public UsuarioRepository UsuarioRepository { get; private set; }
        public AdministradorRepository AdministradorRepository { get; private set; }
        public PacienteRepository PacienteRepository { get; private set; }
        public MedicoRepository MedicoRepository { get; private set; }
        public HorarioRepository HorarioRepository { get; private set; }
        public CitaRepository CitaRepository { get; private set; }

        public static async Task<AppDatabase> CreateAsync(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            var appDb = new AppDatabase();
            await appDb.InitializeRepositoriesAsync();

            return appDb;
        }

        private async Task InitializeRepositoriesAsync()
        {
            await _database.CreateTableAsync<Usuario>();
            await _database.CreateTableAsync<Administrador>();
            await _database.CreateTableAsync<Paciente>();
            await _database.CreateTableAsync<Medico>();
            await _database.CreateTableAsync<Horario>();
            await _database.CreateTableAsync<Cita>();

            UsuarioRepository = new UsuarioRepository(_database);
            AdministradorRepository = new AdministradorRepository(_database);
            PacienteRepository = new PacienteRepository(_database);
            MedicoRepository = new MedicoRepository(_database);
            HorarioRepository = new HorarioRepository(_database);
            CitaRepository = new CitaRepository(_database);
        }
    }
}
