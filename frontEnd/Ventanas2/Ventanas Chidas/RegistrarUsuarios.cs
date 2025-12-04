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
using System.Collections.Generic;
using System.Linq;
using proyectoInventario.backEnd.conexionBd;

namespace proyectoInventario
{
    /// <summary>
    /// Description of RegistrarUsuarios.
    /// </summary>
    public partial class RegistrarUsuarios : Form
    {
        private srvEmpleado servicioEmpleado;
        private bool modoEdicion = false;

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
            CargarUsuariosActivos();
  
            // Suscribirse al evento CellEndEdit para guardar cambios
            dgvUsuarios.CellEndEdit += DgvUsuarios_CellEndEdit;
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

        /// <summary>
        /// Carga los usuarios activos en el DataGridView
        /// </summary>
        private void CargarUsuariosActivos()
        {
            try
            {
                DataTable usuariosActivos = servicioEmpleado.ObtenerUsuariosActivos();

                if (usuariosActivos != null && usuariosActivos.Rows.Count > 0)
                {
                    dgvUsuarios.DataSource = usuariosActivos;

                    // Ocultar la columna ID si existe
                    if (dgvUsuarios.Columns.Contains("ID"))
                    {
                        dgvUsuarios.Columns["ID"].Visible = false;
                    }
                }
                else
                {
                    dgvUsuarios.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios activos: " + ex.Message, "Error",
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
                        CargarUsuariosActivos(); // Recargar el DataGridView
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
        /// Desactiva el usuario seleccionado in the DataGridView
        /// </summary>
        private void BtnEliminarUsuario_Click(object sender, EventArgs e)
        {
            
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

        private void btnEliminarUsuario_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Verificar que hay una fila seleccionada
                if (dgvUsuarios.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un usuario para desactivar.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener el nombre de usuario de la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvUsuarios.SelectedRows[0];
                string nombreUsuario = filaSeleccionada.Cells["Nombre de Usuario"].Value.ToString();
                string nombreCompleto = filaSeleccionada.Cells["Nombre Completo"].Value.ToString();

                // Confirmar la desactivación
                DialogResult resultado = MessageBox.Show(
                   string.Format("¿Está seguro de desactivar al usuario '{0}' ({1})?", nombreCompleto, nombreUsuario),
                     "Confirmar Desactivación",
                      MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    bool desactivado = servicioEmpleado.DesactivarUsuario(nombreUsuario);

                    if (desactivado)
                    {
                        MessageBox.Show("Usuario desactivado exitosamente.", "Éxito",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recargar el DataGridView
                        CargarUsuariosActivos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo desactivar el usuario.", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al desactivar usuario: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            // Alternar modo de edición
            modoEdicion = !modoEdicion;
            dgvUsuarios.ReadOnly = !modoEdicion;
 
   
         if (modoEdicion)
   {
      btnEditarUsuario.Text = "Terminar Edición";
  btnEditarUsuario.BackColor = Color.FromArgb(255, 165, 0); 
    MessageBox.Show("Modo de edición activado. Puede editar los datos directamente en la tabla.\n\n" +
"NOTA: Solo puede editar el Nombre Completo. Los cambios se guardarán automáticamente.",
    "Modo Edición", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
         btnEditarUsuario.Text = "Editar Usuario";
                btnEditarUsuario.BackColor = Color.FromArgb(80, 75, 255); 
   MessageBox.Show("Modo de edición desactivado.", "Modo Lectura",
   MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
            
            // Hacer que solo la columna de Nombre Completo sea editable
            if (dgvUsuarios.Columns.Contains("Nombre de Usuario"))
    {
         dgvUsuarios.Columns["Nombre de Usuario"].ReadOnly = true;
      }
     if (dgvUsuarios.Columns.Contains("Tipo de Usuario"))
         {
 dgvUsuarios.Columns["Tipo de Usuario"].ReadOnly = true;
          }
  }

        /// <summary>
        /// Evento que se dispara cuando se termina de editar una celda
        /// </summary>
        private void DgvUsuarios_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!modoEdicion) return;

            try
            {
                // Obtener la fila editada
                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];
  
                // Obtener el nombre de usuario (clave única)
                if (!dgvUsuarios.Columns.Contains("Nombre de Usuario") || 
                    fila.Cells["Nombre de Usuario"].Value == null)
                {
                    MessageBox.Show("No se puede identificar el usuario.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nombreUsuario = fila.Cells["Nombre de Usuario"].Value.ToString();
      
                // Verificar qué columna se editó
                string columnaNombre = dgvUsuarios.Columns[e.ColumnIndex].Name;

                if (columnaNombre == "Nombre Completo")
                {
                    // Obtener el nuevo nombre completo
                    string nuevoNombreCompleto = fila.Cells["Nombre Completo"].Value?.ToString() ?? "";
        
                    // Dividir el nombre completo en partes
                    string[] partes = nuevoNombreCompleto.Trim().Split(' ');
            
                    if (partes.Length < 2)
                    {
                        MessageBox.Show("El nombre debe contener al menos Nombre y Apellido Paterno separados por espacio.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        CargarUsuariosActivos(); // Recargar para restaurar el valor original
                        return;
                    }

                    string nombre = partes[0];
                    string apellidoPaterno = partes[1];
                    string apellidoMaterno = partes.Length > 2 ? string.Join(" ", partes.Skip(2)) : "";

                    // Obtener el ID del usuario y empleado asociado
                    int idUsuario = Convert.ToInt32(fila.Cells["ID"].Value);
       
                    // Obtener el ID del empleado asociado
                    DataTable empleado = ObtenerEmpleadoPorIdUsuario(idUsuario);
   
                    if (empleado != null && empleado.Rows.Count > 0)
                    {
                        int idEmpleado = Convert.ToInt32(empleado.Rows[0]["ID_Empleado"]);
                        string rfcActual = empleado.Rows[0]["RFC"]?.ToString();

                        // Actualizar el empleado usando el servicio
                        bool actualizado = servicioEmpleado.ActualizarEmpleado(
                            idEmpleado,
                            nombre,
                            apellidoPaterno,
                            apellidoMaterno,
                            rfcActual,
                            idUsuario
                        );

                        if (actualizado)
                        {
                            MessageBox.Show("Datos actualizados correctamente en la base de datos.",
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarUsuariosActivos(); // Recargar para mostrar los cambios
                        }
                        else
                        {
                            MessageBox.Show("No se pudo actualizar los datos.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            CargarUsuariosActivos(); // Recargar para restaurar el valor original
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el empleado asociado al usuario.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        CargarUsuariosActivos(); // Recargar para restaurar el valor original
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los cambios: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                CargarUsuariosActivos(); // Recargar para restaurar el valor original
            }
        }

        /// <summary>
        /// Obtiene el empleado asociado a un ID de usuario
        /// </summary>
        private DataTable ObtenerEmpleadoPorIdUsuario(int idUsuario)
        {
            try
            {
                // Crear una instancia de clsConsultas para ejecutar la consulta
                clsConsultas consultas = new clsConsultas();
  
                string consulta = @"SELECT ID_Empleado, Nombre, ApellidoPaterno, 
                                    ApellidoMaterno, RFC 
                                  FROM Empleado 
                                 WHERE ID_Usuario = @idUsuario";
    
                var parametros = new Dictionary<string, object>
                {
                    { "@idUsuario", idUsuario }
                };

                return consultas.Select(consulta, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener empleado: " + ex.Message);
                return null;
            }
        }
    }
}
