namespace LabWebAppBlazor.Models
{
    public class ExamenDetalleDto
    {
        public int IdExamen { get; set; }
        public string? NombreExamen { get; set; }
        public string? NombreEstudio { get; set; }
        public int? IdResultado { get; set; }
        public string? NumeroResultado { get; set; }
        public bool Seleccionado { get; set; } = false;
    }

}
