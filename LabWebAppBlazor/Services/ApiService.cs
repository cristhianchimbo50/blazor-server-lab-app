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
                Console.WriteLine("⚠️ No se pudo agregar el token. Abandonando llamada.");
                return Enumerable.Empty<PacienteDto>();
            }

            Console.WriteLine("📡 GET /pacientes con token");
            Console.WriteLine("🔐 Header Authorization: " + request.Headers.Authorization);

            var response = await _http.SendAsync(request);
            Console.WriteLine($"📬 Código respuesta: {(int)response.StatusCode} {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine("❌ Cuerpo de error: " + body);
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
                Console.WriteLine("⚠️ No se pudo agregar token en POST.");
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }

            var response = await _http.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"📤 POST pacientes -> STATUS: {response.StatusCode}");
            Console.WriteLine($"📤 BODY: {content}");

            return response;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var response = await _http.PostAsJsonAsync("usuarios/login", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ Login fallido: {error}");
                return null;
            }

            return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        }

        public async Task<bool> VerificarTokenAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "usuarios/verificar-token");

            if (!await AttachTokenAsync(request))
            {
                Console.WriteLine("⚠️ Token no válido para verificar.");
                return false;
            }

            var response = await _http.SendAsync(request);
            Console.WriteLine($"🔍 Verificando token -> {response.StatusCode}");
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
                Console.WriteLine("❌ Token inválido o ausente en sesión.");
                return false;
            }

            var token = tokenResult.Value.Token;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Console.WriteLine("✅ Token adjuntado correctamente.");
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




    }
}
