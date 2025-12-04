/*
 * Created by SharpDevelop.
 * User: compu1
 * Date: 24/11/2025
 * Time: 07:59 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using proyectoInventario.backEnd.POCOS;
using proyectoInventario.frontEnd.Ventanas2;
using proyectoInventario.frontEnd.Ventanas2.Ventanas_Chidas;
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
        
        private void BtnRegistrarVenta_Click(object sender, EventArgs e)
        {
            RegistrarVenta frmRegistrarVenta = new RegistrarVenta(RolUsuario.Dueña);
            frmRegistrarVenta.Show();
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
     

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToShortTimeString();
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void btnReporteVentas_Click(object sender, EventArgs e)
        {
            ReporteVentas frmReporteVentas = new ReporteVentas();
            frmReporteVentas.Show();
            this.Hide();
        }

        private void btnReporteLibros_Click(object sender, EventArgs e)
        {
            ReporteLibros frmReporteLibros = new ReporteLibros();
            frmReporteLibros.Show();
            this.Hide();
        }

        private void btnListaCambios_Click(object sender, EventArgs e)
        {
            ListaCambios frmListaCambios = new ListaCambios();
            frmListaCambios.Show();
            this.Hide();
        }

        private void btnMonitoreo_Click(object sender, EventArgs e)
        {
            Monitoreo frmMonitoreo = new Monitoreo();
            frmMonitoreo.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistrarProducto frmRegistrar = new RegistrarProducto();
            frmRegistrar.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistrarUsuarios frmRegistrar = new RegistrarUsuarios();
            frmRegistrar.Show();
            this.Hide();
        }
    }
}
