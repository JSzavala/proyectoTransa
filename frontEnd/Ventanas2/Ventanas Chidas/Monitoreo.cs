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
    public partial class Monitoreo : Form
    {
        public Monitoreo()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Main frmMain = new Main();
            frmMain.Show();
            this.Close();
        }
    }
}
