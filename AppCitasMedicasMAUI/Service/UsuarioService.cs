using AppCitasMedicasMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppCitasMedicasMAUI.Service
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService()
        
{
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7062");
        }

        public async Task<Usuario> LoginAsync(string correo, string contrasena)
        {
            var request = new
            {
                Correo = correo,
                Contrasena = contrasena
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("usuario/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var usuario = JsonSerializer.Deserialize<Usuario>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return usuario;
            }

            return null;
        }
    }
}
