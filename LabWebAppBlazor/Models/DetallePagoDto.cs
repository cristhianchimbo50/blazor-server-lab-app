namespace LabWebAppBlazor.Models;

public class DetallePagoDto
{
    public int IdDetallePago { get; set; }
    public int? IdPago { get; set; }
    public string? TipoPago { get; set; }
    public decimal? Monto { get; set; }
    public int? IdUsuario { get; set; }

    public DateTime? FechaPago { get; set; }
}
