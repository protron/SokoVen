using System.Diagnostics;

namespace SokoVen.IO
{
    /// <summary>
    /// IALogger.
    /// </summary>
    public class IALogger
    {
        private EventLog log = null;

        public IALogger()
        {
            ConfigIA configIA = Config.get.IA;
            if (configIA.GrabarLog)
            {
                string nombreArchMapa = Config.get.Mapas.Prefijo +
                  CargaMapa.get.numMapa.ToString();
                log = new EventLog(configIA.Log.formarRutaCompleta(nombreArchMapa));
            }
        }

        public void Close()
        {
            if (log != null)
                log.Close();
        }
    }
}

