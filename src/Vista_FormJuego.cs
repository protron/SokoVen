using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using SokoVen.Estructura;
using SokoVen.IO;

namespace SokoVen.Vista
{
    /// <summary>
    /// FormJuego.
    /// </summary>
    public class FormJuego : Form
    {
        public Mapa mapa;
        public Stack listaMovidas;
        public readonly FormCNivel formCNivel = new FormCNivel();
        public readonly FormResolviendo formResol = new FormResolviendo();
        private readonly DibujaMapa dibujaMapa;

        // Variable requerida por el Windows Form Designer.
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ImageList imagenes16;
        private System.Windows.Forms.ImageList imagenes32;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.GroupBox groupBoxNivel;
        private System.Windows.Forms.Button botonCargar;
        private System.Windows.Forms.GroupBox groupBoxMovidas;
        private System.Windows.Forms.Button botonDeshacer;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItemJuego;
        private System.Windows.Forms.MenuItem menuItemAyuda;
        private System.Windows.Forms.MenuItem menuItemCargar;
        private System.Windows.Forms.MenuItem menuItemCambiarNivel;
        private System.Windows.Forms.MenuItem menuItemSalir;
        private System.Windows.Forms.MenuItem menuItemSep1;
        public System.Windows.Forms.Label labelNivel;
        public System.Windows.Forms.Label labelMovidas;
        private System.Windows.Forms.ImageList imagenesBotones;
        private System.Windows.Forms.MenuItem menuItemResolver;

        public FormJuego()
        {
            // Llamada necesaria para soporte del Windows Form Designer.
            InitializeComponent();

            Config.get.cargar();
            formResol.formJuego = this;
            this.dibujaMapa = new DibujaMapa(this, panel, 32, this.imagenes32);
            this.botonCargar_Click(this, null);
        }

        /// <summary>
        /// Limpia los recursos que se estan usando.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.mapa = null;
                this.listaMovidas = null;
                this.formCNivel.Dispose();
                this.formResol.Dispose();
                if (components != null)
                {
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormJuego));
            this.imagenes16 = new System.Windows.Forms.ImageList(this.components);
            this.panel = new System.Windows.Forms.Panel();
            this.imagenes32 = new System.Windows.Forms.ImageList(this.components);
            this.groupBoxNivel = new System.Windows.Forms.GroupBox();
            this.labelNivel = new System.Windows.Forms.Label();
            this.botonCargar = new System.Windows.Forms.Button();
            this.imagenesBotones = new System.Windows.Forms.ImageList(this.components);
            this.groupBoxMovidas = new System.Windows.Forms.GroupBox();
            this.labelMovidas = new System.Windows.Forms.Label();
            this.botonDeshacer = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItemJuego = new System.Windows.Forms.MenuItem();
            this.menuItemCargar = new System.Windows.Forms.MenuItem();
            this.menuItemCambiarNivel = new System.Windows.Forms.MenuItem();
            this.menuItemSep1 = new System.Windows.Forms.MenuItem();
            this.menuItemSalir = new System.Windows.Forms.MenuItem();
            this.menuItemAyuda = new System.Windows.Forms.MenuItem();
            this.menuItemResolver = new System.Windows.Forms.MenuItem();
            this.groupBoxNivel.SuspendLayout();
            this.groupBoxMovidas.SuspendLayout();
            this.SuspendLayout();
            // 
            // imagenes16
            // 
            this.imagenes16.ImageSize = new System.Drawing.Size(16, 16);
            this.imagenes16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagenes16.ImageStream")));
            this.imagenes16.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.White;
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Location = new System.Drawing.Point(96, 12);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(232, 152);
            this.panel.TabIndex = 0;
            // 
            // imagenes32
            // 
            this.imagenes32.ImageSize = new System.Drawing.Size(32, 32);
            this.imagenes32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagenes32.ImageStream")));
            this.imagenes32.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // groupBoxNivel
            // 
            this.groupBoxNivel.Controls.Add(this.labelNivel);
            this.groupBoxNivel.Controls.Add(this.botonCargar);
            this.groupBoxNivel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxNivel.Location = new System.Drawing.Point(8, 8);
            this.groupBoxNivel.Name = "groupBoxNivel";
            this.groupBoxNivel.Size = new System.Drawing.Size(80, 104);
            this.groupBoxNivel.TabIndex = 1;
            this.groupBoxNivel.TabStop = false;
            this.groupBoxNivel.Text = "&Nivel";
            // 
            // labelNivel
            // 
            this.labelNivel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelNivel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labelNivel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelNivel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelNivel.Location = new System.Drawing.Point(8, 16);
            this.labelNivel.Name = "labelNivel";
            this.labelNivel.Size = new System.Drawing.Size(64, 23);
            this.labelNivel.TabIndex = 0;
            this.labelNivel.Text = "1";
            this.labelNivel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelNivel.DoubleClick += new System.EventHandler(this.menuItemCambiarNivel_Click);
            // 
            // botonCargar
            // 
            this.botonCargar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.botonCargar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.botonCargar.ImageIndex = 0;
            this.botonCargar.ImageList = this.imagenesBotones;
            this.botonCargar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.botonCargar.Location = new System.Drawing.Point(8, 40);
            this.botonCargar.Name = "botonCargar";
            this.botonCargar.Size = new System.Drawing.Size(64, 56);
            this.botonCargar.TabIndex = 1;
            this.botonCargar.TabStop = false;
            this.botonCargar.Text = "&Recargar Nivel";
            this.botonCargar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.botonCargar.Click += new System.EventHandler(this.botonCargar_Click);
            this.botonCargar.Enter += new System.EventHandler(this.eventoSacaFoco_Enter);
            // 
            // imagenesBotones
            // 
            this.imagenesBotones.ImageSize = new System.Drawing.Size(22, 22);
            this.imagenesBotones.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagenesBotones.ImageStream")));
            this.imagenesBotones.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // groupBoxMovidas
            // 
            this.groupBoxMovidas.Controls.Add(this.labelMovidas);
            this.groupBoxMovidas.Controls.Add(this.botonDeshacer);
            this.groupBoxMovidas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxMovidas.Location = new System.Drawing.Point(8, 120);
            this.groupBoxMovidas.Name = "groupBoxMovidas";
            this.groupBoxMovidas.Size = new System.Drawing.Size(80, 88);
            this.groupBoxMovidas.TabIndex = 2;
            this.groupBoxMovidas.TabStop = false;
            this.groupBoxMovidas.Text = "&Movidas";
            // 
            // labelMovidas
            // 
            this.labelMovidas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMovidas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labelMovidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelMovidas.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelMovidas.Location = new System.Drawing.Point(8, 16);
            this.labelMovidas.Name = "labelMovidas";
            this.labelMovidas.Size = new System.Drawing.Size(64, 23);
            this.labelMovidas.TabIndex = 0;
            this.labelMovidas.Text = "0";
            this.labelMovidas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // botonDeshacer
            // 
            this.botonDeshacer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.botonDeshacer.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.botonDeshacer.ImageIndex = 1;
            this.botonDeshacer.ImageList = this.imagenesBotones;
            this.botonDeshacer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.botonDeshacer.Location = new System.Drawing.Point(8, 40);
            this.botonDeshacer.Name = "botonDeshacer";
            this.botonDeshacer.Size = new System.Drawing.Size(64, 40);
            this.botonDeshacer.TabIndex = 1;
            this.botonDeshacer.TabStop = false;
            this.botonDeshacer.Text = "D&eshacer";
            this.botonDeshacer.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.botonDeshacer.Click += new System.EventHandler(this.botonDeshacer_Click);
            this.botonDeshacer.Enter += new System.EventHandler(this.eventoSacaFoco_Enter);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemJuego,
            this.menuItemAyuda,
            this.menuItemResolver});
            // 
            // menuItemJuego
            // 
            this.menuItemJuego.Index = 0;
            this.menuItemJuego.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemCargar,
            this.menuItemCambiarNivel,
            this.menuItemSep1,
            this.menuItemSalir});
            this.menuItemJuego.Text = "&Juego";
            // 
            // menuItemCargar
            // 
            this.menuItemCargar.Index = 0;
            this.menuItemCargar.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.menuItemCargar.Text = "&Cargar";
            this.menuItemCargar.Click += new System.EventHandler(this.botonCargar_Click);
            // 
            // menuItemCambiarNivel
            // 
            this.menuItemCambiarNivel.Index = 1;
            this.menuItemCambiarNivel.Shortcut = System.Windows.Forms.Shortcut.F6;
            this.menuItemCambiarNivel.Text = "Cambiar &Nivel ...";
            this.menuItemCambiarNivel.Click += new System.EventHandler(this.menuItemCambiarNivel_Click);
            // 
            // menuItemSep1
            // 
            this.menuItemSep1.Index = 2;
            this.menuItemSep1.Text = "-";
            // 
            // menuItemSalir
            // 
            this.menuItemSalir.Index = 3;
            this.menuItemSalir.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.menuItemSalir.Text = "&Salir";
            this.menuItemSalir.Click += new System.EventHandler(this.menuItemSalir_Click);
            // 
            // menuItemAyuda
            // 
            this.menuItemAyuda.Index = 1;
            this.menuItemAyuda.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.menuItemAyuda.Text = "&Ayuda";
            this.menuItemAyuda.Click += new System.EventHandler(this.menuItemAyuda_Click);
            // 
            // menuItemResolver
            // 
            this.menuItemResolver.Index = 2;
            this.menuItemResolver.Text = "Resolver";
            this.menuItemResolver.Click += new System.EventHandler(this.menuItemResolver_Click);
            // 
            // FormJuego
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(338, 212);
            this.Controls.Add(this.groupBoxMovidas);
            this.Controls.Add(this.groupBoxNivel);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Name = "FormJuego";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SokoVen";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.formJuego_KeyDown);
            this.groupBoxNivel.ResumeLayout(false);
            this.groupBoxMovidas.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        /// <summary>
        /// El punto de entrada principal de la aplicacion.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new FormJuego());
        }

        private void eventoSacaFoco_Enter(object sender, System.EventArgs e)
        {
            panel.Focus();
        }

        private void botonCargar_Click(object sender, System.EventArgs e)
        {
            this.cargaMapa(Int32.Parse(labelNivel.Text));
        }

        public bool cargaMapa(int numMapa)
        {
            this.listaMovidas = new Stack(100);
            try
            {
                this.mapa = CargaMapa.get.cargar(numMapa);
            }
            catch (SokoVen.ErrorAlCargarMapa e)
            {
                MessageBox.Show(e.Message, "Error del Mapa " + e.NumMapa,
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            dibujaMapa.dibujaNuevoMapa(mapa, groupBoxMovidas.Bounds.Bottom);
            this.labelMovidas.Text = listaMovidas.Count.ToString();
            this.labelNivel.Text = numMapa.ToString();
            this.formResol.analizador.Mapa = mapa;
            return true;
        }

        private void formJuego_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.W:
                case Keys.Up: moverTipito(Direccion.arriba); break;
                case Keys.S:
                case Keys.Down: moverTipito(Direccion.abajo); break;
                case Keys.A:
                case Keys.Left: moverTipito(Direccion.izquierda); break;
                case Keys.D:
                case Keys.Right: moverTipito(Direccion.derecha); break;
            }
        }

        private void moverTipito(Direccion direccion)
        {
            try
            {
                Movida movida = Movida.crear(mapa.EstadoActual, direccion);
                listaMovidas.Push(movida);
                mapa.EstadoActual.realizarMovida(movida);
                dibujaMapa.dibujaMovida(mapa, movida);
                this.labelMovidas.Text = listaMovidas.Count.ToString();
            }
            catch (MovidaInvalida)
            {
                return;
            }
            if (mapa.EstadoActual.MapaTerminado())
            {
                MessageBox.Show("Mapa Completo!", "Felicitaciones!",
                  MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void botonDeshacer_Click(object sender, System.EventArgs e)
        {
            if (listaMovidas.Count < 1)
                return;
            Movida movidaQueDeshace =
              Movida.crearDeshacedora((Movida)listaMovidas.Pop());
            mapa.EstadoActual.realizarMovida(movidaQueDeshace);
            dibujaMapa.dibujaMovida(mapa, movidaQueDeshace);
            this.labelMovidas.Text = listaMovidas.Count.ToString();
        }

        private void menuItemSalir_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void menuItemCambiarNivel_Click(object sender, System.EventArgs e)
        {
            this.formCNivel.nivelUpDown.Value = Int32.Parse(this.labelNivel.Text);
            if (this.formCNivel.ShowDialog(this) == DialogResult.OK)
            {
                this.cargaMapa((int)this.formCNivel.nivelUpDown.Value);
            }
        }

        private void menuItemAyuda_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Colocá las cajas sobre las estrellas empujandolas",
              "Instrucciones", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void menuItemResolver_Click(object sender, System.EventArgs e)
        {
            formResol.ShowDialog();
        }
    }
}
