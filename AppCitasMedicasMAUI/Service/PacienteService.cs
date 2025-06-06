using AppCitasMedicasMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AppCitasMedicasMAUI.Service
{
    internal class PacienteService
    {
        private readonly HttpClient _httpClient;

        public PacienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7062");
        }

        public async Task<List<Paciente>> ObtenerPacientesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Paciente>>("Paciente");
        }

        public async Task<Paciente> ObtenerPacientePorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Paciente>($"api/Paciente/{id}");
        }

        public async Task<bool> CrearPacienteAsync(Paciente paciente)
        {
            var response = await _httpClient.PostAsJsonAsync("Paciente", paciente);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarPacienteAsync(int id, Paciente paciente)
        {
            var response = await _httpClient.PutAsJsonAsync($"Paciente/{id}", paciente);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarPacienteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Paciente/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
