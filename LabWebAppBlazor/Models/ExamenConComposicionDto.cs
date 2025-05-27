namespace LabWebAppBlazor.Models
{
    public class ExamenConComposicionDto
    {
        public ExamenDto Examen { get; set; } = new();
        public List<int> IdExamenesHijos { get; set; } = new();
    }

}
