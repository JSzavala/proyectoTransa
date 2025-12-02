using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyectoInventario.frontEnd.Ventanas2.Ventanas_Chidas
{
    public partial class ReporteLibros : Form
    {
        public ReporteLibros()
        {
            InitializeComponent();
        }

        private void lblTitleMain_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToShortTimeString();
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Main frmMain = new Main();
            frmMain.Show();
            this.Close();
        }
    }
}
