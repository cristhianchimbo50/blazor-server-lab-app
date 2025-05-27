namespace LabWebAppBlazor.Models;

public class DetalleOrdenDto
{
    public int IdDetalleOrden { get; set; }
    public int? IdOrden { get; set; }
    public int? IdExamen { get; set; }
    public decimal? Precio { get; set; }
}
