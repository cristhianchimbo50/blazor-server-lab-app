using LabWebAppBlazor.DTOs;
using LabWebAppBlazor.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;


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

        public async Task<OrdenDetalleDto?> ObtenerDetalleOrdenAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"ordenes/detalle/{id}");

            if (!await AttachTokenAsync(request))
            {
                Console.WriteLine("❌ No se pudo adjuntar el token");
                return null;
            }

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"❌ Error HTTP: {response.StatusCode}");
                return null;
            }

            return await response.Content.ReadFromJsonAsync<OrdenDetalleDto>();
        }



        public async Task<HttpResponseMessage> AnularOrdenAsync(int idOrden)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"ordenes/anular/{idOrden}");

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        // Buscar médicos
        public async Task<List<MedicoDto>> BuscarMedicosAsync(string criterio, string valor)
        {
            var url = $"medicos/buscar?criterio={Uri.EscapeDataString(criterio)}&valor={Uri.EscapeDataString(valor)}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (!await AttachTokenAsync(request))
                return new List<MedicoDto>();

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return new List<MedicoDto>();

            return await response.Content.ReadFromJsonAsync<List<MedicoDto>>() ?? new();
        }

        // Editar médico
        public async Task<HttpResponseMessage> EditarMedicoAsync(int id, MedicoDto medico)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"medicos/{id}")
            {
                Content = JsonContent.Create(medico)
            };

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        // Anular médico
        public async Task<HttpResponseMessage> AnularMedicoAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"medicos/{id}");

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        // Obtener médico por ID
        public async Task<MedicoDto?> ObtenerMedicoPorIdAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"medicos/{id}");

            if (!await AttachTokenAsync(request))
                return null;

            var response = await _http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<MedicoDto>();
        }

        // Resultados
        public async Task<IEnumerable<ResultadoDto>> GetResultadosAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "resultados");

            if (!await AttachTokenAsync(request))
                return Enumerable.Empty<ResultadoDto>();

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<ResultadoDto>();

            return await response.Content.ReadFromJsonAsync<IEnumerable<ResultadoDto>>() ?? [];
        }

        public async Task<ResultadoDetalleDto?> ObtenerDetalleResultadoAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"resultados/detalle/{id}");

            if (!await AttachTokenAsync(request))
                return null;

            var response = await _http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ResultadoDetalleDto>();
        }

        public async Task<HttpResponseMessage> GuardarResultadosAsync(ResultadoGuardarDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "resultados/guardar-completo")
            {
                Content = JsonContent.Create(dto)
            };

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        public async Task<HttpResponseMessage> AnularResultadoAsync(int idResultado)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"resultados/anular/{idResultado}");

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        public async Task<IEnumerable<DetallePagoDto>> GetPagosPorOrdenAsync(int idOrden)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"pagos/orden/{idOrden}");

            if (!await AttachTokenAsync(request))
                return Enumerable.Empty<DetallePagoDto>();

            var response = await _http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<DetallePagoDto>();

            return await response.Content.ReadFromJsonAsync<IEnumerable<DetallePagoDto>>() ?? [];
        }

        public async Task<HttpResponseMessage> RegistrarPagoAsync(PagoDto pago)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "pagos/registrar")
            {
                Content = JsonContent.Create(pago)
            };

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        public async Task<IEnumerable<ReactivoDto>> GetReactivosAsync(string? nombre = null, string? fabricante = null, string? unidad = null)
        {
            var query = new List<string>();
            if (!string.IsNullOrWhiteSpace(nombre))
                query.Add($"nombre={Uri.EscapeDataString(nombre)}");
            if (!string.IsNullOrWhiteSpace(fabricante))
                query.Add($"fabricante={Uri.EscapeDataString(fabricante)}");
            if (!string.IsNullOrWhiteSpace(unidad))
                query.Add($"unidad={Uri.EscapeDataString(unidad)}");

            var url = "reactivos";
            if (query.Any())
                url += "?" + string.Join("&", query);

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (!await AttachTokenAsync(request))
                return Enumerable.Empty<ReactivoDto>();

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<ReactivoDto>();

            return await response.Content.ReadFromJsonAsync<IEnumerable<ReactivoDto>>() ?? [];
        }

        public async Task<IEnumerable<ReactivoDto>> GetReactivosAsync()
        {
            return await GetReactivosAsync(null, null, null);
        }

        public async Task<bool> RegistrarReactivoAsync(ReactivoDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "reactivos/registrar")
            {
                Content = JsonContent.Create(dto)
            };

            if (!await AttachTokenAsync(request))
                return false;

            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EditarReactivoAsync(int id, ReactivoDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"reactivos/{id}")
            {
                Content = JsonContent.Create(dto)
            };

            if (!await AttachTokenAsync(request))
                return false;

            var response = await _http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }


        public async Task<HttpResponseMessage> AnularReactivoAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"reactivos/anular/{id}");

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        public async Task<HttpResponseMessage> RegistrarMovimientosAsync(List<MovimientoReactivoDto> movimientos)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "movimientosreactivos/lote")
            {
                Content = JsonContent.Create(movimientos)
            };

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        public async Task<IEnumerable<ExamenReactivoDto>> ObtenerReactivosPorExamenAsync(int idExamen)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"reactivos/por-examen/{idExamen}");

            if (!await AttachTokenAsync(request))
                return Enumerable.Empty<ExamenReactivoDto>();

            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<ExamenReactivoDto>();

            return await response.Content.ReadFromJsonAsync<IEnumerable<ExamenReactivoDto>>() ?? [];
        }


        public async Task<HttpResponseMessage> GuardarReactivosPorExamenAsync(int idExamen, List<ReactivoAsociadoDto> lista)
        {
            var payload = lista.Select(r => new ExamenReactivoDto
            {
                IdExamen = idExamen,
                IdReactivo = r.IdReactivo,
                NombreReactivo = r.NombreReactivo,
                Unidad = r.Unidad,
                CantidadUsada = r.CantidadUsada
            }).ToList();

            var request = new HttpRequestMessage(HttpMethod.Post, "reactivos/examenes-reactivos")
            {
                Content = JsonContent.Create(payload)
            };

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }



        public async Task<HttpResponseMessage> EliminarReactivoAsociadoAsync(int idAsociacion)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"examenes-reactivos/{idAsociacion}");

            if (!await AttachTokenAsync(request))
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);

            return await _http.SendAsync(request);
        }

        public async Task<List<ExamenDto>> FiltrarExamenesAsync(string criterio, string valor)
        {
            var response = await _http.GetAsync($"api/Examenes/filtrar?{criterio}={valor}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<ExamenDto>>() ?? new();
            return new();
        }

        public async Task<List<ReactivoDto>> FiltrarReactivosAsync(string criterio, string valor)
        {
            var response = await _http.GetAsync($"api/Reactivos/filtrar?{criterio}={valor}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<ReactivoDto>>() ?? new();
            return new();
        }

        public async Task<IEnumerable<AsociacionReactivoDto>> ObtenerTodasLasAsociacionesAsync()
        {
            return await _http.GetFromJsonAsync<List<AsociacionReactivoDto>>("reactivos/asociaciones")
                   ?? new List<AsociacionReactivoDto>();
        }

        public async Task<ExamenDto?> ObtenerExamenPorIdAsync(int id)
        {
            var response = await _http.GetAsync($"examenes/{id}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ExamenDto>();
            return null;
        }

        public async Task<ExamenConReactivosDto?> ObtenerExamenConReactivosAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"examenes/con-reactivos/{id}");

            if (!await AttachTokenAsync(request))
            {
                Console.WriteLine("Token no disponible o inválido para examen con reactivos");
                return null;
            }

            var response = await _http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($" Error HTTP {response.StatusCode} al obtener examen con reactivos. Detalle: {error}");
                return null;
            }

            return await response.Content.ReadFromJsonAsync<ExamenConReactivosDto>();
        }



    }
}
