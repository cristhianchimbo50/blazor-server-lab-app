namespace LabWebAppBlazor.Models
{
    public class ConvenioVistaDto
    {
        public int IdConvenio { get; set; }
        public string Medico { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal PorcentajeComision { get; set; }
    }

}
