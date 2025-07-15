using AppCitasMedicasMAUI.Models;
using SQLite;

namespace AppCitasMedicasMAUI.Repositories
{
    public class PacienteRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public PacienteRepository(SQLiteAsyncConnection db)
        {
            _db = db;
            _db.CreateTableAsync<Paciente>().Wait();
        }

        public Task<List<Paciente>> GetAllAsync()
        {
            return _db.Table<Paciente>().ToListAsync();
        }

        public Task<Paciente?> GetByIdAsync(int id)
        {
            return _db.Table<Paciente>().Where(p => p.PacienteId == id).FirstOrDefaultAsync();
        }

        public Task<int> InsertAsync(Paciente paciente)
        {
            return _db.InsertAsync(paciente);
        }

        public Task<int> UpdateAsync(Paciente paciente)
        {
            return _db.UpdateAsync(paciente);
        }

        public Task<int> DeleteAsync(Paciente paciente)
        {
            return _db.DeleteAsync(paciente);
        }
    }
}
