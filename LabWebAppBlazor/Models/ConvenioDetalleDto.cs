namespace LabWebAppBlazor.Models
{
    public class ConvenioDetalleDto
    {
        public int IdConvenio { get; set; }
        public DateOnly FechaConvenio { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal PorcentajeComision { get; set; }

        public string NombreMedico { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;

        public List<OrdenConvenioDto> Ordenes { get; set; } = new();
    }

}
