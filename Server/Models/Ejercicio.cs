namespace GreenCoinHealth.Server.Models
{
    public class Ejercicio
    {
        public int EjercicioId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Repeticiones { get; set; }
        public int Series { get; set; }
        public int RutinaId { get; set; }
        public virtual Rutina Rutina { get; set; }
        public int DificultadId { get; set; }  // referencia a Dificultad
        public virtual Dificultad Dificultad { get; set; }  // Navegación hacia Dificultad
    }
}
