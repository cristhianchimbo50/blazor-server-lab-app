using System.Net.Http.Json;
using LabWebAppBlazor.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;

namespace LabWebAppBlazor.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _http;
        private readonly ProtectedSessionStorage _sessionStorage;

        public ApiService(IHttpClientFactory factory, ProtectedSessionStorage sessionStorage)
        {
            _http = factory.CreateClient("Api");
            _sessionStorage = sessionStorage;
        }

        public async Task<IEnumerable<PacienteDto>> GetPacientesAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "pacientes");

            if (!await AttachTokenAsync(request))
            {
                return Enumerable.Empty<PacienteDto>();
            }


            var response = await _http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return Enumerable.Empty<PacienteDto>();
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<PacienteDto>>() ?? [];
        }

        public async Task<HttpResponseMessage> CrearPacienteAsync(PacienteDto paciente)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "pacientes")
            {
                Content = JsonContent.Create(paciente)
            };

            if (!await AttachTokenAsync(request))
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }

            var response = await _http.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return response;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var response = await _http.PostAsJsonAsync("usuarios/login", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return null;
            }

            return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        }

        public async Task<bool> VerificarTokenAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "usuarios/verificar-token");

            if (!await AttachTokenAsync(request))
            {
                return false;
            }

            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Adjunta el token JWT si está disponible. Devuelve false si no lo está.
        /// </summary>
        private async Task<bool> AttachTokenAsync(HttpRequestMessage request)
        {
            var tokenResult = await _sessionStorage.GetAsync<LoginResponseDto>("authToken");

            if (!tokenResult.Success || tokenResult.Value == null || string.IsNullOrWhiteSpace(tokenResult.Value.Token))
            {
                return false;
            }

            var token = tokenResult.Value.Token;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }

        public async Task<HttpResponseMessage> RegistrarUsuarioAsync(CrearUsuarioDto nuevoUsuario)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "usuarios/registrar")
            {
                Content = JsonContent.Create(nuevoUsuario)
            };

            if (!await AttachTokenAsync(request))
            {
                Console.WriteLine("No se pudo adjuntar token para registrar usuario.");
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }

            var response = await _http.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"POST usuarios/registrar -> STATUS: {response.StatusCode}");
            Console.WriteLine($"BODY: {body}");

            return response;
        }

        public async Task<HttpResponseMessage> CambiarClaveAsync(CambiarClaveDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "usuarios/cambiar-clave")
            {
                Content = JsonContent.Create(dto)
            };

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }


        public async Task<IEnumerable<PacienteDto>> BuscarPacientesAsync(string campo, string valor)
        {
            var url = $"pacientes/buscar?campo={Uri.EscapeDataString(campo)}&valor={Uri.EscapeDataString(valor)}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (!await AttachTokenAsync(request))
                return Enumerable.Empty<PacienteDto>();

            var response = await _http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<PacienteDto>();

            return await response.Content.ReadFromJsonAsync<IEnumerable<PacienteDto>>() ?? [];
        }

        public async Task<HttpResponseMessage> AnularPacienteAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"pacientes/anular/{id}");

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        public async Task<HttpResponseMessage> EditarPacienteAsync(int id, PacienteDto paciente)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"pacientes/{id}")
            {
                Content = JsonContent.Create(paciente)
            };

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        //Orden
        public async Task<IEnumerable<OrdenDto>> GetOrdenesAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "ordenes");

            if (!await AttachTokenAsync(request))
                return Enumerable.Empty<OrdenDto>();

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<OrdenDto>();

            return await response.Content.ReadFromJsonAsync<IEnumerable<OrdenDto>>() ?? [];
        }


    }
}
