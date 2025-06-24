namespace LabWebAppBlazor.Models
{
    public class OrdenSeleccionDto
    {
        public int IdOrden { get; set; }
        public string NombrePaciente { get; set; }
        public DateTime FechaOrden { get; set; }
        public bool Seleccionado { get; set; } = false;
    }

}
