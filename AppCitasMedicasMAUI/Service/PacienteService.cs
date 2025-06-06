using AppCitasMedicasMAUI.Models;
using System.Net.Http.Json;

namespace AppCitasMedicasMAUI.Services
{
    public class PacienteService
    {
        private readonly HttpClient _httpClient;

        public PacienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Paciente>> GetPacientesAsync()
        {
            try
            {
                var pacientes = await _httpClient.GetFromJsonAsync<List<Paciente>>("api/Paciente");
                Console.WriteLine($"Pacientes recibidos: {pacientes?.Count}");
                return pacientes ?? new List<Paciente>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener pacientes: {ex.Message}");
                return new List<Paciente>();
            }
        }

        public async Task<Paciente?> GetPacienteByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<Paciente>($"api/Paciente/{id}");

        public async Task<bool> CreatePacienteAsync(Paciente paciente)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Paciente", paciente);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePacienteAsync(int id, Paciente paciente)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Paciente/{id}", paciente);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePacienteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Paciente/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
