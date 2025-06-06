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

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

            var response = await _httpClient.PostAsync("api/usuario/login", content);

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

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/usuario");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var usuarios = JsonSerializer.Deserialize<List<Usuario>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return usuarios ?? new List<Usuario>();
                }
            }
            catch (Exception ex)
            {
                
            }

            return new List<Usuario>();
        }

    }
}
