using System.Collections;
using System.Drawing;

namespace SokoVen.Estructura
{
    /// <summary>
    /// Movida: guarda la información para generar y retroceder una movida.
    /// </summary>
    public class  Movida
    {
        private Movida() { }
        internal Movida(TipoObjeto tipoOb, Direccion dir, Point posI, Point posF)
        {
            this.tipoObjeto = tipoOb;
            this.direccion = dir;
            this.posInicial = posI;
            this.posFinal = posF;
        }

        public static bool crear(out Movida movida, Estado estado, Direccion direccion)
        {
            movida = null;
            Point posDest;
            if (!estado.Mapa.vecino(out posDest, estado.PosTipito, direccion))
                return false;
            movida = new Movida(TipoObjeto.Tipito, direccion, estado.PosTipito, posDest);
            if (!estado.pasable(posDest))
            {
                if (!estado.MatrizCajas[posDest])
                    return false;
                Point posFinal;
                if (!estado.Mapa.vecino(out posFinal, posDest, direccion))
                    return false;
                movida.SubMovida = new Movida(TipoObjeto.Caja, direccion, posDest, posFinal);
                if (!estado.pasable(movida.SubMovida.PosFinal))
                    return false;
            }
            return true;
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
            get
            {
                yield return posInicial;
                yield return posFinal;
                if (subMovida != null)
                    yield return subMovida.PosFinal;
            }
        }
    }
}
