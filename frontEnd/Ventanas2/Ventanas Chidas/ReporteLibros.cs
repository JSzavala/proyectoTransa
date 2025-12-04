using proyectoInventario.backEnd.conexionBd;
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
        private clsConsultas _misConsultas;
        public ReporteLibros()
        {
            InitializeComponent();
            _misConsultas = new clsConsultas();
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

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            // Validaciones visuales
            if (string.IsNullOrWhiteSpace(txtMes.Text) || string.IsNullOrWhiteSpace(txtAño.Text))
            {
                MessageBox.Show("Por favor ingresa Mes y Año.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                
                var tablaReporte = _misConsultas.ObtenerVentasPorFecha(txtMes.Text, txtAño.Text);

                if (tablaReporte != null && tablaReporte.Rows.Count > 0)
                {
                    dgvReporteLibros.DataSource = tablaReporte;
                    dgvReporteLibros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    dgvReporteLibros.DataSource = null;
                    MessageBox.Show("No se encontraron ventas en esa fecha.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar el reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
