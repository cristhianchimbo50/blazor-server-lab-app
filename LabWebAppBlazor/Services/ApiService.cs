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

        //buscar paciente por cedula
        public async Task<PacienteDto?> ObtenerPacientePorCedulaAsync(string cedula)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"pacientes/buscar?campo=cedula&valor={cedula}");

            if (!await AttachTokenAsync(request))
                return null;

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;

            var lista = await response.Content.ReadFromJsonAsync<List<PacienteDto>>();
            return lista?.FirstOrDefault();
        }

        // EXÁMENES

        public async Task<IEnumerable<ExamenDto>> GetExamenesAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "examenes");

            if (!await AttachTokenAsync(request))
                return Enumerable.Empty<ExamenDto>();

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<ExamenDto>();

            return await response.Content.ReadFromJsonAsync<IEnumerable<ExamenDto>>() ?? [];
        }

        public async Task<ExamenDto?> GetExamenByIdAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"examenes/{id}");

            if (!await AttachTokenAsync(request))
                return null;

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ExamenDto>();
        }

        public async Task<HttpResponseMessage> CrearExamenAsync(ExamenDto examen)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "examenes")
            {
                Content = JsonContent.Create(examen)
            };

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        public async Task<HttpResponseMessage> EditarExamenAsync(int id, ExamenDto examen)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"examenes/{id}")
            {
                Content = JsonContent.Create(examen)
            };

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        public async Task<HttpResponseMessage> AnularExamenAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"examenes/anular/{id}");

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        // Crear examen con hijos
        public async Task<HttpResponseMessage> CrearExamenConHijosAsync(ExamenConComposicionDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "examenes/con-hijos")
            {
                Content = JsonContent.Create(dto)
            };

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        // Obtener hijos de un examen
        public async Task<IEnumerable<ExamenDto>> ObtenerHijosDeExamenAsync(int idPadre)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"ExamenComposicion/padre/{idPadre}");

            if (!await AttachTokenAsync(request))
                return Enumerable.Empty<ExamenDto>();

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<ExamenDto>();

            return await response.Content.ReadFromJsonAsync<IEnumerable<ExamenDto>>() ?? [];
        }

        // Agregar hijo (relación padre-hijo)
        public async Task<HttpResponseMessage> AgregarExamenHijoAsync(int idPadre, int idHijo)
        {
            var composicion = new ExamenComposicionDto
            {
                IdExamenPadre = idPadre,
                IdExamenHijo = idHijo
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "ExamenComposicion")
            {
                Content = JsonContent.Create(composicion)
            };

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        // Eliminar hijo (relación padre-hijo)
        public async Task<HttpResponseMessage> EliminarExamenHijoAsync(int idPadre, int idHijo)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"ExamenComposicion/padre/{idPadre}/hijo/{idHijo}");

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        public async Task<HttpResponseMessage> RegistrarOrdenCompletaAsync(OrdenCompletaDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "ordenes/completa")
            {
                Content = JsonContent.Create(dto)
            };

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        public async Task<IEnumerable<MedicoDto>> GetMedicosAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "medicos");

            if (!await AttachTokenAsync(request))
                return Enumerable.Empty<MedicoDto>();

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<MedicoDto>();

            return await response.Content.ReadFromJsonAsync<IEnumerable<MedicoDto>>() ?? [];
        }

        public async Task<HttpResponseMessage> CrearMedicoAsync(MedicoDto medico)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "medicos")
            {
                Content = JsonContent.Create(medico)
            };

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        public async Task<IEnumerable<ExamenDto>> BuscarExamenesAsync(string campo, string valor)
        {
            var url = $"examenes/buscar?campo={Uri.EscapeDataString(campo)}&valor={Uri.EscapeDataString(valor)}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (!await AttachTokenAsync(request))
                return Enumerable.Empty<ExamenDto>();

            var response = await _http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<ExamenDto>();

            return await response.Content.ReadFromJsonAsync<IEnumerable<ExamenDto>>() ?? [];
        }

       




    }
}
