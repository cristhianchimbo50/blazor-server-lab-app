﻿using LabWebAppBlazor.Models;

namespace LabWebAppBlazor.Services
{
    public interface IApiService
    {
        Task<IEnumerable<PacienteDto>> GetPacientesAsync();
        Task<HttpResponseMessage> CrearPacienteAsync(PacienteDto paciente);
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);

        Task<bool> VerificarTokenAsync();

        Task<HttpResponseMessage> RegistrarUsuarioAsync(CrearUsuarioDto nuevoUsuario);

        Task<HttpResponseMessage> CambiarClaveAsync(CambiarClaveDto modelo);

        Task<IEnumerable<PacienteDto>> BuscarPacientesAsync(string campo, string valor);

        Task<HttpResponseMessage> AnularPacienteAsync(int id);

        Task<HttpResponseMessage> EditarPacienteAsync(int id, PacienteDto paciente);


        //Orden
        Task<IEnumerable<OrdenDto>> GetOrdenesAsync();

        Task<PacienteDto?> ObtenerPacientePorCedulaAsync(string cedula);

        //examenes
        // Exámenes
        Task<IEnumerable<ExamenDto>> GetExamenesAsync();
        Task<ExamenDto?> GetExamenByIdAsync(int id);
        Task<HttpResponseMessage> CrearExamenAsync(ExamenDto examen);
        Task<HttpResponseMessage> EditarExamenAsync(int id, ExamenDto examen);
        Task<HttpResponseMessage> AnularExamenAsync(int id);

        // Exámenes con composición
        Task<HttpResponseMessage> CrearExamenConHijosAsync(ExamenConComposicionDto dto);
        Task<IEnumerable<ExamenDto>> ObtenerHijosDeExamenAsync(int idPadre);
        Task<HttpResponseMessage> AgregarExamenHijoAsync(int idPadre, int idHijo);
        Task<HttpResponseMessage> EliminarExamenHijoAsync(int idPadre, int idHijo);

        //Ordenes
        Task<HttpResponseMessage> RegistrarOrdenCompletaAsync(OrdenCompletaDto dto);

        Task<IEnumerable<MedicoDto>> GetMedicosAsync();
        Task<HttpResponseMessage> CrearMedicoAsync(MedicoDto medico);

        Task<IEnumerable<ExamenDto>> BuscarExamenesAsync(string campo, string valor);

        Task<OrdenDetalleDto?> ObtenerDetalleOrdenAsync(int id);

        Task<HttpResponseMessage> AnularOrdenAsync(int idOrden);

        //medico
        Task<List<MedicoDto>> BuscarMedicosAsync(string criterio, string valor);
        Task<HttpResponseMessage> EditarMedicoAsync(int id, MedicoDto medico);
        Task<HttpResponseMessage> AnularMedicoAsync(int id);
        Task<MedicoDto?> ObtenerMedicoPorIdAsync(int id);

        // Resultados
        Task<IEnumerable<ResultadoDto>> GetResultadosAsync();
        Task<ResultadoDetalleDto?> ObtenerDetalleResultadoAsync(int id);
        Task<HttpResponseMessage> GuardarResultadosAsync(ResultadoGuardarDto dto);
        Task<HttpResponseMessage> AnularResultadoAsync(int idResultado);

        Task<IEnumerable<DetallePagoDto>> GetPagosPorOrdenAsync(int idOrden);

        Task<HttpResponseMessage> RegistrarPagoAsync(PagoDto pago);

        Task<IEnumerable<ReactivoDto>> GetReactivosAsync();

        Task<bool> RegistrarReactivoAsync(ReactivoDto nuevo);

        Task<bool> EditarReactivoAsync(int id, ReactivoDto dto);
        Task<HttpResponseMessage> AnularReactivoAsync(int id);

        Task<HttpResponseMessage> RegistrarMovimientosAsync(List<MovimientoReactivoDto> movimientos);

    }
}
