using System.Drawing;
using System.Collections;

namespace SokoVen.Estructura
{
    /// <summary>
    /// Los lugares que contiene el mapa pueden ser:
    /// * Paredes: Nunca puede pasarse por ahí y tampoco puede moverse.
    /// * Pisos: Espacio vacio transitable (a menos que sobre este haya una caja).
    /// * Destinos: Igual a los pisos pero ademas representa los lugares donde el
    ///             jugador tedrá que poner las cajas para pasar de nivel.
    /// </summary>
    public enum Lugar
    {
        Piso = 0,
        Destino,
        Pared
    }

    /// <summary>
    /// Lugares: La clase que administra los lugares del mapa, estos lugares son
    /// las posiciones fijas (no movibles).
    /// Los lugares contenidos pueden ser:
    /// * Paredes: Nunca puede pasarse por ahí y tampoco puede moverse.
    /// * Pisos: Espacio vacio transitable (a menos que sobre este haya una caja).
    /// * Destinos: Igual a los pisos pero ademas representa los lugares donde el
    ///             jugador tedrá que poner las cajas para pasar de nivel.
    /// </summary>
    public class Lugares
    {
        public Lugares(byte ancho, byte alto)
        {
            area = new Lugar[ancho, alto];
            posDestinos = new ArrayList();
        }

        private Lugar[,] area;
        public Lugar this[Point posicion]
        {
            get { return area[posicion.X, posicion.Y]; }
            set
            {
                area[posicion.X, posicion.Y] = value;
                if (value == Lugar.Destino)
                    posDestinos.Add(posicion);
            }
        }

        private ArrayList posDestinos;
        public ArrayList PosDestinos
        {
            get { return posDestinos; }
        }

        public byte Ancho
        {
            get { return (byte)area.GetLength(0); }
        }

        public byte Alto
        {
            get { return (byte)area.GetLength(1); }
        }
    }
}
