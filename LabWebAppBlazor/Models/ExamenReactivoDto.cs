namespace LabWebAppBlazor.Models
{
    public class ExamenReactivoDto
    {
        public int IdExamenReactivo { get; set; }
        public int IdExamen { get; set; }
        public int IdReactivo { get; set; }
        public string? NombreReactivo { get; set; }
        public string? Unidad { get; set; }
        public decimal CantidadUsada { get; set; }
    }

}
