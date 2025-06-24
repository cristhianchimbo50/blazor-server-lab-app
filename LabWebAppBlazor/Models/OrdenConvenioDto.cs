namespace LabWebAppBlazor.Models
{
    public class OrdenConvenioDto
    {
        public int IdOrden { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;
        public string Paciente { get; set; } = string.Empty;
        public DateOnly? FechaOrden { get; set; }
        public decimal Total { get; set; }
        public string EstadoPago { get; set; } = string.Empty;
        public bool Seleccionado { get; set; } = false;
    }

}
