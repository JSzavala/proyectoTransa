/*
 * Created by SharpDevelop.
 * User: compu1
 * Date: 24/11/2025
 * Time: 09:32 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using proyectoInventario.backEnd.Servicios;
using System.Data;

namespace proyectoInventario
{
    /// <summary>
    /// Description of RegistrarUsuarios.
    /// </summary>
    public partial class RegistrarUsuarios : Form
    {
        private srvEmpleado servicioEmpleado;

        public RegistrarUsuarios()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
            servicioEmpleado = new srvEmpleado();
            CargarTiposUsuario();
        }

        /// <summary>
        /// Carga los tipos de usuario disponibles en el ComboBox
        /// </summary>
        private void CargarTiposUsuario()
        {
            try
            {
                DataTable tiposUsuario = servicioEmpleado.ObtenerTiposUsuario();

                cmbRol.DataSource = tiposUsuario;
                cmbRol.DisplayMember = "NombreTipo";
                cmbRol.ValueMember = "ID_TipoUsuario";
                cmbRol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de usuario: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            Main frmMain = new Main();
            frmMain.Show();
            this.Close();
        }

        private void BtnRegistrarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre del empleado es requerido.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtApellidoPaterno.Text))
                {
                    MessageBox.Show("El apellido paterno es requerido.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtApellidoPaterno.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
                {
                    MessageBox.Show("El nombre de usuario es requerido.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombreUsuario.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtContraseña.Text))
                {
                    MessageBox.Show("La contraseña es requerida.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContraseña.Focus();
                    return;
                }

                if (cmbRol.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un rol.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbRol.Focus();
                    return;
                }

                // Confirmar registro
                DialogResult resultado = MessageBox.Show(
                    "¿Está seguro de registrar este empleado/usuario?",
                    "Confirmar Registro",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    int idTipoUsuario = Convert.ToInt32(cmbRol.SelectedValue);

                    long idEmpleado = servicioEmpleado.RegistrarEmpleadoConUsuario(
                        txtNombre.Text.Trim(),
                        txtApellidoPaterno.Text.Trim(),
                        txtApellidoMaterno.Text.Trim(),
                        txtRFC.Text.Trim(),
                        txtNombreUsuario.Text.Trim(),
                        txtContraseña.Text,
                        idTipoUsuario
                    );

                    if (idEmpleado > 0)
                    {
                        MessageBox.Show("Empleado registrado exitosamente con ID: " + idEmpleado,
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo registrar el empleado.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar empleado: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Limpia todos los campos del formulario
        /// </summary>
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellidoPaterno.Clear();
            txtApellidoMaterno.Clear();
            txtRFC.Clear();
            txtNombreUsuario.Clear();
            txtContraseña.Clear();
            cmbRol.SelectedIndex = -1;
            txtNombre.Focus();
        }
    }
}
