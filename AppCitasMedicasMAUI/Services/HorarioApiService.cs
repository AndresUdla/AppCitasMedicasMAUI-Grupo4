using AppCitasMedicasMAUI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppCitasMedicasMAUI.Services
{
    public class HorarioApiService
    {
        private readonly HttpClient _httpClient;

        public HorarioApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/Horario
        public async Task<List<Horario>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Horario>>("api/Horario");
        }

        // GET: api/Horario/5
        public async Task<Horario?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Horario>($"api/Horario/{id}");
        }

        // POST: api/Horario
        public async Task<Horario?> CreateAsync(Horario horario)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Horario", horario);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Horario>();
            }
            return null;
        }

        // PUT: api/Horario/5
        public async Task<bool> UpdateAsync(int id, Horario horario)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Horario/{id}", horario);
            return response.IsSuccessStatusCode;
        }

        // DELETE: api/Horario/5
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Horario/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
