namespace LabWebAppBlazor.Models
{
    public class ConvenioRegistrarDto
    {
        public int? IdMedico { get; set; }
        public decimal Comision { get; set; }
        public decimal TotalPagar { get; set; }
        public int IdUsuario { get; set; }
        public DateOnly FechaConvenio { get; set; }
        public List<OrdenConvenioSeleccionadoDto> Ordenes { get; set; } = new();
    }
}
