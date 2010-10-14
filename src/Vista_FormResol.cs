using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;

using SokoVen.Estructura;
using SokoVen.IA;

namespace SokoVen.Vista {
  /// <summary>
  /// FormResolviendo.
  /// </summary>
  public class FormResolviendo : System.Windows.Forms.Form {
    public Analizador analizador;
    public FormJuego formJuego;

    // Variable requerida por el Windows Form Designer.
    private System.ComponentModel.Container components = null;
    private System.Windows.Forms.Label labelEspere;
    private System.Windows.Forms.Button botonOK;
    private System.Windows.Forms.RichTextBox richTBox;

    public FormResolviendo() {
      // Llamada necesaria para soporte del Windows Form Designer.
      InitializeComponent();

      this.analizador = new Analizador();
    }

    /// <summary>
    /// Limpia los recursos que se estan usando.
    /// </summary>
    protected override void Dispose (bool disposing) {
      if (disposing) {
        if (components != null) {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Metodo necesario para el Windows Form Designer.
    /// No modificar el contenido del metodo con el editor de codigo.
    /// </summary>
    private void InitializeComponent() {
      this.labelEspere = new System.Windows.Forms.Label();
      this.botonOK = new System.Windows.Forms.Button();
      this.richTBox = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // labelEspere
      // 
      this.labelEspere.AutoSize = true;
      this.labelEspere.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.labelEspere.Location = new System.Drawing.Point(74, 19);
      this.labelEspere.Name = "labelEspere";
      this.labelEspere.TabIndex = 0;
      this.labelEspere.Text = "Espere por favor ...";
      this.labelEspere.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // botonOK
      // 
      this.botonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.botonOK.Location = new System.Drawing.Point(32, 32);
      this.botonOK.Name = "botonOK";
      this.botonOK.TabIndex = 1;
      this.botonOK.Text = "&Aceptar";
      this.botonOK.Visible = false;
      // 
      // richTBox
      // 
      this.richTBox.AutoSize = true;
      this.richTBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.richTBox.Location = new System.Drawing.Point(8, 8);
      this.richTBox.Name = "richTBox";
      this.richTBox.ReadOnly = true;
      this.richTBox.Size = new System.Drawing.Size(488, 160);
      this.richTBox.TabIndex = 2;
      this.richTBox.Text = "123456789012345678901234567890123456789012345678901234567890123456" +  
        "7890123456789\n2\n3\n4\n5\n6\n7\n8\n9\n10";
      this.richTBox.Visible = false;
      this.richTBox.WordWrap = false;
      // 
      // FormResolviendo
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(234, 56);
      this.Controls.Add(this.labelEspere);
      this.Controls.Add(this.botonOK);
      this.Controls.Add(this.richTBox);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormResolviendo";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Resolviendo";
      this.Load += new System.EventHandler(this.FormResolviendo_Load);
      this.ResumeLayout(false);
    }
    #endregion

    private void FormResolviendo_Load(object sender, System.EventArgs e) {
      this.ResumeLayout(true);
      DateTime tiempoInicio = DateTime.Now;
      Queue movidas = Resolvedor.resolver(this.analizador, formJuego.mapa.EstadoActual);
      TimeSpan lapsoRes = (TimeSpan)(DateTime.Now - tiempoInicio);
      StringBuilder mensaje = new StringBuilder(80, 805);
      mensaje.Append("Tiempo de resolución: ");
      mensaje.Append(lapsoRes.ToString());
      mensaje.Append('\n');
      int sobranteLargo = mensaje.Length;
      mensaje.Append(movidas.Count.ToString());
      mensaje.Append(" movidas: ");
      foreach (Movida movida in movidas) {
        if (mensaje.Length - sobranteLargo >= 79) {
          if (mensaje.Length >= mensaje.MaxCapacity - 10) {
            mensaje.Insert(mensaje.Length - 4, " ...");
            break;
          }
          mensaje.Append('\n');
          sobranteLargo = mensaje.Length;
        } else {
          mensaje.Append(" ");
        }
        mensaje.Append(movida.Direccion.ToString());
      }
      this.richTBox.Text = mensaje.ToString();
      this.Text = "Solucion";
      this.botonOK.Location = new System.Drawing.Point(richTBox.Left +
        (richTBox.Width - botonOK.Width) / 2, richTBox.Bounds.Bottom + 8);
      this.Height = botonOK.Bottom + 40;
      this.Width = richTBox.Bounds.Right + 12;
      this.richTBox.Visible = true;
      this.botonOK.Visible = true;
      this.labelEspere.Visible = false;
    }
  }
}

