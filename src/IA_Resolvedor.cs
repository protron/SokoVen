using System;
using System.Collections;
using System.Drawing;

using SokoVen.Estructura;
using SokoVen.IO;

namespace SokoVen.IA {
  /// <summary>
  /// Resolvedor.
  /// </summary>
  public class Resolvedor {
    /// Devuelve:
    /// * La lista de movidas usadas para llegar a la solucion.
    public static Queue resolver(Analizador analizador, Estado estadoInicial) {
//      IALogger logger = new IALogger();
      try {
//      logger.WriteLine("---COMIENZO---");
        Queue estadosPorVisitar = new Queue();
        Queue estadosVistos = new Queue();
        Hashtable tablaMovidas = new Hashtable();
        tablaMovidas.Add(estadoInicial, new Queue());
        estadosPorVisitar.Enqueue(estadoInicial);
        estadosVistos.Enqueue(estadoInicial);
        while (estadosPorVisitar.Count > 0) {
          Estado estadoActual = (Estado) estadosPorVisitar.Dequeue();
          if (estadoActual.MapaTerminado()) {
/*
                foreach (DictionaryEntry de in tablaMovidas) {
                  sw.Write("q(" + ((Estado) de.Key).GetHashCode() + ")");
                  Queue queue = (Queue) de.Value;
                  foreach (Movida mov in queue)
                    sw.Write(mov.direccion.Nombre);
                  sw.WriteLine();
                }
                sw.WriteLine(" -- MOVIDA GANADORA --");
                Queue q = (Queue) tablaMovidas[estadoActual];
                foreach (Movida mov in q)
                  sw.Write(mov.direccion.Nombre);
                sw.WriteLine();
*/
            return (Queue) tablaMovidas[estadoActual];
          }
          for (int nDir = 0; nDir < 4; nDir++) {
            Direccion direccion = Direccion.todas[nDir];
            Movida movida;
            try {
              movida = Movida.crear(estadoActual, direccion);
            } catch (MovidaInvalida) {
              continue;
            }
            Estado estadoProximo = (Estado) estadoActual.Clone();
            estadoProximo.realizarMovida(movida);
/*
              if (estadosVisitados.Contains(estadoProximo)) {
                sw.Write("--- Repetido: <Proximo>");
                sw.Write(estadoProximo.PosTipito.ToString() + estadoProximo.GetHashCode());
                sw.Write("</Proximo> <Visitados>");
                foreach (Estado estado in estadosVisitados) {
                  sw.Write(estado.PosTipito.ToString());
                  sw.Write(estado.Equals(estadoProximo) ? "* " : ". ");
                }
                sw.WriteLine("</Visitados>");
                foreach (DictionaryEntry de in tablaMovidas) {
                  sw.Write("queue(" + ((Estado) de.Key).Equals(estadoProximo) + ":");
                  if (((Estado) de.Key).Equals(estadoProximo)) {
                    sw.Write("SI[" + ((Estado) de.Key).GetHashCode() + "," + estadoProximo.GetHashCode() + "]");
                  }
                  Queue queue = (Queue) de.Value;
                  foreach (Movida mov in queue)
                    sw.Write(mov.direccion.Nombre);
                  sw.Write(")");
                }
                  sw.WriteLine();
                  sw.Write("ContainsValue:");
                  sw.Write(tablaMovidas.ContainsValue(estadoProximo) ? "TRUE" : "FALSE");
                  foreach (Estado e in tablaMovidas.Keys) {
                    sw.Write(" " + e.posTipito.ToString() + " ");
                    if (! e.Equals(estadoProximo))
                      sw.Write("NO-");
                    sw.Write("Igual");
                  }
                  sw.WriteLine();
                  sw.WriteLine();
              }
              sw.WriteLine();
              sw.Write(estadoActual.posTipito.ToString());
              sw.Write(estadoActual.GetHashCode() + " ");
              sw.Write(estadoProximo.posTipito.ToString());
              sw.Write(estadoProximo.GetHashCode() + " ");
              sw.WriteLine(estadosVistos.Contains(estadoProximo) ? "TRUE" : "FALSE");
*/
            if (! estadosVistos.Contains(estadoProximo)) {
              if (! esEstupida(movida, analizador)) {
                estadosVistos.Enqueue(estadoProximo);
                estadosPorVisitar.Enqueue(estadoProximo);
                Queue listaMovidas = (Queue) ((Queue) tablaMovidas[estadoActual]).Clone();
                listaMovidas.Enqueue(movida);
                tablaMovidas.Add(estadoProximo, listaMovidas);
              }
            }
          }
        }
//        logger.WriteLine("El mapa no tiene solucion");
        throw new ErrorSokoVen("El mapa no tiene solucion");
      } finally {
//        logger.Close();
      }
    }

    public static bool esEstupida(Movida movida, Analizador analizador) {
      if (movida.SubMovida != null) {
        Point nuevaPosCaja = movida.SubMovida.PosFinal;
        if (! analizador[nuevaPosCaja])
          return true;
      }
      return false;
    }
  }
}

