using System;
using System.Drawing;
using System.IO;
using SokoVen.Estructura;

namespace SokoVen.IO {
  /// <summary>
  /// CargaMapa: la clase encargada de cargar desde el archivo el objeto Mapa.
  /// </summary>
  public class CargaMapa {
    private readonly static CargaMapa singleton = new CargaMapa();
    private CargaMapa() {}
    public static CargaMapa get {
      get {return singleton;}
    }

    public int numMapa;
    public string rutaMapa;

    public Mapa cargar(int numMapa) {
      this.numMapa = numMapa;
      this.rutaMapa = Config.get.Mapas.formarRutaCompleta(numMapa.ToString());
      try {
       using (StreamReader sr = File.OpenText(this.rutaMapa)) {
        byte alto = leerByteDesdeLinea(sr, "Alto=");
        byte ancho = leerByteDesdeLinea(sr, "Ancho=");
        Mapa mapa = new Mapa(ancho, alto);
        int numFila = 0;
        string linea = "";
        while ((linea = sr.ReadLine()) != null) {
          int numColumna = 0;
          foreach (char caracter in linea) {
            Point posicion = new Point(numColumna, numFila);
            Lugar lugar;
            TipoObjeto objeto = TipoObjeto.Ninguno;
            switch (caracter) {
              case ' ':
                lugar = Lugar.Piso;
                break;
              case '$':
                lugar = Lugar.Piso;
                objeto = TipoObjeto.Caja;
                break;
              case '@':
                lugar = Lugar.Piso;
                objeto = TipoObjeto.Tipito;
                break;
              case '.':
                lugar = Lugar.Destino;
                break;
              case '*':
                lugar = Lugar.Destino;
                objeto = TipoObjeto.Caja;
                break;
              case '+':
                lugar = Lugar.Destino;
                objeto = TipoObjeto.Tipito;
                break;
              case '#':
                lugar = Lugar.Pared;
                break;
              default:
                throw new ErrorAlCargarMapa("La posicion " + posicion.ToString()
                  + " contiene el caracter no valido '" + caracter + "'");
            }
            mapa.Lugares[posicion] = lugar;
            if (objeto != TipoObjeto.Ninguno)
              mapa.EstadoActual[posicion] = objeto;
            numColumna++;
          }
          numFila++;
        }
        return mapa;
       }
      } catch (ErrorAlCargarMapa eacm) {
        eacm.NumMapa = numMapa;
        eacm.NombreArchivo = this.rutaMapa;
        throw eacm;
      } catch (System.Exception se) {
        ErrorAlCargarMapa eacm = new ErrorAlCargarMapa(
          @"No se pudo abrir el archivo """ + this.rutaMapa + '"', se);
        eacm.NumMapa = numMapa;
        eacm.NombreArchivo = this.rutaMapa;
        throw eacm;
      }
    }

    private byte leerByteDesdeLinea(StreamReader sr, string comienzoEsperado) {
      string linea = sr.ReadLine();
      linea = linea.Trim();
      string comienzo = linea.Substring(0, comienzoEsperado.Length);
      if (! comienzo.ToUpper().StartsWith(comienzoEsperado.ToUpper()))
        throw new ErrorAlCargarMapa("La linea debería empezar con '" +
        comienzoEsperado + "', pero comienza con '" + comienzo + "'");
      string fin = linea.Substring(comienzoEsperado.Length);
      return Byte.Parse(fin);
    }
  }
}
