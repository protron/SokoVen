using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SokoVen.Vista {
  /// <summary>
  /// FormCNivel.
  /// </summary>
  public class FormCNivel : System.Windows.Forms.Form {
    // Variable requerida por el Windows Form Designer.
    private System.ComponentModel.Container components = null;
    private System.Windows.Forms.Button botonAceptar;
    private System.Windows.Forms.Button botonCancelar;
    private System.Windows.Forms.Panel panelContenedor;
    public System.Windows.Forms.NumericUpDown nivelUpDown;
    private System.Windows.Forms.Label labelNivel;
    private System.Windows.Forms.Panel panelDivisor;

    public FormCNivel() {
      // Llamada necesaria para soporte del Windows Form Designer.
      InitializeComponent();
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
      this.panelContenedor = new System.Windows.Forms.Panel();
      this.botonCancelar = new System.Windows.Forms.Button();
      this.botonAceptar = new System.Windows.Forms.Button();
      this.panelDivisor = new System.Windows.Forms.Panel();
      this.nivelUpDown = new System.Windows.Forms.NumericUpDown();
      this.labelNivel = new System.Windows.Forms.Label();
      this.panelContenedor.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nivelUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // panelContenedor
      // 
      this.panelContenedor.Controls.Add(this.botonCancelar);
      this.panelContenedor.Controls.Add(this.botonAceptar);
      this.panelContenedor.Controls.Add(this.panelDivisor);
      this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panelContenedor.DockPadding.All = 8;
      this.panelContenedor.Location = new System.Drawing.Point(0, 50);
      this.panelContenedor.Name = "panelContenedor";
      this.panelContenedor.Size = new System.Drawing.Size(154, 60);
      this.panelContenedor.TabIndex = 4;
      // 
      // botonCancelar
      // 
      this.botonCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.botonCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.botonCancelar.Location = new System.Drawing.Point(80, 20);
      this.botonCancelar.Name = "botonCancelar";
      this.botonCancelar.Size = new System.Drawing.Size(64, 32);
      this.botonCancelar.TabIndex = 4;
      this.botonCancelar.Text = "&Cancelar";
      // 
      // botonAceptar
      // 
      this.botonAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.botonAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.botonAceptar.Location = new System.Drawing.Point(8, 20);
      this.botonAceptar.Name = "botonAceptar";
      this.botonAceptar.Size = new System.Drawing.Size(64, 32);
      this.botonAceptar.TabIndex = 3;
      this.botonAceptar.Text = "&Aceptar";
      // 
      // panelDivisor
      // 
      this.panelDivisor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panelDivisor.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelDivisor.Location = new System.Drawing.Point(8, 8);
      this.panelDivisor.Name = "panelDivisor";
      this.panelDivisor.Size = new System.Drawing.Size(138, 4);
      this.panelDivisor.TabIndex = 0;
      // 
      // nivelUpDown
      // 
      this.nivelUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nivelUpDown.Location = new System.Drawing.Point(80, 8);
      this.nivelUpDown.Maximum = new System.Decimal(new int[] {
            40,
            0,
            0,
            0});
      this.nivelUpDown.Minimum = new System.Decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nivelUpDown.Name = "nivelUpDown";
      this.nivelUpDown.Size = new System.Drawing.Size(64, 35);
      this.nivelUpDown.TabIndex = 1;
      this.nivelUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.nivelUpDown.Value = new System.Decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // labelNivel
      // 
      this.labelNivel.AutoSize = true;
      this.labelNivel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.labelNivel.Location = new System.Drawing.Point(8, 12);
      this.labelNivel.Name = "labelNivel";
      this.labelNivel.Size = new System.Drawing.Size(71, 31);
      this.labelNivel.TabIndex = 0;
      this.labelNivel.Text = "&Nivel:";
      // 
      // FormCNivel
      // 
      this.AcceptButton = this.botonAceptar;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.botonCancelar;
      this.ClientSize = new System.Drawing.Size(154, 110);
      this.ControlBox = false;
      this.Controls.Add(this.labelNivel);
      this.Controls.Add(this.nivelUpDown);
      this.Controls.Add(this.panelContenedor);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "FormCNivel";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Cambiar Nivel";
      this.Activated += new System.EventHandler(this.FormCNivel_Activated);
      this.panelContenedor.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.nivelUpDown)).EndInit();
      this.ResumeLayout(false);
    }
    #endregion
    
    private void FormCNivel_Activated(object sender, System.EventArgs e) {
      this.ActiveControl =  nivelUpDown;
      nivelUpDown.Focus();
      nivelUpDown.Select(0, nivelUpDown.Text.Length);
    }
  }
}

