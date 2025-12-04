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
    this.txtNombreUsuario = new System.Windows.Forms.TextBox();
    this.txtContraseña = new System.Windows.Forms.TextBox();
    this.cmbRol = new System.Windows.Forms.ComboBox();
    this.btnRegistrarCompra = new System.Windows.Forms.Button();
    this.btnVolver = new System.Windows.Forms.Button();
    this.lblNombreUsuario = new System.Windows.Forms.Label();
    this.lblContraseña = new System.Windows.Forms.Label();
    this.lblRol = new System.Windows.Forms.Label();
    this.txtNombre = new System.Windows.Forms.TextBox();
    this.lblNombre = new System.Windows.Forms.Label();
    this.txtApellidoPaterno = new System.Windows.Forms.TextBox();
    this.lblApellidoPaterno = new System.Windows.Forms.Label();
    this.txtApellidoMaterno = new System.Windows.Forms.TextBox();
    this.lblApellidoMaterno = new System.Windows.Forms.Label();
    this.txtRFC = new System.Windows.Forms.TextBox();
    this.lblRFC = new System.Windows.Forms.Label();
    this.lblSeccionEmpleado = new System.Windows.Forms.Label();
    this.lblSeccionUsuario = new System.Windows.Forms.Label();
    this.SuspendLayout();
    // 
    // lblTitleMain
    // 
    this.lblTitleMain.AutoSize = true;
    this.lblTitleMain.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
    this.lblTitleMain.ForeColor = System.Drawing.Color.White;
    this.lblTitleMain.Location = new System.Drawing.Point(88, 20);
  this.lblTitleMain.Name = "lblTitleMain";
    this.lblTitleMain.Size = new System.Drawing.Size(224, 25);
  this.lblTitleMain.TabIndex = 5;
    this.lblTitleMain.Text = "Registrar Empleado/Usuario";
    // 
    // lblSeccionEmpleado
    // 
    this.lblSeccionEmpleado.AutoSize = true;
    this.lblSeccionEmpleado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
    this.lblSeccionEmpleado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(75)))), ((int)(((byte)(255)))));
    this.lblSeccionEmpleado.Location = new System.Drawing.Point(30, 55);
    this.lblSeccionEmpleado.Name = "lblSeccionEmpleado";
    this.lblSeccionEmpleado.Size = new System.Drawing.Size(141, 19);
    this.lblSeccionEmpleado.TabIndex = 30;
    this.lblSeccionEmpleado.Text = "Datos del Empleado";
    // 
    // lblNombre
    // 
    this.lblNombre.AutoSize = true;
    this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.lblNombre.ForeColor = System.Drawing.Color.White;
    this.lblNombre.Location = new System.Drawing.Point(30, 80);
    this.lblNombre.Name = "lblNombre";
    this.lblNombre.Size = new System.Drawing.Size(54, 15);
    this.lblNombre.TabIndex = 31;
 this.lblNombre.Text = "Nombre:";
    // 
    // txtNombre
    // 
    this.txtNombre.Location = new System.Drawing.Point(30, 98);
 this.txtNombre.Name = "txtNombre";
    this.txtNombre.Size = new System.Drawing.Size(340, 20);
    this.txtNombre.TabIndex = 0;
    // 
    // lblApellidoPaterno
  // 
    this.lblApellidoPaterno.AutoSize = true;
    this.lblApellidoPaterno.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.lblApellidoPaterno.ForeColor = System.Drawing.Color.White;
    this.lblApellidoPaterno.Location = new System.Drawing.Point(30, 125);
 this.lblApellidoPaterno.Name = "lblApellidoPaterno";
    this.lblApellidoPaterno.Size = new System.Drawing.Size(102, 15);
  this.lblApellidoPaterno.TabIndex = 32;
 this.lblApellidoPaterno.Text = "Apellido Paterno:";
    // 
    // txtApellidoPaterno
    // 
    this.txtApellidoPaterno.Location = new System.Drawing.Point(30, 143);
    this.txtApellidoPaterno.Name = "txtApellidoPaterno";
    this.txtApellidoPaterno.Size = new System.Drawing.Size(340, 20);
    this.txtApellidoPaterno.TabIndex = 1;
  // 
    // lblApellidoMaterno
    // 
    this.lblApellidoMaterno.AutoSize = true;
    this.lblApellidoMaterno.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.lblApellidoMaterno.ForeColor = System.Drawing.Color.White;
    this.lblApellidoMaterno.Location = new System.Drawing.Point(30, 170);
    this.lblApellidoMaterno.Name = "lblApellidoMaterno";
    this.lblApellidoMaterno.Size = new System.Drawing.Size(134, 15);
    this.lblApellidoMaterno.TabIndex = 33;
    this.lblApellidoMaterno.Text = "Apellido Materno (opc):";
    // 
    // txtApellidoMaterno
    // 
    this.txtApellidoMaterno.Location = new System.Drawing.Point(30, 188);
  this.txtApellidoMaterno.Name = "txtApellidoMaterno";
    this.txtApellidoMaterno.Size = new System.Drawing.Size(340, 20);
 this.txtApellidoMaterno.TabIndex = 2;
    // 
    // lblRFC
    // 
    this.lblRFC.AutoSize = true;
    this.lblRFC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.lblRFC.ForeColor = System.Drawing.Color.White;
    this.lblRFC.Location = new System.Drawing.Point(30, 215);
 this.lblRFC.Name = "lblRFC";
    this.lblRFC.Size = new System.Drawing.Size(63, 15);
    this.lblRFC.TabIndex = 34;
    this.lblRFC.Text = "RFC (opc):";
    // 
    // txtRFC
    // 
    this.txtRFC.Location = new System.Drawing.Point(30, 233);
    this.txtRFC.MaxLength = 13;
    this.txtRFC.Name = "txtRFC";
    this.txtRFC.Size = new System.Drawing.Size(340, 20);
    this.txtRFC.TabIndex = 3;
    // 
    // lblSeccionUsuario
    // 
    this.lblSeccionUsuario.AutoSize = true;
    this.lblSeccionUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
 this.lblSeccionUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(75)))), ((int)(((byte)(255)))));
    this.lblSeccionUsuario.Location = new System.Drawing.Point(30, 270);
    this.lblSeccionUsuario.Name = "lblSeccionUsuario";
    this.lblSeccionUsuario.Size = new System.Drawing.Size(131, 19);
    this.lblSeccionUsuario.TabIndex = 35;
  this.lblSeccionUsuario.Text = "Datos del Usuario";
 // 
    // lblNombreUsuario
    // 
    this.lblNombreUsuario.AutoSize = true;
    this.lblNombreUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.lblNombreUsuario.ForeColor = System.Drawing.Color.White;
    this.lblNombreUsuario.Location = new System.Drawing.Point(30, 295);
 this.lblNombreUsuario.Name = "lblNombreUsuario";
    this.lblNombreUsuario.Size = new System.Drawing.Size(119, 15);
 this.lblNombreUsuario.TabIndex = 16;
    this.lblNombreUsuario.Text = "Nombre de Usuario:";
    // 
// txtNombreUsuario
    // 
    this.txtNombreUsuario.Location = new System.Drawing.Point(30, 313);
    this.txtNombreUsuario.Name = "txtNombreUsuario";
    this.txtNombreUsuario.Size = new System.Drawing.Size(340, 20);
  this.txtNombreUsuario.TabIndex = 4;
 // 
    // lblContraseña
    // 
    this.lblContraseña.AutoSize = true;
this.lblContraseña.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.lblContraseña.ForeColor = System.Drawing.Color.White;
    this.lblContraseña.Location = new System.Drawing.Point(30, 340);
    this.lblContraseña.Name = "lblContraseña";
    this.lblContraseña.Size = new System.Drawing.Size(73, 15);
    this.lblContraseña.TabIndex = 17;
  this.lblContraseña.Text = "Contraseña:";
  // 
    // txtContraseña
    // 
    this.txtContraseña.Location = new System.Drawing.Point(30, 358);
  this.txtContraseña.Name = "txtContraseña";
  this.txtContraseña.PasswordChar = '*';
    this.txtContraseña.Size = new System.Drawing.Size(340, 20);
    this.txtContraseña.TabIndex = 5;
    // 
    // lblRol
    // 
    this.lblRol.AutoSize = true;
    this.lblRol.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.lblRol.ForeColor = System.Drawing.Color.White;
    this.lblRol.Location = new System.Drawing.Point(30, 385);
 this.lblRol.Name = "lblRol";
    this.lblRol.Size = new System.Drawing.Size(26, 15);
    this.lblRol.TabIndex = 18;
    this.lblRol.Text = "Rol:";
    // 
    // cmbRol
    // 
    this.cmbRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
    this.cmbRol.FormattingEnabled = true;
    this.cmbRol.Location = new System.Drawing.Point(30, 403);
    this.cmbRol.Name = "cmbRol";
    this.cmbRol.Size = new System.Drawing.Size(340, 21);
    this.cmbRol.TabIndex = 6;
    // 
    // btnRegistrarCompra
    // 
    this.btnRegistrarCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(75)))), ((int)(((byte)(255)))));
    this.btnRegistrarCompra.Cursor = System.Windows.Forms.Cursors.Hand;
    this.btnRegistrarCompra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.btnRegistrarCompra.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.btnRegistrarCompra.ForeColor = System.Drawing.Color.White;
    this.btnRegistrarCompra.Location = new System.Drawing.Point(100, 445);
    this.btnRegistrarCompra.Name = "btnRegistrarCompra";
  this.btnRegistrarCompra.Size = new System.Drawing.Size(200, 38);
    this.btnRegistrarCompra.TabIndex = 7;
    this.btnRegistrarCompra.Text = "Registrar Empleado/Usuario";
    this.btnRegistrarCompra.UseVisualStyleBackColor = false;
    this.btnRegistrarCompra.Click += new System.EventHandler(this.BtnRegistrarUsuario_Click);
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
this.btnVolver.TabIndex = 8;
    this.btnVolver.Text = "Volver";
    this.btnVolver.UseVisualStyleBackColor = false;
    this.btnVolver.Click += new System.EventHandler(this.BtnVolver_Click);
    // 
    // RegistrarUsuarios
    // 
    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    this.AutoScroll = true;
    this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(13)))), ((int)(((byte)(32)))));
    this.ClientSize = new System.Drawing.Size(420, 520);
    this.Controls.Add(this.lblSeccionUsuario);
    this.Controls.Add(this.lblRFC);
    this.Controls.Add(this.txtRFC);
    this.Controls.Add(this.lblApellidoMaterno);
    this.Controls.Add(this.txtApellidoMaterno);
    this.Controls.Add(this.lblApellidoPaterno);
    this.Controls.Add(this.txtApellidoPaterno);
 this.Controls.Add(this.lblNombre);
 this.Controls.Add(this.txtNombre);
    this.Controls.Add(this.lblSeccionEmpleado);
    this.Controls.Add(this.lblRol);
    this.Controls.Add(this.lblContraseña);
    this.Controls.Add(this.lblNombreUsuario);
    this.Controls.Add(this.btnVolver);
    this.Controls.Add(this.btnRegistrarCompra);
    this.Controls.Add(this.cmbRol);
    this.Controls.Add(this.txtContraseña);
    this.Controls.Add(this.txtNombreUsuario);
  this.Controls.Add(this.lblTitleMain);
    this.Name = "RegistrarUsuarios";
    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
    this.Text = "Registrar Empleado/Usuario";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
   private System.Windows.Forms.Button btnVolver;
 private System.Windows.Forms.Button btnRegistrarCompra;
        private System.Windows.Forms.ComboBox cmbRol;
    private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.TextBox txtNombreUsuario;
  private System.Windows.Forms.Label lblTitleMain;
private System.Windows.Forms.Label lblNombreUsuario;
private System.Windows.Forms.Label lblContraseña;
private System.Windows.Forms.Label lblRol;
private System.Windows.Forms.TextBox txtNombre;
private System.Windows.Forms.Label lblNombre;
private System.Windows.Forms.TextBox txtApellidoPaterno;
private System.Windows.Forms.Label lblApellidoPaterno;
private System.Windows.Forms.TextBox txtApellidoMaterno;
private System.Windows.Forms.Label lblApellidoMaterno;
private System.Windows.Forms.TextBox txtRFC;
private System.Windows.Forms.Label lblRFC;
private System.Windows.Forms.Label lblSeccionEmpleado;
private System.Windows.Forms.Label lblSeccionUsuario;
  }
}
