using System;
using System.Xml;

namespace SokoVen.IO {
  /// <summary>
  /// Config: Clase que guarda la configuración del programa, esta configuración
  ///         es cargada por esta misma clase desde el archivo "config.xml".
  /// </summary>
  public class Config {
    private readonly static Config singleton = new Config();
    private Config() {}
    public static Config get {
      get {return singleton;}
    }

    private ConfigArchivos mapas =
      new ConfigArchivos("Mapas", "Nivel", ".txt");
    public ConfigArchivos Mapas {
      get {return mapas;}
    }

    private ConfigIA ia = new ConfigIA();
    public ConfigIA IA {
      get {return ia;}
    }

    const string archivoConfig = "config.xml";
    const string primerElemento = "config_sokoven";

    public bool cargar() {
      XmlTextReader reader = new XmlTextReader(archivoConfig);
      try {
        try {
          reader.MoveToContent();
        } catch(Exception) {
          return false;
        }
        if (reader.Name != primerElemento)
          throw new ErrorSokoVen("El archivo " + archivoConfig +
            " debería empezar con: <" + primerElemento + '>');
        while (reader.Read()) {
          switch (reader.MoveToContent()) {
            case XmlNodeType.Element:
              leeElemento(reader);
              break;
          }
        }
      } finally {
        if (reader != null)
          reader.Close();
      }
      return true;
    }

    private bool leeElemento(XmlTextReader reader) {
      switch (reader.Name) {
        case "mapas":
          mapas.cargarDesdeXML(reader);
          break;
        case "inteligencia_artificial":
          if (reader.MoveToAttribute("grabar_log"))
            IA.GrabarLog = bool.Parse(reader.Value);
          if (reader.Read())
            if (reader.MoveToContent() == XmlNodeType.Element &&
              reader.Name == "log") {
              IA.Log.cargarDesdeXML(reader);;
            }
          break;
        default:
          reader.Skip();
          return false;
      }
      return true;
    }
  }

  public class ConfigArchivos {
    public ConfigArchivos(string directorio, string prefijo,
        string extension) {
      this.Directorio = directorio;
      this.Prefijo = prefijo;
      this.Extension = extension;
    }

    const string barra = @"\";

    private string directorio;
    public string Directorio {
      get {return directorio;}
      set {
        directorio = value;
        if (directorio.Length >= 1)
          if (!directorio.EndsWith(barra))
            directorio += barra;
      }
    }

    private string prefijo;
    public string Prefijo {
      get {return prefijo;}
      set {prefijo = value;}
    }

    private string extension;
    public string Extension {
      get {return extension;}
      set {extension = value;}
    }

    public string formarRutaCompleta(string medio) {
      return this.Directorio + this.Prefijo + medio + this.Extension;
    }

    public void cargarDesdeXML(XmlTextReader reader) {
      if (reader.MoveToAttribute("directorio"))
        this.Directorio = reader.Value;
      if (reader.MoveToAttribute("prefijo"))
        this.Prefijo = reader.Value;
      if (reader.MoveToAttribute("extension"))
        this.Extension = reader.Value;
    }
  }

  public class ConfigIA {
    public ConfigIA() {}

    private bool grabarLog = true;
    public bool GrabarLog {
      get {return grabarLog;}
      set {grabarLog = value;}
    }

    private ConfigArchivos log =
      new ConfigArchivos("", "", "-IA.log");
    public ConfigArchivos Log {
      get {return log;}
    }
  }
}

