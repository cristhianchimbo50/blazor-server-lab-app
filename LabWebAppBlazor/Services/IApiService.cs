using LabWebAppBlazor.Models;

namespace LabWebAppBlazor.Services
{
    public interface IApiService
    {
        Task<IEnumerable<PacienteDto>> GetPacientesAsync();
        Task<HttpResponseMessage> CrearPacienteAsync(PacienteDto paciente);
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);

        Task<bool> VerificarTokenAsync();


    }
}
