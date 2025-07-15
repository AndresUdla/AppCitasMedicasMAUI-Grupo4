using AppCitasMedicasMAUI.Models;
using SQLite;

namespace AppCitasMedicasMAUI.Repositories
{
    public class MedicoRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public MedicoRepository(SQLiteAsyncConnection db)
        {
            _db = db;
            _db.CreateTableAsync<Medico>().Wait();
        }

        public Task<List<Medico>> GetAllAsync()
        {
            return _db.Table<Medico>().ToListAsync();
        }

        public Task<Medico?> GetByIdAsync(int id)
        {
            return _db.Table<Medico>().Where(m => m.MedicoId == id).FirstOrDefaultAsync();
        }

        public Task<int> InsertAsync(Medico medico)
        {
            return _db.InsertAsync(medico);
        }

        public Task<int> UpdateAsync(Medico medico)
        {
            return _db.UpdateAsync(medico);
        }

        public Task<int> DeleteAsync(Medico medico)
        {
            return _db.DeleteAsync(medico);
        }
    }
}
    