using System;
using System.Drawing;
using System.Collections;

using SokoVen.Estructura;

namespace SokoVen.IA
{
    /// <summary>
    /// Analizador.
    /// </summary>
    public class Analizador
    {
        private Mapa mapa;
        private int[] cantEmp = new int[2];

        public Analizador() { }

        public Mapa Mapa
        {
            get { return mapa; }
            set
            {
                this.mapa = value;
                cantEmp[(int)Eje.Horizontal] = mapa.Alto;
                cantEmp[(int)Eje.Vertical] = mapa.Ancho;
                this.posValidas = new bool[mapa.Ancho, mapa.Alto];
                this.marcaEmparedadas();
                this.cargarPosValidas();
            }
        }

        private ArrayList[][] emparedadas;
        private bool[,] posValidas;
        public bool this[Point posicion]
        {
            get { return posValidas[posicion.X, posicion.Y]; }
        }

        private void cargarPosValidas()
        {
            for (int y = 0; y < mapa.Alto; y++)
            {
                for (int x = 0; x < mapa.Ancho; x++)
                {
                    Point posicion = new Point(x, y);
                    Lugar lugar = mapa.Lugares[posicion];
                    posValidas[x, y] = lugar == Lugar.Pared || lugar == Lugar.Destino ||
                      movible(posicion);
                }
            }
            foreach (int eje in Enum.GetValues(typeof(Eje)))
            {
                for (int i = 0; i < cantEmp[eje]; i++)
                {
                    foreach (Emparedada emparedada in emparedadas[eje][i])
                    {
                        bool tieneDestino = false;
                        Point[] puntos = emparedada.darPuntosAfectados();
                        for (int a = 0; a < puntos.Length; a++)
                        {
                            if (mapa.Lugares[puntos[a]] == Lugar.Destino)
                            {
                                tieneDestino = true;
                                break;
                            }
                        }
                        if (!tieneDestino)
                        {
                            for (int b = 0; b < puntos.Length; b++)
                            {
                                posValidas[puntos[b].X, puntos[b].Y] = false;
                            }
                        }
                    }
                }
            }
        }

        public bool movible(Point posicion)
        {
            if (tieneVecinoPasable(posicion, Direccion.arriba) &&
                tieneVecinoPasable(posicion, Direccion.abajo))
                return true;
            if (tieneVecinoPasable(posicion, Direccion.izquierda) &&
                tieneVecinoPasable(posicion, Direccion.derecha))
                return true;
            return false;
        }

        public bool tieneVecinoPasable(Point posicion, Direccion direccion)
        {
            Point posVecina;
            if (!mapa.vecino(out posVecina, posicion, direccion))
                return false;
            return mapa.Lugares[posVecina] != Lugar.Pared;
        }

        private void inicializaEmparedadas()
        {
            this.emparedadas = new ArrayList[2][];
            foreach (int eje in Enum.GetValues(typeof(Eje)))
            {
                this.emparedadas[eje] = new ArrayList[cantEmp[eje]];
                for (int i = 0; i < cantEmp[eje]; i++)
                    this.emparedadas[eje][i] = new ArrayList(cantEmp[1 - eje]);
            }
        }

        private void marcaEmparedadas()
        {
            inicializaEmparedadas();
            Direccion[][] dir = { Direccion.verticales, Direccion.horizontales };
            bool ultimaFuePared;
            bool[] posible = new bool[2];
            int posFija, posVar, s, comienzo = 0;
            posible[0] = posible[1] = ultimaFuePared = true;
            foreach (int eje in Enum.GetValues(typeof(Eje)))
            {
                for (posFija = 0; posFija < cantEmp[eje]; posFija++)
                {
                    for (posVar = 0; posVar < cantEmp[1 - eje]; posVar++)
                    {
                        Point posicion = Emparedada.getPos((Eje)eje, posFija, posVar);
                        Lugar lugar = mapa.Lugares[posicion];
                        if (lugar == Lugar.Pared)
                        {
                            creaEmparedada(ref ultimaFuePared, posible, (Eje)eje, posFija,
                              comienzo, posVar);
                        }
                        else
                        {
                            if (ultimaFuePared)
                            {
                                comienzo = posVar;
                                ultimaFuePared = false;
                            }
                            for (s = 0; s <= 1; s++)
                            {
                                if (posible[s] && tieneVecinoPasable(posicion, dir[eje][s]))
                                    posible[s] = false;
                            }
                        }
                    }
                    creaEmparedada(ref ultimaFuePared, posible, (Eje)eje, posFija,
                      comienzo, posVar);
                }
            }
        }

        private void creaEmparedada(ref bool ultimaFuePared, bool[] posible,
            Eje eje, int posFija, int comienzo, int posVar)
        {
            if (!ultimaFuePared && (posible[0] || posible[1]))
            {
                Emparedada emp = new Emparedada();
                emp.posAlta = posVar - 1;
                emp.posBaja = comienzo;
                emp.posFija = posFija;
                emp.eje = eje;
                this.emparedadas[(int)eje][posFija].Add(emp);
            }
            posible[0] = posible[1] = ultimaFuePared = true;
        }
    }

    public enum Eje
    {
        Horizontal = 0,
        Vertical = 1
    }

    /// <summary>
    /// Emparedada: Representa a un conjunto de posiciones (definida por un
    /// valor sobre uno de los ejes y un rango sobre el otro) que está al
    /// lado de una pared cóncava.
    /// Esto sirve para detectar posiciones de las cuales las cajas no pueden
    /// salir. Y si no hay suficientes destinos sobre esta Emparedada no hace
    /// falta seguir buscando una salida ya que no la hay.
    /// </summary>
    public struct Emparedada
    {
        public Eje eje;
        public int posFija, posBaja, posAlta;

        public Point[] darPuntosAfectados()
        {
            int dif = posAlta - posBaja;
            Point[] puntos = new Point[dif + 1];
            for (int i = 0; i <= dif; i++)
                puntos[i] = getPos(eje, posFija, posBaja + i);
            return puntos;
        }

        public static Point getPos(Eje eje, int posFija, int posVar)
        {
            if (eje == Eje.Horizontal)
                return new Point(posVar, posFija);
            else
                return new Point(posFija, posVar);
        }
    }
}

