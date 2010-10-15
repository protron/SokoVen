using System;
using System.Drawing;
using System.Collections;

namespace SokoVen.Estructura
{
    /// <summary>
    /// MatrizCajas: representa a una matriz con un bool en cada casilla del mapa.
    /// Ese bool representa si hay o no una caja esa posicion del mapa.
    /// </summary>
    public class MatrizCajas : IEnumerable, ICloneable
    {
        public MatrizCajas(byte ancho, byte alto)
        {
            vector = new BitArray((int)ancho * alto, false);
            this.ancho = ancho;
        }

        public MatrizCajas(MatrizCajas original)
        {
            this.vector = (BitArray)original.vector.Clone();
            this.ancho = original.ancho;
        }

        internal BitArray vector;
        internal byte ancho;

        private int posAVector(Point posicion)
        {
            return posicion.Y * ancho + posicion.X;
        }

        public bool this[Point posicion]
        {
            get { return vector[posAVector(posicion)]; }
            set { vector[posAVector(posicion)] = value; }
        }

        public IEnumerator GetEnumerator()
        {
            return vector.GetEnumerator();
        }

        public override bool Equals(Object obj)
        {
            if (this.GetType() != obj.GetType())
                return false;
            MatrizCajas elOtro = (MatrizCajas)obj;
            for (int i = 0; i < this.vector.Length; i++)
            {
                if (this.vector[i] != elOtro.vector[i])
                    return false;
            }
            return true;
            //      return this.vector.Equals(elOtro.vector);
        }

        public override int GetHashCode()
        {
            return vector.GetHashCode();
        }

        public Object Clone()
        {
            return (Object)new MatrizCajas(this);
        }
    }
}

