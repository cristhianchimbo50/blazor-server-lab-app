namespace LabWebAppBlazor.Models
{
    public class OrdenDetalleDto
    {
        public int IdOrden { get; set; }
        public int IdMedico { get; set; }
        public string? NombreMedico { get; set; }
        public string EstadoPago { get; set; } = "";
        public string? NumeroOrden { get; set; }
        public string? FechaOrden { get; set; }
        public string? CedulaPaciente { get; set; }
        public string? NombrePaciente { get; set; }
        public string? DireccionPaciente { get; set; }
        public string? CorreoPaciente { get; set; }
        public string? TelefonoPaciente { get; set; }
        public int IdPaciente { get; set; }
        public decimal TotalOrden { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public bool Anulado { get; set; }

        public List<ExamenDetalleDto> Examenes { get; set; } = new();
    }
}
