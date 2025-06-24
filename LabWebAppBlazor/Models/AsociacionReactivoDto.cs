namespace LabWebAppBlazor.DTOs
{
    public class AsociacionReactivoDto
    {
        public int IdExamen { get; set; }
        public string NombreExamen { get; set; } = string.Empty;
        public string NombreReactivo { get; set; } = string.Empty;
        public decimal CantidadUsada { get; set; }
        public string Unidad { get; set; } = string.Empty;
    }

}

