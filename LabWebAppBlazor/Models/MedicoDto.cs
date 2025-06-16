namespace LabWebAppBlazor.Models;

public class MedicoDto
{
    public int IdMedico { get; set; }
    public string NombreMedico { get; set; } = string.Empty;
    public string? Especialidad { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public bool Anulado { get; set; }
    public int? IdUsuario { get; set; }
}
