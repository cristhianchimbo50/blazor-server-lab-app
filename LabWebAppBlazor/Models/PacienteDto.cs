using System.ComponentModel.DataAnnotations;

namespace LabWebAppBlazor.Models
{
    public class PacienteDto
    {
        public int IdPaciente { get; set; } // Autogenerado por la BD

        [Required(ErrorMessage = "La cédula es obligatoria")]
        public string CedulaPaciente { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string NombrePaciente { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacPaciente { get; set; }

        public int? EdadPaciente { get; set; }  // Nullable en BD

        public string DireccionPaciente { get; set; }  // Nullable en BD

        public string CorreoElectronicoPaciente { get; set; }  // Nullable

        public string TelefonoPaciente { get; set; }  // Nullable

        public DateTime? FechaRegistro { get; set; }  // Nullable

        public bool? Anulado { get; set; }  // Nullable

        public int? IdUsuario { get; set; }  // Nullable (FK)
    }
}
