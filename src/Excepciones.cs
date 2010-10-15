using System;
using System.Drawing;

namespace SokoVen
{
    /// <summary>
    /// ErrorSokoVen.
    /// </summary>
    public class ErrorSokoVen : ApplicationException
    {
        public ErrorSokoVen(string msg) : base(msg) { }
        public ErrorSokoVen(string msg, Exception inner)
            : base(msg + '\n' +
                inner.Message, inner) { }
    }

    /// <summary>
    /// ErrorAlCargarMapa.
    /// </summary>
    public class ErrorAlCargarMapa : ErrorSokoVen
    {
        private int numMapa;
        public int NumMapa
        {
            get { return numMapa; }
            set { numMapa = value; }
        }
        private string nombreArchivo;
        public string NombreArchivo
        {
            get { return nombreArchivo; }
            set { nombreArchivo = value; }
        }
        public ErrorAlCargarMapa(string msg) : base(msg) { }
        public ErrorAlCargarMapa(string msg, Exception inner) : base(msg, inner) { }
    }

    /// <summary>
    /// ErrorSokoVen.
    /// </summary>
    public class MovidaInvalida : ErrorSokoVen
    {
        public MovidaInvalida(string msg) : base("Movida Invalida: " + msg) { }
        public MovidaInvalida(string msg, Exception inner)
            : base(
                "Movida Invalida: " + msg, inner) { }
    }

    /// <summary>
    /// FueraDelMapa.
    /// </summary>
    public class FueraDelMapa : MovidaInvalida
    {
        private Point posicion;
        public Point Posicion
        {
            get { return posicion; }
        }
        public FueraDelMapa(string msg, Point posicion)
            : base(msg)
        {
            this.posicion = posicion;
        }
        public FueraDelMapa(string msg, Exception inner, Point posicion) :
            base(msg, inner)
        {
            this.posicion = posicion;
        }
    }

    /// <summary>
    /// NoExisteMovida.
    /// </summary>
    public class NoExisteMovida : ErrorSokoVen
    {
        public NoExisteMovida(string msg) : base(msg) { }
    }
}
