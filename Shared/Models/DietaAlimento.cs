namespace GreenCoinHealth.Shared.Models
{
    public class DietaAlimento
    {
        public int DietaId { get; set; }
        public virtual Dieta Dieta { get; set; }
        public int AlimentoId { get; set; }
        public virtual Alimentos Alimento { get; set; }
    }
}
