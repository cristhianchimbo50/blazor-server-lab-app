namespace LabWebAppBlazor.Models
{
    public class MovimientoReactivoDto
    {
        public int IdReactivo { get; set; }
        public string? NombreReactivo { get; set; }
        public string? Unidad { get; set; }
        public decimal Cantidad { get; set; }
        public string? TipoMovimiento { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public int? IdOrden { get; set; }
        public string? Observacion { get; set; }
    }
}
