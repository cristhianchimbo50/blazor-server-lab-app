namespace LabWebAppBlazor.Models
{
    public class ReactivoAsociadoDto
    {
        public int IdReactivo { get; set; }
        public string NombreReactivo { get; set; } = string.Empty;
        public decimal CantidadUsada { get; set; }
        public string Unidad { get; set; } = string.Empty;
    }
}
