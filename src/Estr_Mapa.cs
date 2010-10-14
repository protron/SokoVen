using System.Drawing;

namespace SokoVen.Estructura {
  /// <summary>
  /// Mapa: Esta contine:
  /// * Los lugares del mapa (objetos fijos).
  /// * El estado actual del juego (objetos movibles).
  /// * La lista de movidas realizadas.
  /// </summary>
  public class Mapa {
    public Mapa(byte ancho, byte alto) {
      lugares = new Lugares(ancho, alto);
      estadoActual = new Estado(this);
    }

    private Estado estadoActual;
    public Estado EstadoActual {
      get {return estadoActual;}
      set {estadoActual = value;}
    }

    private Lugares lugares;
    public Lugares Lugares {
      get {return lugares;}
    }

    public byte Ancho {
      get {return lugares.Ancho;}
    }

    public byte Alto {
      get {return lugares.Alto;}
    }

    public Point vecino(Point posicion, Direccion direccion) {
      Point nuevaPos = posicion + direccion.Desp;
      if (nuevaPos.X < 0 || nuevaPos.X >= lugares.Ancho ||
          nuevaPos.Y < 0 || nuevaPos.Y >= lugares.Alto) {
        string msg = "El mapa no tiene posición " + nuevaPos;
        throw new FueraDelMapa(msg, nuevaPos);
      }
      /*try {
        Lugar l = lugares[nuevaPos]; //prueba que no esté fuera de rango
      } catch (System.IndexOutOfRangeException e) {
        string msg = "El mapa no tiene posición " + nuevaPos;
        throw new FueraDelMapa(msg, e, nuevaPos);
      }*/
      return nuevaPos;
    }
  }
}
