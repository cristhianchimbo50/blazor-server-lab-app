using System.ComponentModel.DataAnnotations;

namespace LabWebAppBlazor.Models
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        public string CorreoUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Clave { get; set; } = string.Empty;
    }


}
