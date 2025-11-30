/*
 * Created by SharpDevelop.
 * User: compu1
 * Date: 24/11/2025
 * Time: 07:59 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using proyectoInventario.backEnd.POCOS;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace proyectoInventario
{
    /// <summary>
    /// Description of Main.
    /// </summary>
    public partial class Main : Form
    {
    public Main()
   {
       //
// The InitializeComponent() call is required for Windows Forms designer support.
       //
            InitializeComponent();
       
   //
     // TODO: Add constructor code after the InitializeComponent() call.
        //
   }
        
        private void BtnRegistrarProducto_Click(object sender, EventArgs e)
 {
            RegistrarProducto frmRegistrarProducto = new RegistrarProducto();
 frmRegistrarProducto.Show();
 this.Hide();
      }
        
        private void BtnRegistrarCompra_Click(object sender, EventArgs e)
    {
    RegistrarCompra frmRegistrarCompra = new RegistrarCompra();
         frmRegistrarCompra.Show();
            this.Hide();
        }
     
   private void BtnBuscar_Click(object sender, EventArgs e)
        {
    Buscar frmBuscar = new Buscar();
 frmBuscar.Show();
            this.Hide();
      }
        
        private void BtnDescontinuar_Click(object sender, EventArgs e)
     {
      Descontinuar frmDescontinuar = new Descontinuar();
            frmDescontinuar.Show();
    this.Hide();
        }
  
        private void BtnRegistrarVenta_Click(object sender, EventArgs e)
     {
 RegistrarVenta frmRegistrarVenta = new RegistrarVenta(RolUsuario.Dueña);
        frmRegistrarVenta.Show();
this.Hide();
        }
        
        private void BtnRegistrarUsuarios_Click(object sender, EventArgs e)
        {
  RegistrarUsuarios frmRegistrarUsuarios = new RegistrarUsuarios();
            frmRegistrarUsuarios.Show();
       this.Hide();
 }
        
    private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
       DialogResult resultado = MessageBox.Show(
   "¿Está seguro que desea cerrar sesión?", 
                "Confirmar cierre de sesión", 
     MessageBoxButtons.YesNo, 
            MessageBoxIcon.Question);
 
            if (resultado == DialogResult.Yes)
            {
       Login frmLogin = new Login();
   frmLogin.Show();
          this.Close();
            }
        }
     
        private void Button2Click(object sender, EventArgs e)
        {
    // Este método existe por compatibilidad con el designer anterior
   BtnDescontinuar_Click(sender, e);
        }
     
        private void Label1Click(object sender, EventArgs e)
        {
     // Evento del label (sin funcionalidad específica)
   }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
       // Redirigir al método correcto
       BtnBuscar_Click(sender, e);
    }
    }
}
