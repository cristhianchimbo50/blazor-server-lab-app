using System.Text.Json.Serialization;

namespace LabWebAppBlazor.Models
{
    public class LoginResponseDto
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("nombre")]
        public string? Nombre { get; set; }

        [JsonPropertyName("rol")]
        public string? Rol { get; set; }

        [JsonPropertyName("correoUsuario")]
        public string? CorreoUsuario { get; set; }

        [JsonPropertyName("esContraseñaTemporal")]
        public bool EsContraseñaTemporal { get; set; }
    }

}
