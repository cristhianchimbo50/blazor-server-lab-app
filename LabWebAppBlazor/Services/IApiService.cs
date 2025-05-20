using LabWebAppBlazor.Models;

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

    }
}
