namespace LabWebAppBlazor.Models
{
    public class ConvenioGuardarDto
    {
        public string Nombre { get; set; }
        public int IdMedico { get; set; }
        public List<int> Ordenes { get; set; } = new();
    }

}
