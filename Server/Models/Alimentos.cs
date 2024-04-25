namespace GreenCoinHealth.Server.Models
{
    public class Alimentos
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public double Precio { get; set; }
        //public DateTime FechaVencimiento { get; set; }
        public bool EsOrganico { get; set; }
        public string Origen { get; set; } = string.Empty;
        //  public string[] Ingredientes { get; set; } = Array.Empty<string>();
    }
}
