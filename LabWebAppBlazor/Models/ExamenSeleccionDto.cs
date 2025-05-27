namespace LabWebAppBlazor.Models;

public class ExamenSeleccionDto
{
    public int IdExamen { get; set; }
    public string NombreExamen { get; set; } = "";
    public string Estudio { get; set; } = "";
    public decimal? Precio { get; set; }
    public bool Seleccionado { get; set; } = false;
}
