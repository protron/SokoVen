using System;
using System.Collections;
using System.Drawing;

using SokoVen.Estructura;
using SokoVen.IO;

namespace SokoVen.IA
{
    /// <summary>
    /// Resolvedor.
    /// </summary>
    public class Resolvedor
    {
        /// Devuelve:
        /// * La lista de movidas usadas para llegar a la solucion.
        public static Queue resolver(Analizador analizador, Estado estadoInicial)
        {
            //using (IALogger logger = new IALogger())
            //{
            //    logger.WriteLine("---COMIENZO---");
            int initCapacity = analizador.Mapa.Lugares.Count * 3;
            Queue estadosPorVisitar = new Queue(initCapacity, 3.0f);
            Hashtable tablaMovidas = new Hashtable(initCapacity, 0.5f);
            tablaMovidas.Add(estadoInicial, new Queue());
            estadosPorVisitar.Enqueue(estadoInicial);
            while (estadosPorVisitar.Count > 0)
            {
                Estado estadoActual = (Estado)estadosPorVisitar.Dequeue();
                if (estadoActual.MapaTerminado())
                {
                    return (Queue)tablaMovidas[estadoActual];
                }
                for (int nDir = 0; nDir < 4; nDir++)
                {
                    Direccion direccion = Direccion.todas[nDir];
                    Movida movida;
                    if (!Movida.crear(out movida, estadoActual, direccion))
                        continue;
                    Estado estadoProximo = (Estado)estadoActual.Clone();
                    estadoProximo.realizarMovida(movida);
                    if (!tablaMovidas.ContainsKey(estadoProximo))
                    {
                        if (!esEstupida(movida, analizador))
                        {
                            estadosPorVisitar.Enqueue(estadoProximo);
                            Queue listaMovidas = (Queue)((Queue)tablaMovidas[estadoActual]).Clone();
                            listaMovidas.Enqueue(movida);
                            tablaMovidas.Add(estadoProximo, listaMovidas);
                        }
                    }
                }
            }
            //    logger.WriteLine("El mapa no tiene solucion");
            throw new ErrorSokoVen("El mapa no tiene solucion");
            //}
        }

        public static bool esEstupida(Movida movida, Analizador analizador)
        {
            if (movida.SubMovida != null)
            {
                Point nuevaPosCaja = movida.SubMovida.PosFinal;
                if (!analizador[nuevaPosCaja])
                    return true;
            }
            return false;
        }
    }
}

