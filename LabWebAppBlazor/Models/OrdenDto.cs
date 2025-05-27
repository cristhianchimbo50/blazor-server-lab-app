namespace LabWebAppBlazor.Models
{
    public class OrdenDto
    {
        public int IdOrden { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;
        public string CedulaPaciente { get; set; } = string.Empty;
        public string NombrePaciente { get; set; } = string.Empty;
        public int? IdPaciente { get; set; }
        public int? IdMedico { get; set; }
        public DateOnly FechaOrden { get; set; }
        public decimal Total { get; set; }
        public decimal SaldoPendiente { get; set; }
        public decimal TotalPagado { get; set; }
        public string EstadoPago { get; set; } = "PENDIENTE";
        public bool Anulado { get; set; } = false;
        public string? Observacion { get; set; }
        public int? IdUsuario { get; set; }
    }
}
