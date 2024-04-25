namespace GreenCoinHealth.Server.Models
{
    public class Dieta
    {
        public int DietaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<DietaAlimento> DietaAlimentos { get; set; }
    }
}
