/*
 * Created by SharpDevelop.
 * User: compu1
 * Date: 24/11/2025
 * Time: 09:32 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace proyectoInventario
{
    partial class RegistrarUsuarios
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
       System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrarUsuarios));
  this.lblTitleMain = new System.Windows.Forms.Label();
    this.txtIDUsuario = new System.Windows.Forms.TextBox();
    this.txtNombreUsuario = new System.Windows.Forms.TextBox();
    this.txtContraseña = new System.Windows.Forms.TextBox();
    this.cmbRol = new System.Windows.Forms.ComboBox();
    this.btnRegistrarCompra = new System.Windows.Forms.Button();
    this.btnVolver = new System.Windows.Forms.Button();
    this.lblIDUsuario = new System.Windows.Forms.Label();
    this.lblNombreUsuario = new System.Windows.Forms.Label();
    this.lblContraseña = new System.Windows.Forms.Label();
    this.lblRol = new System.Windows.Forms.Label();
    this.SuspendLayout();
    // 
    // lblTitleMain
    // 
    this.lblTitleMain.AutoSize = true;
    this.lblTitleMain.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
    this.lblTitleMain.ForeColor = System.Drawing.Color.White;
    this.lblTitleMain.Location = new System.Drawing.Point(108, 34);
  this.lblTitleMain.Name = "lblTitleMain";
    this.lblTitleMain.Size = new System.Drawing.Size(172, 25);
    this.lblTitleMain.TabIndex = 5;
    this.lblTitleMain.Text = "Registrar Usuarios";
    // 
    // lblIDUsuario
  // 
    this.lblIDUsuario.AutoSize = true;
    this.lblIDUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.lblIDUsuario.ForeColor = System.Drawing.Color.White;
    this.lblIDUsuario.Location = new System.Drawing.Point(47, 65);
    this.lblIDUsuario.Name = "lblIDUsuario";
    this.lblIDUsuario.Size = new System.Drawing.Size(83, 15);
    this.lblIDUsuario.TabIndex = 15;
    this.lblIDUsuario.Text = "ID de Usuario:";
    // 
    // txtIDUsuario
    // 
    this.txtIDUsuario.Location = new System.Drawing.Point(47, 83);
 this.txtIDUsuario.Name = "txtIDUsuario";
    this.txtIDUsuario.Size = new System.Drawing.Size(280, 20);
    this.txtIDUsuario.TabIndex = 6;
    // 
    // lblNombreUsuario
    // 
    this.lblNombreUsuario.AutoSize = true;
    this.lblNombreUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.lblNombreUsuario.ForeColor = System.Drawing.Color.White;
    this.lblNombreUsuario.Location = new System.Drawing.Point(47, 106);
 this.lblNombreUsuario.Name = "lblNombreUsuario";
    this.lblNombreUsuario.Size = new System.Drawing.Size(119, 15);
    this.lblNombreUsuario.TabIndex = 16;
    this.lblNombreUsuario.Text = "Nombre de Usuario:";
    // 
    // txtNombreUsuario
    // 
    this.txtNombreUsuario.Location = new System.Drawing.Point(47, 124);
    this.txtNombreUsuario.Name = "txtNombreUsuario";
    this.txtNombreUsuario.Size = new System.Drawing.Size(280, 20);
    this.txtNombreUsuario.TabIndex = 7;
    // 
    // lblContraseña
    // 
    this.lblContraseña.AutoSize = true;
    this.lblContraseña.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.lblContraseña.ForeColor = System.Drawing.Color.White;
    this.lblContraseña.Location = new System.Drawing.Point(47, 147);
    this.lblContraseña.Name = "lblContraseña";
    this.lblContraseña.Size = new System.Drawing.Size(73, 15);
    this.lblContraseña.TabIndex = 17;
  this.lblContraseña.Text = "Contraseña:";
  // 
    // txtContraseña
    // 
    this.txtContraseña.Location = new System.Drawing.Point(47, 165);
  this.txtContraseña.Name = "txtContraseña";
  this.txtContraseña.PasswordChar = '*';
    this.txtContraseña.Size = new System.Drawing.Size(280, 20);
    this.txtContraseña.TabIndex = 8;
    // 
    // lblRol
    // 
    this.lblRol.AutoSize = true;
    this.lblRol.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.lblRol.ForeColor = System.Drawing.Color.White;
    this.lblRol.Location = new System.Drawing.Point(47, 188);
 this.lblRol.Name = "lblRol";
    this.lblRol.Size = new System.Drawing.Size(26, 15);
    this.lblRol.TabIndex = 18;
    this.lblRol.Text = "Rol:";
    // 
    // cmbRol
    // 
    this.cmbRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
    this.cmbRol.FormattingEnabled = true;
    this.cmbRol.Items.AddRange(new object[] {
    "Administrador",
    "Vendedor"});
    this.cmbRol.Location = new System.Drawing.Point(47, 206);
    this.cmbRol.Name = "cmbRol";
    this.cmbRol.Size = new System.Drawing.Size(280, 21);
    this.cmbRol.TabIndex = 9;
    // 
    // btnRegistrarCompra
    // 
    this.btnRegistrarCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(75)))), ((int)(((byte)(255)))));
    this.btnRegistrarCompra.Cursor = System.Windows.Forms.Cursors.Hand;
    this.btnRegistrarCompra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.btnRegistrarCompra.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.btnRegistrarCompra.ForeColor = System.Drawing.Color.White;
    this.btnRegistrarCompra.Location = new System.Drawing.Point(110, 243);
    this.btnRegistrarCompra.Name = "btnRegistrarCompra";
  this.btnRegistrarCompra.Size = new System.Drawing.Size(155, 38);
    this.btnRegistrarCompra.TabIndex = 10;
    this.btnRegistrarCompra.Text = "Registrar Usuario";
    this.btnRegistrarCompra.UseVisualStyleBackColor = false;
    // 
    // btnVolver
    // 
    this.btnVolver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(50)))), ((int)(((byte)(86)))));
    this.btnVolver.Cursor = System.Windows.Forms.Cursors.Hand;
    this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.btnVolver.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.btnVolver.ForeColor = System.Drawing.Color.White;
    this.btnVolver.Location = new System.Drawing.Point(12, 12);
    this.btnVolver.Name = "btnVolver";
    this.btnVolver.Size = new System.Drawing.Size(75, 38);
    this.btnVolver.TabIndex = 14;
    this.btnVolver.Text = "Volver";
    this.btnVolver.UseVisualStyleBackColor = false;
    this.btnVolver.Click += new System.EventHandler(this.BtnVolver_Click);
    // 
    // RegistrarUsuarios
    // 
    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(13)))), ((int)(((byte)(32)))));
    this.ClientSize = new System.Drawing.Size(375, 320);
    this.Controls.Add(this.lblRol);
    this.Controls.Add(this.lblContraseña);
    this.Controls.Add(this.lblNombreUsuario);
    this.Controls.Add(this.lblIDUsuario);
    this.Controls.Add(this.btnVolver);
    this.Controls.Add(this.btnRegistrarCompra);
    this.Controls.Add(this.cmbRol);
    this.Controls.Add(this.txtContraseña);
    this.Controls.Add(this.txtNombreUsuario);
    this.Controls.Add(this.txtIDUsuario);
    this.Controls.Add(this.lblTitleMain);
    this.Name = "RegistrarUsuarios";
    this.Text = "Registrar Usuarios";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
     private System.Windows.Forms.Button btnVolver;
 private System.Windows.Forms.Button btnRegistrarCompra;
        private System.Windows.Forms.ComboBox cmbRol;
    private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.TextBox txtIDUsuario;
  private System.Windows.Forms.Label lblTitleMain;
private System.Windows.Forms.Label lblIDUsuario;
private System.Windows.Forms.Label lblNombreUsuario;
private System.Windows.Forms.Label lblContraseña;
private System.Windows.Forms.Label lblRol;
  }
}
