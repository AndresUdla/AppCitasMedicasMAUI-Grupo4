using AppCitasMedicasMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AppCitasMedicasMAUI.Service 
{
    public class PacienteService
    {
        private readonly HttpClient _httpClient;

        public PacienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // No necesitamos _httpClient.BaseAddress aquí porque ya lo configuraste en MauiProgram.cs
        }

        public async Task<List<Paciente>> ObtenerPacientesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Paciente>>("api/Paciente");
        }

        public async Task<Paciente> ObtenerPacientePorIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Paciente>($"api/Paciente/{id}");
        }

        public async Task<bool> CrearPacienteAsync(Paciente paciente)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Paciente", paciente);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarPacienteAsync(int id, Paciente paciente)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Paciente/{id}", paciente);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarPacienteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Paciente/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
