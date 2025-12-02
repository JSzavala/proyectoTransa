/*
 * Created by SharpDevelop.
 * User: compu1
 * Date: 24/11/2025
 * Time: 07:59 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace proyectoInventario
{
 partial class Main
  {
      /// <summary>
        /// Designer variable used to keep track of non-visual components.
  /// </summary>
        private System.ComponentModel.IContainer components = null;
   
   /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
     {
if (disposing) {
      if (components != null) {
   components.Dispose();
       }
            }
            base.Dispose(disposing);
      }
    
    /// <summary>
   /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
   /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitleMain = new System.Windows.Forms.Label();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.btnRegistrarVenta = new System.Windows.Forms.Button();
            this.btnReporteVentas = new System.Windows.Forms.Button();
            this.btnReporteLibros = new System.Windows.Forms.Button();
            this.btnListaCambios = new System.Windows.Forms.Button();
            this.btnMonitoreo = new System.Windows.Forms.Button();
            this.lblHora = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblTitleMain
            // 
            this.lblTitleMain.AutoSize = true;
            this.lblTitleMain.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleMain.ForeColor = System.Drawing.Color.White;
            this.lblTitleMain.Location = new System.Drawing.Point(101, 18);
            this.lblTitleMain.Name = "lblTitleMain";
            this.lblTitleMain.Size = new System.Drawing.Size(114, 24);
            this.lblTitleMain.TabIndex = 2;
            this.lblTitleMain.Text = "Bienvenido";
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.Crimson;
            this.btnCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(101, 391);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(106, 25);
            this.btnCerrarSesion.TabIndex = 8;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.BtnCerrarSesion_Click);
            // 
            // btnRegistrarVenta
            // 
            this.btnRegistrarVenta.BackColor = System.Drawing.Color.Blue;
            this.btnRegistrarVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrarVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarVenta.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarVenta.ForeColor = System.Drawing.Color.White;
            this.btnRegistrarVenta.Location = new System.Drawing.Point(101, 91);
            this.btnRegistrarVenta.Name = "btnRegistrarVenta";
            this.btnRegistrarVenta.Size = new System.Drawing.Size(107, 40);
            this.btnRegistrarVenta.TabIndex = 12;
            this.btnRegistrarVenta.Text = "Registrar Venta";
            this.btnRegistrarVenta.UseVisualStyleBackColor = false;
            this.btnRegistrarVenta.Click += new System.EventHandler(this.BtnRegistrarVenta_Click);
            // 
            // btnReporteVentas
            // 
            this.btnReporteVentas.BackColor = System.Drawing.Color.Blue;
            this.btnReporteVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReporteVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporteVentas.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporteVentas.ForeColor = System.Drawing.Color.White;
            this.btnReporteVentas.Location = new System.Drawing.Point(101, 149);
            this.btnReporteVentas.Name = "btnReporteVentas";
            this.btnReporteVentas.Size = new System.Drawing.Size(107, 40);
            this.btnReporteVentas.TabIndex = 13;
            this.btnReporteVentas.Text = "Reporte de Ventas Empleado";
            this.btnReporteVentas.UseVisualStyleBackColor = false;
            this.btnReporteVentas.Click += new System.EventHandler(this.btnReporteVentas_Click);
            // 
            // btnReporteLibros
            // 
            this.btnReporteLibros.BackColor = System.Drawing.Color.Blue;
            this.btnReporteLibros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReporteLibros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporteLibros.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporteLibros.ForeColor = System.Drawing.Color.White;
            this.btnReporteLibros.Location = new System.Drawing.Point(101, 206);
            this.btnReporteLibros.Name = "btnReporteLibros";
            this.btnReporteLibros.Size = new System.Drawing.Size(107, 40);
            this.btnReporteLibros.TabIndex = 14;
            this.btnReporteLibros.Text = "Reporte de Libros Vendidos";
            this.btnReporteLibros.UseVisualStyleBackColor = false;
            this.btnReporteLibros.Click += new System.EventHandler(this.btnReporteLibros_Click);
            // 
            // btnListaCambios
            // 
            this.btnListaCambios.BackColor = System.Drawing.Color.Blue;
            this.btnListaCambios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnListaCambios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnListaCambios.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListaCambios.ForeColor = System.Drawing.Color.White;
            this.btnListaCambios.Location = new System.Drawing.Point(101, 264);
            this.btnListaCambios.Name = "btnListaCambios";
            this.btnListaCambios.Size = new System.Drawing.Size(107, 40);
            this.btnListaCambios.TabIndex = 15;
            this.btnListaCambios.Text = "Lista de Cambios";
            this.btnListaCambios.UseVisualStyleBackColor = false;
            this.btnListaCambios.Click += new System.EventHandler(this.btnListaCambios_Click);
            // 
            // btnMonitoreo
            // 
            this.btnMonitoreo.BackColor = System.Drawing.Color.Blue;
            this.btnMonitoreo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMonitoreo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMonitoreo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonitoreo.ForeColor = System.Drawing.Color.White;
            this.btnMonitoreo.Location = new System.Drawing.Point(101, 320);
            this.btnMonitoreo.Name = "btnMonitoreo";
            this.btnMonitoreo.Size = new System.Drawing.Size(107, 40);
            this.btnMonitoreo.TabIndex = 16;
            this.btnMonitoreo.Text = "Monitoreo de Productos";
            this.btnMonitoreo.UseVisualStyleBackColor = false;
            this.btnMonitoreo.Click += new System.EventHandler(this.btnMonitoreo_Click);
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.ForeColor = System.Drawing.Color.White;
            this.lblHora.Location = new System.Drawing.Point(117, 51);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(35, 13);
            this.lblHora.TabIndex = 17;
            this.lblHora.Text = "label1";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.ForeColor = System.Drawing.Color.White;
            this.lblFecha.Location = new System.Drawing.Point(158, 51);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(35, 13);
            this.lblFecha.TabIndex = 18;
            this.lblFecha.Text = "label2";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(314, 442);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblHora);
            this.Controls.Add(this.btnMonitoreo);
            this.Controls.Add(this.btnListaCambios);
            this.Controls.Add(this.btnReporteLibros);
            this.Controls.Add(this.btnReporteVentas);
            this.Controls.Add(this.btnRegistrarVenta);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.lblTitleMain);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);
            this.PerformLayout();

      }
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Label lblTitleMain;
        private System.Windows.Forms.Button btnRegistrarVenta;
        private System.Windows.Forms.Button btnReporteVentas;
        private System.Windows.Forms.Button btnReporteLibros;
        private System.Windows.Forms.Button btnListaCambios;
        private System.Windows.Forms.Button btnMonitoreo;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Timer timer1;
    }
}
