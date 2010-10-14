using System;
using System.Windows.Forms;
using System.Drawing;

using SokoVen.Estructura;

namespace SokoVen.Vista {
  /// <summary>
  /// DibujaMapa: Clase que se encarga de dibujar y actualizar el mapa dentro
  ///             del componente "panel" del formulario "FormJuego".
  /// </summary>
  public class DibujaMapa {
    private Form form;
    private Panel panel;
    private int tamImg;
    private ImageList imagenes;
    private readonly Size picBoxSize;

    public DibujaMapa(Form form, Panel panel, int tamImg, ImageList imagenes) {
      this.form = form;
      this.panel = panel;
      this.imagenes = imagenes;
      this.tamImg = tamImg;
      this.picBoxSize = new Size(tamImg, tamImg);
    }

    private PictureBox[,] pictureBoxs;
    public PictureBox this[Point posicion] {
      get {return pictureBoxs[posicion.X, posicion.Y];}
      set {pictureBoxs[posicion.X, posicion.Y] = value;}
    }

   public void dibujaNuevoMapa(Mapa mapa, int posInfUltimoComp) {
      const int BORDE = 12;
      form.SuspendLayout();
      panel.Height = mapa.Alto * tamImg + 2;
      panel.Width = mapa.Ancho * tamImg + 2;
      int altoVent = Math.Max(panel.Bounds.Bottom, posInfUltimoComp);
      form.ClientSize = new Size(panel.Bounds.Right + BORDE, altoVent + BORDE);
      dibujaMapaEntero(mapa);
      form.ResumeLayout(false);
   }


    public void dibujaMapaEntero(Mapa mapa) {
      form.SuspendLayout();
      panel.Controls.Clear();
      this.pictureBoxs = new PictureBox[mapa.Ancho, mapa.Alto];
      for (int y = 0; y < mapa.Alto; y++) {
        for (int x = 0; x < mapa.Ancho ; x++) {
          Point posicion = new Point(x, y);
          PictureBox pictureBox = new PictureBox();
          pictureBox.ClientSize = this.picBoxSize;
          pictureBox.Location = new Point(x * tamImg, y * tamImg);
          int nImagen = getNumImagen(mapa, posicion);
          if (nImagen >= 0)
            pictureBox.Image = this.imagenes.Images[nImagen];
          this.pictureBoxs[x, y] = pictureBox;
          panel.Controls.Add(pictureBox);
        }
      }
      form.ResumeLayout(false);
    }

    public void dibujaMovida(Mapa mapa, Movida movida) {
      form.SuspendLayout();
      foreach (Point posicion in movida.PosAfectadas) {
        int nImagen = getNumImagen(mapa, posicion);
        this.pictureBoxs[posicion.X, posicion.Y].Image = (nImagen >= 0) ?
          imagenes.Images[nImagen] : null;
      }
      form.ResumeLayout(false);
    }

    private int getNumImagen(Mapa mapa, Point posicion) {
      int nImg = -1;
      TipoObjeto obj = mapa.EstadoActual[posicion];
      switch (mapa.Lugares[posicion]) {
        case Lugar.Piso:
          nImg = obj == TipoObjeto.Tipito ? 1 : obj == TipoObjeto.Caja ? 0 : -1;
          break;
        case Lugar.Destino:
          nImg = obj == TipoObjeto.Tipito ? 4 : obj == TipoObjeto.Caja ? 3 : 2;
          break;
        case Lugar.Pared:
          nImg = 5;
          break;
      }
      return nImg;
    }
  }
}

