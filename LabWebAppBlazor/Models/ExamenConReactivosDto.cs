namespace LabWebAppBlazor.Models
{
    public class ExamenConReactivosDto
    {
        public ExamenDto Examen { get; set; } = new();
        public List<ExamenReactivoDto> Reactivos { get; set; } = new();
    }
}
