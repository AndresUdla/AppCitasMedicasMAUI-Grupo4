using AppCitasMedicasMAUI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppCitasMedicasMAUI.Services
{
    public class AdministradorApiService
    {
        private readonly HttpClient _httpClient;

        public AdministradorApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/Administrador
        public async Task<List<Administrador>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Administrador>>("api/Administrador");
        }

        // GET: api/Administrador/5
        public async Task<Administrador?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Administrador>($"api/Administrador/{id}");
        }

        // POST: api/Administrador
        public async Task<Administrador?> CreateAsync(Administrador administrador)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Administrador", administrador);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Administrador>();
            }
            return null;
        }

        // PUT: api/Administrador/5
        public async Task<bool> UpdateAsync(int id, Administrador administrador)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Administrador/{id}", administrador);
            return response.IsSuccessStatusCode;
        }

        // DELETE: api/Administrador/5
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Administrador/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
