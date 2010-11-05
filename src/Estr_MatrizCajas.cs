using System;
using System.Drawing;
using System.Collections;

namespace SokoVen.Estructura
{
    /// <summary>
    /// MatrizCajas: representa a una matriz con un bool en cada casilla del mapa.
    /// Ese bool representa si hay o no una caja esa posicion del mapa.
    /// </summary>
    public class MatrizCajas : IEnumerable, ICloneable, IEquatable<MatrizCajas>
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

        private static int[] GetArrayIterable(BitArray bitArray)
        {
            int bits = (bitArray.Length + 0x1f) / 0x20;
            int[] arr = new int[bits];
            bitArray.CopyTo(arr, 0);
            return arr;
        }

        public override bool Equals(Object obj)
        {
            if (this.GetType() != obj.GetType())
                return false;
            MatrizCajas elOtro = (MatrizCajas)obj;
            return this.Equals(elOtro);
        }

        public bool Equals(MatrizCajas elOtro)
        {
            int[] miVector = GetArrayIterable(this.vector);
            int[] otroVector = GetArrayIterable(elOtro.vector);
            for (int i = 0; i < miVector.Length; i++)
            {
                if (miVector[i] != otroVector[i])
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int[] miVector = GetArrayIterable(this.vector);
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                foreach (int a in miVector)
                {
                    hash = hash * 23 + a.GetHashCode();
                }
                return hash;
            }
        }

        public Object Clone()
        {
            return (Object)new MatrizCajas(this);
        }
    }
}

