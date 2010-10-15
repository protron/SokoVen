using System.Collections;
using System.Drawing;

namespace SokoVen.Estructura
{
    /// <summary>
    /// Movida: guarda la información para generar y retroceder una movida.
    /// </summary>
    public class Movida
    {
        private Movida() { }
        internal Movida(TipoObjeto tipoOb, Direccion dir, Point posI, Point posF)
        {
            this.tipoObjeto = tipoOb;
            this.direccion = dir;
            this.posInicial = posI;
            this.posFinal = posF;
        }

        public static Movida crear(Estado estado, Direccion direccion)
        {
            Point posDest = estado.Mapa.vecino(estado.PosTipito, direccion);
            Movida movida = new Movida(TipoObjeto.Tipito, direccion, estado.PosTipito,
              posDest);
            if (!estado.pasable(posDest))
            {
                if (!estado.MatrizCajas[posDest])
                    throw new MovidaInvalida("Choque contra objeto no movible");
                movida.SubMovida = new Movida(TipoObjeto.Caja, direccion, posDest,
                  estado.Mapa.vecino(posDest, direccion));
                if (!estado.pasable(movida.SubMovida.PosFinal))
                    throw new MovidaInvalida("Choque contra caja bloqueda");
            }
            return movida;
        }

        public static Movida crearDeshacedora(Movida original)
        {
            Movida inversa = Movida.crearInversa(original);
            if (original.SubMovida == null)
                return inversa;
            Movida inversaSub = Movida.crearInversa(original.SubMovida);
            inversaSub.SubMovida = inversa;
            return inversaSub;
        }

        private static Movida crearInversa(Movida original)
        {
            return new Movida(original.TipoObjeto, original.Direccion.Inversa,
              original.posFinal, original.PosInicial);
        }

        private Movida subMovida;
        internal Movida SubMovida
        {
            get { return subMovida; }
            set
            {
                if (subMovida != null)
                    throw new ErrorSokoVen("Ya existia una subMovida");
                subMovida = value;
            }
        }

        internal TipoObjeto tipoObjeto;
        public TipoObjeto TipoObjeto
        {
            get { return tipoObjeto; }
        }

        internal Direccion direccion;
        public Direccion Direccion
        {
            get { return direccion; }
        }

        internal Point posInicial;
        public Point PosInicial
        {
            get { return posInicial; }
        }

        internal Point posFinal;
        public Point PosFinal
        {
            get { return posFinal; }
        }

        public IEnumerable PosAfectadas
        {
            get { return new ColPosAfectadas(this); }
        }

        /// <summary>
        /// ColPosAfectadas: Objeto enumerable de las posiciones afectadas por la
        ///                  movida.
        /// </summary>
        private class ColPosAfectadas : IEnumerable
        {
            private Movida movida;
            public ColPosAfectadas(Movida movida)
            {
                this.movida = movida;
            }
            public IEnumerator GetEnumerator()
            {
                return new EnumPosAf(movida);
            }
        }

        /// <summary>
        /// EnumPosAf: Enumerador de las posiciones afectadas por la movida.
        /// </summary>
        private class EnumPosAf : IEnumerator
        {
            Movida movida;
            int cantidad;
            int nIndex;

            public EnumPosAf(Movida movida)
            {
                this.movida = movida;
                cantidad = movida.SubMovida == null ? 1 : 2;
                Reset();
            }

            public void Reset()
            {
                nIndex = -1;
            }

            public bool MoveNext()
            {
                nIndex++;
                return nIndex <= cantidad;
            }

            object IEnumerator.Current
            {
                get
                {
                    switch (nIndex)
                    {
                        case 0: return movida.PosInicial;
                        case 1: return movida.PosFinal;
                        case 2: return movida.SubMovida.PosFinal;
                        default: return null;
                    }
                }
            }
        }
    }
}
