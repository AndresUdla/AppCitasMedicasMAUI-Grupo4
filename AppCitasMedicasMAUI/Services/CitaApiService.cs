using AppCitasMedicasMAUI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppCitasMedicasMAUI.Services
{
    public class CitaApiService
    {
        private readonly HttpClient _httpClient;

        public CitaApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/Cita
        public async Task<List<Cita>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Cita>>("api/Cita");
        }

        // GET: api/Cita/5
        public async Task<Cita?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Cita>($"api/Cita/{id}");
        }

        // POST: api/Cita
        public async Task<Cita?> CreateAsync(Cita cita)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Cita", cita);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Cita>();
            }
            return null;
        }

        // PUT: api/Cita/5
        public async Task<bool> UpdateAsync(int id, Cita cita)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Cita/{id}", cita);
            return response.IsSuccessStatusCode;
        }

        // DELETE: api/Cita/5
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Cita/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
