using AppCitasMedicasMAUI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppCitasMedicasMAUI.Services
{
    public class MedicoApiService
    {
        private readonly HttpClient _httpClient;

        public MedicoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/Medico
        public async Task<List<Medico>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Medico>>("api/Medico");
        }

        // GET: api/Medico/5
        public async Task<Medico?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Medico>($"api/Medico/{id}");
        }

        // POST: api/Medico
        public async Task<Medico?> CreateAsync(Medico medico)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Medico", medico);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Medico>();
            }
            return null;
        }

        // PUT: api/Medico/5
        public async Task<bool> UpdateAsync(int id, Medico medico)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Medico/{id}", medico);
            return response.IsSuccessStatusCode;
        }

        // DELETE: api/Medico/5
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Medico/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
