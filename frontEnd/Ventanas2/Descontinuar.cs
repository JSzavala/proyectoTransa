/*
 * Created by SharpDevelop.
 * User: compu1
 * Date: 24/11/2025
 * Time: 09:05 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using proyectoInventario.backEnd.conexionBd;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace proyectoInventario
{
    /// <summary>
    /// Description of Descontinuar.
    /// </summary>
    public partial class Descontinuar : Form
    {
  public Descontinuar()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            CargarProductos();
            InitializeComponent();
    //
    // TODO: Add constructor code after the InitializeComponent() call.
    //
    }
        
        private void BtnVolver_Click(object sender, EventArgs e)
        {
           Main frmMain = new Main();
           frmMain.Show();
           this.Close();
        }

        private void LblTitleMainClick(object sender, EventArgs e)
        {
            // Evento del label (sin funcionalidad específica)
        }
        private void CargarProductos()
        {
            clsConsultas consultas = new clsConsultas();
            // Actualizado para la nueva estructura de BD
            DataTable productos = consultas.Select("SELECT CLAVE, NOMBRE, PRECIO, STOCK FROM Producto");
   dgvProductos.DataSource = productos;
        }
        
        private void btnRegistrarCompra_Click(object sender, EventArgs e)
        {
        string claveProducto = txtIDProducto.Text.Trim();
      
       if (string.IsNullOrWhiteSpace(claveProducto))
   {
   MessageBox.Show("Ingrese una clave de producto válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
       return;
   }
       
 clsConsultas consultas = new clsConsultas();
   bool resultado = consultas.DescontinuarProducto(claveProducto);

     if (resultado)
  {
     MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
     CargarProductos(); // Recargar la lista
           txtIDProducto.Clear();
   }
     else
     {
    MessageBox.Show("No se pudo eliminar el producto. Verifica la clave.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
       }
 }
    }
}
