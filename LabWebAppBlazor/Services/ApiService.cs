using System.Net.Http.Json;
using LabWebAppBlazor.Models;

namespace LabWebAppBlazor.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<PacienteDto>> GetPacientesAsync()
        {
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
    }
}
