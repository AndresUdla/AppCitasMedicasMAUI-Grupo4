using AppCitasMedicasMAUI.Models;
using SQLite;

namespace AppCitasMedicasMAUI.Repositories
{
    public class CitaRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public CitaRepository(SQLiteAsyncConnection db)
        {
            _db = db;
            _db.CreateTableAsync<Cita>().Wait();
        }

        public Task<List<Cita>> GetAllAsync()
        {
            return _db.Table<Cita>().ToListAsync();
        }

        public Task<Cita?> GetByIdAsync(int id)
        {
            return _db.Table<Cita>().Where(c => c.CitaId == id).FirstOrDefaultAsync();
        }

        public Task<int> InsertAsync(Cita cita)
        {
            return _db.InsertAsync(cita);
        }

        public Task<int> UpdateAsync(Cita cita)
        {
            return _db.UpdateAsync(cita);
        }

        public Task<int> DeleteAsync(Cita cita)
        {
            return _db.DeleteAsync(cita);
        }
    }
}
