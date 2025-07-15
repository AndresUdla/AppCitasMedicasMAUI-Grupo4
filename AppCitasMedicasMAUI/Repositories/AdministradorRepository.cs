using AppCitasMedicasMAUI.Models;
using SQLite;

namespace AppCitasMedicasMAUI.Repositories
{
    public class AdministradorRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public AdministradorRepository(SQLiteAsyncConnection db)
        {
            _db = db;
            _db.CreateTableAsync<Administrador>().Wait();
        }

        public Task<List<Administrador>> GetAllAsync()
        {
            return _db.Table<Administrador>().ToListAsync();
        }

        public Task<Administrador?> GetByIdAsync(int id)
        {
            return _db.Table<Administrador>().Where(a => a.AdministradorId == id).FirstOrDefaultAsync();
        }

        public Task<int> InsertAsync(Administrador administrador)
        {
            return _db.InsertAsync(administrador);
        }

        public Task<int> UpdateAsync(Administrador administrador)
        {
            return _db.UpdateAsync(administrador);
        }

        public Task<int> DeleteAsync(Administrador administrador)
        {
            return _db.DeleteAsync(administrador);
        }
    }
}
