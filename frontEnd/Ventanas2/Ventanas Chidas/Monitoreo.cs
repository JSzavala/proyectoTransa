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
    public partial class Monitoreo : Form
    {
        private clsConsultas _misConsultas;

        public Monitoreo()
        {
            InitializeComponent();
            _misConsultas = new clsConsultas();
            CargarDatosMonitoreo();
        }

        private void CargarDatosMonitoreo()
        {
            try
            {
                DataTable datosAuditoria = _misConsultas.ObtenerTodosLosCambios();

                if (datosAuditoria != null && datosAuditoria.Rows.Count > 0)
                {
                    dgvMonitoreo.DataSource = datosAuditoria;
                    dgvMonitoreo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    dgvMonitoreo.DataSource = null;
                    MessageBox.Show("No hay registros de auditoría.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de monitoreo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToShortTimeString();
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }
    }
}
