using System.Net.Http.Json;
using LabWebAppBlazor.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace LabWebAppBlazor.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _http;
        private readonly ProtectedSessionStorage _sessionStorage;

        public ApiService(HttpClient http, ProtectedSessionStorage sessionStorage)
        {
            _http = http;
            _sessionStorage = sessionStorage;
        }

        public async Task<IEnumerable<PacienteDto>> GetPacientesAsync()
        {
            await AddJwtTokenAsync();
            return await _http.GetFromJsonAsync<IEnumerable<PacienteDto>>("pacientes");
        }


        public async Task<HttpResponseMessage> CrearPacienteAsync(PacienteDto paciente)
        {
            var response = await _http.PostAsJsonAsync("pacientes", paciente);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"STATUS: {response.StatusCode}");
            Console.WriteLine($"BODY: {content}");
            return response;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var response = await _http.PostAsJsonAsync("usuarios/login", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Login fallido: {error}");
                return null;
            }

            return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        }

        private async Task AddJwtTokenAsync()
        {
            var tokenResult = await _sessionStorage.GetAsync<LoginResponseDto>("authToken");

            if (tokenResult.Success && tokenResult.Value is not null)
            {
                Console.WriteLine("Authorization header actual: " + _http.DefaultRequestHeaders.Authorization);

                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResult.Value.Token);
            }
        }


        public async Task<bool> VerificarTokenAsync()
        {
            await AddJwtTokenAsync();
            var response = await _http.GetAsync("usuarios/verificar-token");
            return response.IsSuccessStatusCode;
        }

    }
}