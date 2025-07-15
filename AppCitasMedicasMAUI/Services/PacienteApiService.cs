using AppCitasMedicasMAUI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppCitasMedicasMAUI.Services
{
    public class PacienteApiService
    {
        private readonly HttpClient _httpClient;

        public PacienteApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/Paciente
        public async Task<List<Paciente>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Paciente>>("api/Paciente");
        }

        // GET: api/Paciente/5
        public async Task<Paciente?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Paciente>($"api/Paciente/{id}");
        }

        // POST: api/Paciente
        public async Task<Paciente?> CreateAsync(Paciente paciente)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Paciente", paciente);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Paciente>();
            }
            return null;
        }

        // PUT: api/Paciente/5
        public async Task<bool> UpdateAsync(int id, Paciente paciente)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Paciente/{id}", paciente);
            return response.IsSuccessStatusCode;
        }

        // DELETE: api/Paciente/5
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Paciente/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
