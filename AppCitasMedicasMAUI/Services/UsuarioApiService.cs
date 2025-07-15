using AppCitasMedicasMAUI.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppCitasMedicasMAUI.Services
{
    public class UsuarioApiService
    {
        private readonly HttpClient _httpClient;

        public UsuarioApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/Usuario
        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Usuario>>("api/Usuario");
        }

        // GET: api/Usuario/5
        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Usuario>($"api/Usuario/{id}");
        }

        // POST: api/Usuario
        public async Task<Usuario?> CreateAsync(Usuario usuario)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Usuario", usuario);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>();
            }
            return null;
        }

        // PUT: api/Usuario/5
        public async Task<bool> UpdateAsync(int id, Usuario usuario)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Usuario/{id}", usuario);
            return response.IsSuccessStatusCode;
        }

        // DELETE: api/Usuario/5
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Usuario/{id}");
            return response.IsSuccessStatusCode;
        }

        // POST: api/Usuario/login
        public async Task<Usuario?> LoginAsync(string correo, string contrasena)
        {
            var loginRequest = new { Correo = correo, Contrasena = contrasena };
            var response = await _httpClient.PostAsJsonAsync("api/Usuario/login", loginRequest);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>();
            }
            return null;
        }
    }
}
