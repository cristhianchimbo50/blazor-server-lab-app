namespace LabWebAppBlazor.Models;

public class OrdenCompletaDto
{
    public OrdenDto Orden { get; set; } = new();
    public List<int> IdsExamenes { get; set; } = new();
    public decimal DineroEfectivo { get; set; }
    public decimal Transferencia { get; set; }
    public string Observaciones { get; set; } = string.Empty;
}
