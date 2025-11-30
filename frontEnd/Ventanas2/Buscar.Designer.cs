/*
 * Created by SharpDevelop.
 * User: compu1
 * Date: 24/11/2025
 * Time: 09:00 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace proyectoInventario
{
    partial class Buscar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
     System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Buscar));
   this.lblTitleMain = new System.Windows.Forms.Label();
      this.txtIDProducto = new System.Windows.Forms.TextBox();
    this.btnBuscar = new System.Windows.Forms.Button();
   this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.btnVolver = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNombreProducto = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
        this.SuspendLayout();
      // 
            // lblTitleMain
   // 
            this.lblTitleMain.AutoSize = true;
            this.lblTitleMain.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
     this.lblTitleMain.ForeColor = System.Drawing.Color.White;
  this.lblTitleMain.Location = new System.Drawing.Point(49, 62);
     this.lblTitleMain.Name = "lblTitleMain";
  this.lblTitleMain.Size = new System.Drawing.Size(160, 25);
        this.lblTitleMain.TabIndex = 5;
            this.lblTitleMain.Text = "Buscar Producto";
            // 
        // lblNombreProducto
    // 
            this.lblNombreProducto.AutoSize = true;
            this.lblNombreProducto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    this.lblNombreProducto.ForeColor = System.Drawing.Color.White;
    this.lblNombreProducto.Location = new System.Drawing.Point(54, 90);
    this.lblNombreProducto.Name = "lblNombreProducto";
    this.lblNombreProducto.Size = new System.Drawing.Size(181, 15);
    this.lblNombreProducto.TabIndex = 17;
 this.lblNombreProducto.Text = "Nombre o Clave del Producto:";
    // 
        // txtIDProducto
      // 
            this.txtIDProducto.Location = new System.Drawing.Point(54, 108);
      this.txtIDProducto.Name = "txtIDProducto";
            this.txtIDProducto.Size = new System.Drawing.Size(155, 20);
       this.txtIDProducto.TabIndex = 6;
        // 
            // btnBuscar
        // 
 this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(75)))), ((int)(((byte)(255)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
 this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
       this.btnBuscar.ForeColor = System.Drawing.Color.White;
        this.btnBuscar.Location = new System.Drawing.Point(236, 103);
this.btnBuscar.Name = "btnBuscar";
        this.btnBuscar.Size = new System.Drawing.Size(67, 27);
         this.btnBuscar.TabIndex = 10;
      this.btnBuscar.Text = "Buscar";
   this.btnBuscar.UseVisualStyleBackColor = false;
      // 
// dgvProductos
            // 
 this.dgvProductos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(35)))));
      this.dgvProductos.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.dgvProductos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvProductos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.MenuBar;
         dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe MDL2 Assets", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
  dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
         dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
  dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dgvProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
      this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
     this.dgvProductos.Location = new System.Drawing.Point(23, 161);
  this.dgvProductos.Name = "dgvProductos";
    this.dgvProductos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
         this.dgvProductos.Size = new System.Drawing.Size(799, 204);
            this.dgvProductos.TabIndex = 11;
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
this.btnVolver.TabIndex = 12;
this.btnVolver.Text = "Volver";
this.btnVolver.UseVisualStyleBackColor = false;
this.btnVolver.Click += new System.EventHandler(this.BtnVolver_Click);
    // 
    // label1
            // 
 this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.White;
        this.label1.Location = new System.Drawing.Point(37, 141);
   this.label1.Name = "label1";
       this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 16;
     this.label1.Text = "Resultados";
   // 
            // Buscar
// 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
       this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(13)))), ((int)(((byte)(32)))));
      this.ClientSize = new System.Drawing.Size(854, 391);
            this.Controls.Add(this.lblNombreProducto);
   this.Controls.Add(this.label1);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtIDProducto);
 this.Controls.Add(this.lblTitleMain);
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Buscar";
  this.Text = "Buscar Producto";
       ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
     this.ResumeLayout(false);
this.PerformLayout();
        }
     private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Button btnBuscar;
private System.Windows.Forms.TextBox txtIDProducto;
        private System.Windows.Forms.Label lblTitleMain;
private System.Windows.Forms.Label lblNombreProducto;
}
}
