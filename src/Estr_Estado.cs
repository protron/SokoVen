using System;
using System.Drawing;
using System.Collections;

namespace SokoVen.Estructura
{
    /// <summary>
    /// TipoObjeto: Representación de los diferentes tipos de objetos
    /// * Ninguno: Lugar donde no hay ningun objeto.
    /// * Tipito: Es la representación del jugador (uno por mapa).
    /// * Caja: Objeto movible que se apoya sobre un piso o un destino.
    /// </summary>
    public enum TipoObjeto
    {
        Ninguno = 0,
        Tipito,
        Caja
    }

    /// <summary>
    /// Estado: Representa al estado actual del mapa y para ello contine:
    /// * La ubicación de las cajas
    /// * La ubicación del tipito
    /// Y apunta a:
    /// * El mapa del cual representa su estado
    /// </summary>
    public class Estado : ICloneable
    {
        public Estado(Mapa mapa)
        {
            this.mapa = mapa;
            this.matrizCajas = new MatrizCajas((byte)mapa.Ancho, (byte)mapa.Alto);
            this.posTipito = PosInvalida;
        }

        public Estado(Estado original)
        {
            this.mapa = original.Mapa;
            this.matrizCajas = (MatrizCajas)original.MatrizCajas.Clone();
            this.posTipito = new Point((Size)original.PosTipito);
        }

        private static readonly Point PosInvalida = new Point(-1, -1);

        internal Point posTipito;
        public Point PosTipito
        {
            get { return posTipito; }
            set
            {
                if (posTipito != PosInvalida)
                    throw new ErrorSokoVen("No puede haber más de un Tipito");
                posTipito = value;
            }
        }

        internal MatrizCajas matrizCajas;
        public MatrizCajas MatrizCajas
        {
            get { return matrizCajas; }
        }

        internal Mapa mapa;
        public Mapa Mapa
        {
            get { return mapa; }
        }

        public TipoObjeto this[Point posicion]
        {
            get
            {
                if (posicion == posTipito)
                    return TipoObjeto.Tipito;
                if (matrizCajas[posicion] == true)
                    return TipoObjeto.Caja;
                return TipoObjeto.Ninguno;
            }
            set
            {
                if (value == TipoObjeto.Tipito)
                    posTipito = posicion;
                else if (value == TipoObjeto.Caja)
                    matrizCajas[posicion] = true;
            }
        }

        public bool MapaTerminado()
        {
            foreach (Point posicion in mapa.Lugares.PosDestinos)
            {
                if (!MatrizCajas[posicion])
                    return false;
            }
            return true;
        }

        public virtual bool pasable(Point posicion)
        {
            return mapa.Lugares[posicion] != Lugar.Pared &&
              this[posicion] == TipoObjeto.Ninguno;
        }

        public void realizarMovida(Movida movida)
        {
            if (movida.SubMovida != null)
                realizarMovida(movida.SubMovida);
            if (movida.TipoObjeto == TipoObjeto.Caja)
            {
                matrizCajas[movida.PosInicial] = false;
                matrizCajas[movida.PosFinal] = true;
            }
            else if (movida.TipoObjeto == TipoObjeto.Tipito)
            {
                posTipito = movida.PosFinal;
            }
            else
                throw new ErrorSokoVen("Movida de TipoObjeto.Ninguno");
        }

        public override bool Equals(Object obj)
        {
            if (this.GetType() != obj.GetType())
                return false;
            Estado elOtro = (Estado)obj;
            return this.posTipito.Equals(elOtro.posTipito) &&
              this.matrizCajas.Equals(elOtro.matrizCajas);
        }

        public override int GetHashCode()
        {
            return ((posTipito.Y << 24) + (posTipito.X << 16)) ^ matrizCajas.GetHashCode();
        }

        public Object Clone()
        {
            return (Object)new Estado(this);
        }
    }
}

