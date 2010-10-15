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
    /// NoExisteMovida.
    /// </summary>
    public class NoExisteMovida : ErrorSokoVen
    {
        public NoExisteMovida(string msg) : base(msg) { }
    }
}
