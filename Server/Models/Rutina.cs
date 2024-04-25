namespace GreenCoinHealth.Server.Models
{
    public class Rutina
    {
        public int RutinaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Ejercicio> Ejercicios { get; set; } = new List<Ejercicio>();
    }
}