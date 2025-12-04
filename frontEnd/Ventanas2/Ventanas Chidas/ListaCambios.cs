using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using proyectoInventario.backEnd.conexionBd;

namespace proyectoInventario.frontEnd.Ventanas2.Ventanas_Chidas
{
    public partial class ListaCambios : Form
    {
        private clsConsultas _misConsultas;

        public ListaCambios()
        {
            InitializeComponent();
            _misConsultas = new clsConsultas();
            CargarCambiosUpdate();
        }

        private void CargarCambiosUpdate()
        {
            try
            {
                DataTable datosUpdate = _misConsultas.ObtenerCambiosUpdate();

                if (datosUpdate != null && datosUpdate.Rows.Count > 0)
                {
                    dataGridView1.DataSource = datosUpdate;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    dataGridView1.DataSource = null;
                    MessageBox.Show("No hay registros de cambios tipo UPDATE.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de cambios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Main frmMain = new Main();
            frmMain.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToShortTimeString();
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }
    }
}
