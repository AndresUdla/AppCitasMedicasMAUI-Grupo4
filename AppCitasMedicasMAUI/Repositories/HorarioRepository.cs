using AppCitasMedicasMAUI.Models;
using SQLite;

namespace AppCitasMedicasMAUI.Repositories
{
    public class HorarioRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public HorarioRepository(SQLiteAsyncConnection db)
        {
            _db = db;
            _db.CreateTableAsync<Horario>().Wait();
        }

        public Task<List<Horario>> GetAllAsync()
        {
            return _db.Table<Horario>().ToListAsync();
        }

        public Task<Horario?> GetByIdAsync(int id)
        {
            return _db.Table<Horario>().Where(h => h.HorarioId == id).FirstOrDefaultAsync();
        }

        public Task<int> InsertAsync(Horario horario)
        {
            return _db.InsertAsync(horario);
        }

        public Task<int> UpdateAsync(Horario horario)
        {
            return _db.UpdateAsync(horario);
        }

        public Task<int> DeleteAsync(Horario horario)
        {
            return _db.DeleteAsync(horario);
        }
    }
}
