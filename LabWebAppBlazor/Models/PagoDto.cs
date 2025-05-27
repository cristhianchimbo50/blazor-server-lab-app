namespace LabWebAppBlazor.Models;

public class PagoDto
{
    public int IdPago { get; set; }
    public int? IdOrden { get; set; }
    public DateTime? FechaPago { get; set; }
    public decimal? MontoPagado { get; set; }
    public string? Observacion { get; set; }
    public bool? Anulado { get; set; }
    public int? IdUsuario { get; set; }
    public decimal DineroEfectivo { get; set; }
    public decimal Transferencia { get; set; }
    public string Observaciones { get; set; } = string.Empty;
}
