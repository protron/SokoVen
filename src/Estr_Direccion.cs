using System.Collections;
using System.Drawing;

namespace SokoVen.Estructura
{
    /// <summary>
    /// Direccion: Contiene las 4 direcciones, una lista con todas ellas, y ademas
    ///            una lista para las verticales y otra para la horizontales.
    /// </summary>
    public class Direccion
    {
        private Direccion(Size desp, Direccion inversa, string toString)
        {
            this.desp = desp;
            this.toString = toString;
            this.inversa = inversa;
        }

        public static readonly Direccion
          arriba = new Direccion(new Size(0, -1), abajo, "^"),
          derecha = new Direccion(new Size(1, 0), izquierda, ">"),
          abajo = new Direccion(new Size(0, 1), arriba, "v"),
          izquierda = new Direccion(new Size(-1, 0), derecha, "<");

        public static readonly Direccion[]
          todas = { arriba, derecha, abajo, izquierda },
          verticales = { arriba, abajo },
          horizontales = { izquierda, derecha };

        private Size desp;
        public Size Desp
        {
            get { return desp; }
        }

        private Direccion inversa;
        public Direccion Inversa
        {
            get { return inversa; }
        }

        private string toString;
        public override string ToString()
        {
            return toString;
        }
    }
}
