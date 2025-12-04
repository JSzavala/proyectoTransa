/*
 * Created by SharpDevelop.
 * User: compu1
 * Date: 24/11/2025
 * Time: 08:30 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using proyectoInventario.backEnd.conexionBd;

namespace proyectoInventario
{
    /// <summary>
  /// Description of RegistrarProducto.
    /// </summary>
    public partial class RegistrarProducto : Form
    {
        private clsConsultas consultas;
        private bool modoEdicion = false;
   
        public RegistrarProducto()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
         
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
            consultas = new clsConsultas();
            ConfigurarDataGridView();
            CargarProductos();
            
            // Suscribirse al evento CellEndEdit para guardar cambios
            dgvProductos.CellEndEdit += DgvProductos_CellEndEdit;
        }

        /// <summary>
        /// Configura el estilo y comportamiento del DataGridView
        /// </summary>
    private void ConfigurarDataGridView()
        {
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;
            
            // Configurar colores
            dgvProductos.BackgroundColor = Color.FromArgb(20, 20, 35);
            dgvProductos.DefaultCellStyle.BackColor = Color.FromArgb(20, 20, 35);
            dgvProductos.DefaultCellStyle.ForeColor = Color.White;
            dgvProductos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(80, 75, 255);
            dgvProductos.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        /// <summary>
        /// Carga todos los productos activos (no descontinuados) desde la base de datos
        /// </summary>
        private void CargarProductos()
        {
      try
            {
    string consulta = "SELECT CLAVE, NOMBRE, DESCRIPCION, PRECIO, STOCK FROM Producto WHERE DESCONTINUADO = FALSE ORDER BY CLAVE";
                DataTable productos = consultas.Select(consulta);
  dgvProductos.DataSource = productos;
   
// Renombrar encabezados de columnas para mejor presentación
 if (dgvProductos.Columns.Count > 0)
         {
       dgvProductos.Columns["CLAVE"].HeaderText = "Clave";
           dgvProductos.Columns["NOMBRE"].HeaderText = "Nombre";
   dgvProductos.Columns["DESCRIPCION"].HeaderText = "Descripción";
    dgvProductos.Columns["PRECIO"].HeaderText = "Precio";
  dgvProductos.Columns["STOCK"].HeaderText = "Stock";

               // Formatear columna de precio
           dgvProductos.Columns["PRECIO"].DefaultCellStyle.Format = "C2";
      }
            }
   catch (Exception ex)
            {
   MessageBox.Show($"Error al cargar productos: {ex.Message}", 
        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón Agregar al Inventario
        /// </summary>
        private void BtnAgregarInv_Click(object sender, EventArgs e)
        {
            try
            {
            // Validar que todos los campos estén llenos
                if (string.IsNullOrWhiteSpace(txtIDProducto.Text))
                {
                    MessageBox.Show("Por favor, ingrese la clave del producto.", 
                    "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIDProducto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombreProducto.Text))
                {
                    MessageBox.Show("Por favor, ingrese el nombre del producto.", 
                    "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombreProducto.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPrecio.Text))
                {
                    MessageBox.Show("Por favor, ingrese el precio del producto.", 
                    "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrecio.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtStockInicial.Text))
                {
                    MessageBox.Show("Por favor, ingrese el stock inicial del producto.", 
                    "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStockInicial.Focus();
                    return;
                }

                // Validar formato de los datos numéricos
                if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio < 0)
                {
                    MessageBox.Show("El precio debe ser un número válido mayor o igual a cero.", 
                    "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrecio.Focus();
                    return;
                }

                if (!int.TryParse(txtStockInicial.Text, out int stock) || stock < 0)
                {
                    MessageBox.Show("El stock debe ser un número entero válido mayor o igual a cero.", 
                    "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStockInicial.Focus();
                    return;
                }

                string clave = txtIDProducto.Text.Trim().ToUpper();
                string nombre = txtNombreProducto.Text.Trim();
                string descripcion = ""; // Campo opcional, puede dejarse vacío

                // Validar longitud de la clave (máximo 20 caracteres según la BD)
                if (clave.Length > 20)
                {
                    MessageBox.Show("La clave del producto no puede exceder 20 caracteres.", 
                    "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIDProducto.Focus();
                    return;
                }

                // Verificar si la clave ya existe
                if (VerificarClaveExistente(clave))
                {
                    MessageBox.Show($"Ya existe un producto con la clave '{clave}'. Por favor, use otra clave.", 
                    "Clave duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIDProducto.Focus();
                    return;
                }

                // Insertar el producto en la base de datos
                bool resultado = InsertarProducto(clave, nombre, descripcion, precio, stock);

                if (resultado)
                {
                    MessageBox.Show("Producto registrado exitosamente.", 
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
       
                    // Limpiar los campos
                    LimpiarCampos();          
                    // Recargar la lista de productos
                    CargarProductos();
            
                    // Colocar el foco en el primer campo
                    txtIDProducto.Focus();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el producto. Intente nuevamente.", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el producto: {ex.Message}", 
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Verifica si una clave de producto ya existe en la base de datos
        /// </summary>
        /// <param name="clave">Clave a verificar</param>
        /// <returns>True si la clave existe, False en caso contrario</returns>
        private bool VerificarClaveExistente(string clave)
        {
            try
            {
                string consulta = "SELECT COUNT(*) as Total FROM Producto WHERE CLAVE = @clave";
                var parametros = new Dictionary<string, object>
                {
                    { "@clave", clave }
                };

                DataTable resultado = consultas.Select(consulta, parametros);
   
                if (resultado.Rows.Count > 0)
                {
                    int total = Convert.ToInt32(resultado.Rows[0]["Total"]);
                    return total > 0;
                }
       
                return false;
            }
            catch (Exception)
            {
                // En caso de error, asumir que no existe
                return false;
            }
        }

        /// <summary>
        /// Inserta un nuevo producto en la base de datos
        /// </summary>
        /// <param name="clave">Clave del producto</param>
        /// <param name="nombre">Nombre del producto</param>
        /// <param name="descripcion">Descripción del producto</param>
        /// <param name="precio">Precio del producto</param>
        /// <param name="stock">Stock inicial del producto</param>
        /// <returns>True si se insertó correctamente, False en caso contrario</returns>
        private bool InsertarProducto(string clave, string nombre, string descripcion, decimal precio, int stock)
        {
            try
            {
                string consulta = @"INSERT INTO Producto (CLAVE, NOMBRE, DESCRIPCION, PRECIO, STOCK, DESCONTINUADO) 
                VALUES (@clave, @nombre, @descripcion, @precio, @stock, FALSE)";
         
                var parametros = new Dictionary<string, object>
                {
                    { "@clave", clave },
                    { "@nombre", nombre },
                    { "@descripcion", descripcion },
                    { "@precio", precio },
                    { "@stock", stock }
                };

                long resultado = consultas.Insert(consulta, parametros);
                return resultado >= 0; // Insert devuelve el ID generado, cualquier valor >= 0 indica éxito
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar producto en la base de datos: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Limpia todos los campos del formulario
        /// </summary>
        private void LimpiarCampos()
        {
            txtIDProducto.Clear();
            txtNombreProducto.Clear();
            txtPrecio.Clear();
            txtStockInicial.Clear();
        }

        private void BtnVolver_Click(object sender, EventArgs e)
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

        /// <summary>
        /// Elimina lógicamente el producto seleccionado marcándolo como descontinuado
        /// </summary>
        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que hay una fila seleccionada
                if (dgvProductos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar un producto para descontinuar.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener la clave del producto seleccionado
                DataGridViewRow filaSeleccionada = dgvProductos.SelectedRows[0];
                string clave = filaSeleccionada.Cells["CLAVE"].Value.ToString();
                string nombre = filaSeleccionada.Cells["NOMBRE"].Value.ToString();

                // Confirmar la descontinuación
                DialogResult resultado = MessageBox.Show(
                string.Format("¿Está seguro de descontinuar el producto '{0}' ({1})?\n\n" +
                "El producto será marcado como descontinuado y no se eliminará de la base de datos.", 
                nombre, clave),
                "Confirmar Descontinuación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // Marcar el producto como descontinuado (borrado lógico)
                    string consulta = "UPDATE Producto SET DESCONTINUADO = TRUE WHERE CLAVE = @clave";
                    var parametros = new Dictionary<string, object>
                    {
                        { "@clave", clave }
                    };

                    int filasAfectadas = consultas.Update(consulta, parametros);

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Producto descontinuado exitosamente.\n\n" +
                        "El producto ha sido marcado como descontinuado y ya no aparecerá en el inventario activo.", 
                        "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recargar el DataGridView
                        CargarProductos();
                    }
                    else
                    {
                    MessageBox.Show("No se pudo descontinuar el producto.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descontinuar producto: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Activa/Desactiva el modo de edición del DataGridView
        /// </summary>
        private void btnEditarProducto_Click(object sender, EventArgs e)
        {
            // Alternar modo de edición
            modoEdicion = !modoEdicion;
            dgvProductos.ReadOnly = !modoEdicion;

            // Cambiar el texto del botón según el modo
            if (modoEdicion)
            {
                btnEditarProducto.Text = "Terminar Edición";
                btnEditarProducto.BackColor = Color.FromArgb(255, 165, 0); // Naranja
                MessageBox.Show("Modo de edición activado. Puede editar los datos directamente en la tabla.\n\n" +
                "NOTA: La clave (CLAVE) no es editable. Los cambios se guardarán automáticamente.",
                "Modo Edición", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                btnEditarProducto.Text = "Editar";
                btnEditarProducto.BackColor = Color.FromArgb(80, 75, 255); // Azul original
                MessageBox.Show("Modo de edición desactivado.", "Modo Lectura",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Hacer que la columna CLAVE siempre sea de solo lectura
            if (dgvProductos.Columns.Contains("CLAVE"))
            {
                dgvProductos.Columns["CLAVE"].ReadOnly = true;
            }
        }

        /// <summary>
        /// Evento que se dispara cuando se termina de editar una celda
        /// </summary>
        private void DgvProductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!modoEdicion) return;

            try
            {
                // Obtener la fila editada
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                // Obtener la clave del producto (clave primaria)
                if (!dgvProductos.Columns.Contains("CLAVE") || fila.Cells["CLAVE"].Value == null)
                {
                    MessageBox.Show("No se puede identificar el producto.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string clave = fila.Cells["CLAVE"].Value.ToString();

                // Obtener todos los valores de la fila
                string nombre = fila.Cells["NOMBRE"].Value?.ToString() ?? "";
                string descripcion = fila.Cells["DESCRIPCION"].Value?.ToString() ?? "";
    
                // Validar y obtener precio
                if (!decimal.TryParse(fila.Cells["PRECIO"].Value?.ToString(), out decimal precio))
                {
                    MessageBox.Show("El precio debe ser un número válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CargarProductos(); // Recargar para restaurar valores
                    return;
                }

                // Validar y obtener stock
                if (!int.TryParse(fila.Cells["STOCK"].Value?.ToString(), out int stock))
                {
                    MessageBox.Show("El stock debe ser un número entero válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CargarProductos(); // Recargar para restaurar valores
                    return;
                }

                // Validar que los valores sean positivos
                if (precio < 0)
                {
                    MessageBox.Show("El precio no puede ser negativo.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CargarProductos();
                    return;
                }

                if (stock < 0)
                {
                    MessageBox.Show("El stock no puede ser negativo.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CargarProductos();
                    return;
                }

                // Actualizar el producto en la base de datos
                string consulta = @"UPDATE Producto 
                SET NOMBRE = @nombre, 
                DESCRIPCION = @descripcion, 
                PRECIO = @precio, 
                STOCK = @stock 
                WHERE CLAVE = @clave";

                var parametros = new Dictionary<string, object>
                {
                    { "@clave", clave },
                    { "@nombre", nombre },
                    { "@descripcion", descripcion },
                    { "@precio", precio },
                    { "@stock", stock }
                };

                int filasAfectadas = consultas.Update(consulta, parametros);

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Producto actualizado correctamente en la base de datos.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProductos(); 
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el producto.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CargarProductos(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los cambios: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                CargarProductos(); 
            }
        }
    }
}
