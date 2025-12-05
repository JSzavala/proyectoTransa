/*
 * Created by SharpDevelop.
 * User: compu1
 * Date: 25/11/2025
 * Time: 07:51 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using proyectoInventario.backEnd.POCOS;
using System;
using System.Drawing;
using System.Windows.Forms;
using proyectoInventario.backEnd.conexionBd;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;

namespace proyectoInventario
{
	/// <summary>
	/// Description of RegistrarVenta.
	/// </summary>
	public partial class RegistrarVenta : Form
	{
        private clsConsultas consultas;
        private DataTable dtProductosVenta;
        private int idEmpleado = 1; // TODO: Obtener del usuario logueado

    public RegistrarVenta(RolUsuario rolUsuario)
    {
	    //
		// The InitializeComponent() call is required for Windows Forms designer support.
		//
		InitializeComponent();
		
		//
		// TODO: Add constructor code after the InitializeComponent() call.
		//
		consultas = new clsConsultas();
		InicializarDataTableProductos();
		ConfigurarDataGridView();
	}

        /// <summary>
        /// Inicializa el DataTable que almacenará temporalmente los productos de la venta
        /// </summary>
        private void InicializarDataTableProductos()
        {
            dtProductosVenta = new DataTable();
            dtProductosVenta.Columns.Add("CLAVE", typeof(string));
            dtProductosVenta.Columns.Add("NOMBRE", typeof(string));
            dtProductosVenta.Columns.Add("PRECIO", typeof(decimal));
            dtProductosVenta.Columns.Add("CANTIDAD", typeof(int));
            dtProductosVenta.Columns.Add("SUBTOTAL", typeof(decimal));
  
            dgvProductosVenta.DataSource = dtProductosVenta;
        }

        /// <summary>
        /// Configura el estilo y comportamiento del DataGridView
        /// </summary>
        private void ConfigurarDataGridView()
        {
            dgvProductosVenta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductosVenta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductosVenta.MultiSelect = false;
            dgvProductosVenta.AllowUserToAddRows = false;
  
            // Configurar colores
            dgvProductosVenta.BackgroundColor = Color.FromArgb(20, 20, 35);
            dgvProductosVenta.DefaultCellStyle.BackColor = Color.FromArgb(20, 20, 35);
            dgvProductosVenta.DefaultCellStyle.ForeColor = Color.White;
            dgvProductosVenta.DefaultCellStyle.SelectionBackColor = Color.FromArgb(80, 75, 255);
            dgvProductosVenta.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvProductosVenta.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgvProductosVenta.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            
            // Hacer que solo la columna CANTIDAD sea editable
            if (dgvProductosVenta.Columns.Count > 0)
            {
                dgvProductosVenta.Columns["CLAVE"].ReadOnly = true;
                dgvProductosVenta.Columns["NOMBRE"].ReadOnly = true;
                dgvProductosVenta.Columns["PRECIO"].ReadOnly = true;
                dgvProductosVenta.Columns["CANTIDAD"].ReadOnly = false; // Editable
                dgvProductosVenta.Columns["SUBTOTAL"].ReadOnly = true;
        
                // Formatear columnas de precio
                dgvProductosVenta.Columns["PRECIO"].DefaultCellStyle.Format = "C2";
                dgvProductosVenta.Columns["SUBTOTAL"].DefaultCellStyle.Format = "C2";
      
                // Renombrar encabezados
                dgvProductosVenta.Columns["CLAVE"].HeaderText = "Clave";
                dgvProductosVenta.Columns["NOMBRE"].HeaderText = "Nombre";
                dgvProductosVenta.Columns["PRECIO"].HeaderText = "Precio Unit.";
                dgvProductosVenta.Columns["CANTIDAD"].HeaderText = "Cantidad";
                dgvProductosVenta.Columns["SUBTOTAL"].HeaderText = "Subtotal";
            }
            
            dgvProductosVenta.ReadOnly = false; // Permitir edición de celdas específicas
        }

        /// <summary>
        /// Evento KeyUp del TextBox de búsqueda - detecta Enter para buscar producto
        /// </summary>
        private void txtBuscarProducto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BuscarYAgregarProducto();
            }
        }

  /// <summary>
        /// Busca el producto en la BD y lo agrega al DataGridView
        /// </summary>
        private void BuscarYAgregarProducto()
        {
            try
            {
                string clave = txtBuscarProducto.Text.Trim().ToUpper();
        
                if (string.IsNullOrWhiteSpace(clave))
                {
                    MessageBox.Show("Ingrese la clave del producto.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar si el producto ya está en la lista
                DataRow[] productosExistentes = dtProductosVenta.Select($"CLAVE = '{clave}'");
                if (productosExistentes.Length > 0)
                {
                    // Si ya existe, incrementar la cantidad
                    int cantidadActual = Convert.ToInt32(productosExistentes[0]["CANTIDAD"]);
                    decimal precio = Convert.ToDecimal(productosExistentes[0]["PRECIO"]);
        
                    productosExistentes[0]["CANTIDAD"] = cantidadActual + 1;
                    productosExistentes[0]["SUBTOTAL"] = precio * (cantidadActual + 1);
     
                    MessageBox.Show($"Cantidad del producto '{clave}' incrementada a {cantidadActual + 1}.",
                    "Producto Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Buscar producto en la BD
                    string consulta = "SELECT CLAVE, NOMBRE, PRECIO, STOCK FROM Producto WHERE CLAVE = @clave AND DESCONTINUADO = FALSE";
                    var parametros = new Dictionary<string, object>
                    {
                        { "@clave", clave }
                    };

                    DataTable resultado = consultas.Select(consulta, parametros);

                    if (resultado.Rows.Count == 0)
                    {
                        MessageBox.Show($"No se encontró el producto con clave '{clave}' o está descontinuado.",
                        "Producto No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtBuscarProducto.Focus();
                        return;
                    }

                    DataRow producto = resultado.Rows[0];
                    int stockDisponible = Convert.ToInt32(producto["STOCK"]);

                    if (stockDisponible <= 0)
                    {
                        MessageBox.Show($"El producto '{producto["NOMBRE"]}' no tiene stock disponible.",
                        "Sin Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtBuscarProducto.Clear();
                        txtBuscarProducto.Focus();
                        return;
                    }

                    // Agregar producto al DataTable
                    DataRow nuevaFila = dtProductosVenta.NewRow();
                    nuevaFila["CLAVE"] = producto["CLAVE"];
                    nuevaFila["NOMBRE"] = producto["NOMBRE"];
                    nuevaFila["PRECIO"] = producto["PRECIO"];
                    nuevaFila["CANTIDAD"] = 1;
                    nuevaFila["SUBTOTAL"] = Convert.ToDecimal(producto["PRECIO"]) * 1;

                    dtProductosVenta.Rows.Add(nuevaFila);

                    MessageBox.Show($"Producto '{producto["NOMBRE"]}' agregado a la venta.",
                    "Producto Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Recalcular total
                CalcularTotal();

                // Limpiar y enfocar el TextBox
                txtBuscarProducto.Clear();
                txtBuscarProducto.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar el producto: {ex.Message}",
            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Calcula el total de la venta sumando todos los subtotales
        /// </summary>
        private void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataRow fila in dtProductosVenta.Rows)
            {
                total += Convert.ToDecimal(fila["SUBTOTAL"]);
            }

            txtTotalVenta.Text = total.ToString("C2");
        }

        /// <summary>
        /// Evento que se dispara cuando se termina de editar una celda (para actualizar cantidad)
        /// </summary>
        private void dgvProductosVenta_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Solo procesar si se editó la columna CANTIDAD
                if (dgvProductosVenta.Columns[e.ColumnIndex].Name != "CANTIDAD")
                return;

                DataGridViewRow fila = dgvProductosVenta.Rows[e.RowIndex];

                // Validar cantidad
                if (!int.TryParse(fila.Cells["CANTIDAD"].Value?.ToString(), out int cantidad) || cantidad <= 0)
                {
                    MessageBox.Show("La cantidad debe ser un número entero mayor a cero.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    fila.Cells["CANTIDAD"].Value = 1; // Restaurar a 1
                    cantidad = 1;
                }

                // Verificar stock disponible
                string clave = fila.Cells["CLAVE"].Value.ToString();
                string consultaStock = "SELECT STOCK FROM Producto WHERE CLAVE = @clave";
                var parametros = new Dictionary<string, object>
                {
                    { "@clave", clave }
                };

                DataTable resultado = consultas.Select(consultaStock, parametros);
                if (resultado.Rows.Count > 0)
                {
                    int stockDisponible = Convert.ToInt32(resultado.Rows[0]["STOCK"]);
                    if (cantidad > stockDisponible)
                    {
                        MessageBox.Show($"Stock insuficiente. Stock disponible: {stockDisponible}",
                        "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        fila.Cells["CANTIDAD"].Value = stockDisponible;
                        cantidad = stockDisponible;
                    }
                }

                // Recalcular subtotal
                decimal precio = Convert.ToDecimal(fila.Cells["PRECIO"].Value);
                fila.Cells["SUBTOTAL"].Value = precio * cantidad;

                // Recalcular total general
                CalcularTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar cantidad: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Elimina el producto seleccionado del DataGridView
        /// </summary>
        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductosVenta.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un producto para eliminar.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow filaSeleccionada = dgvProductosVenta.SelectedRows[0];
                string nombreProducto = filaSeleccionada.Cells["NOMBRE"].Value.ToString();

                DialogResult resultado = MessageBox.Show(
                $"¿Está seguro de eliminar '{nombreProducto}' de la venta?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    dtProductosVenta.Rows.RemoveAt(filaSeleccionada.Index);
                    CalcularTotal();
        
                    MessageBox.Show("Producto eliminado de la venta.",
                    "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar producto: {ex.Message}",
               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private void BtnVolver_Click(object sender, EventArgs e)
		{
			Main frmMain = new Main();
			frmMain.Show();
			this.Close();
		}
		
		private void RegistrarVentaLoad(object sender, EventArgs e)
		{
			// Evento de carga del formulario
            txtBuscarProducto.Focus();
		}

        /// <summary>
        /// Registra la venta completa con todos los productos del DataGridView
        /// </summary>
        private void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            MySqlConnection conexion = null;
            MySqlTransaction transaction = null;

            try
            {
                // Validar que haya productos en la venta
                if (dtProductosVenta.Rows.Count == 0)
                {
                    MessageBox.Show("Debe agregar al menos un producto a la venta.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Calcular total
                decimal totalVenta = 0;
                foreach (DataRow fila in dtProductosVenta.Rows)
                {
                    totalVenta += Convert.ToDecimal(fila["SUBTOTAL"]);
                }

                // Confirmar venta
                DialogResult confirmacion = MessageBox.Show(
                $"¿Confirma el registro de la venta?\n\nTotal: {totalVenta:C2}\nProductos: {dtProductosVenta.Rows.Count}",
                "Confirmar Venta",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes)
                return;

                // Obtener conexión y abrir
                conexion = consultas.ObtenerConexion();
  
                // Asegurarse de que la conexión esté abierta
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                // Iniciar transacción
                transaction = conexion.BeginTransaction();

                try
                {
                    string insertVenta = @"INSERT INTO Venta (ID_Empleado, Total, Estado, FechaHoraVenta) 
                    VALUES (@idEmpleado, @total, 'Pagada', NOW())";
                    var parametrosVenta = new Dictionary<string, object>
                    {
                        { "@idEmpleado", idEmpleado },
                        { "@total", totalVenta }
                    };

                    long idVenta = consultas.Insert(insertVenta, parametrosVenta, transaction);

                    if (idVenta <= 0)
                    {
                        throw new Exception("No se pudo generar el ID de la venta.");
                    }

                    //Insertar cada producto en DetalleVenta y actualizar Stock
                    foreach (DataRow producto in dtProductosVenta.Rows)
                    {
                        string clave = producto["CLAVE"].ToString();
                        int cantidad = Convert.ToInt32(producto["CANTIDAD"]);
                        decimal precioUnitario = Convert.ToDecimal(producto["PRECIO"]);
                        decimal subtotal = Convert.ToDecimal(producto["SUBTOTAL"]);

                        // Verificar stock disponible nuevamente (por seguridad)
                        string verificarStock = "SELECT STOCK FROM Producto WHERE CLAVE = @clave FOR UPDATE";
                        var paramStock = new Dictionary<string, object> { { "@clave", clave } };
      
                        using (MySqlCommand cmdVerificar = new MySqlCommand(verificarStock, conexion, transaction))
                        {
                            cmdVerificar.Parameters.AddWithValue("@clave", clave);
                            object stockObj = cmdVerificar.ExecuteScalar();
     
                            if (stockObj == null)
                            {
                                throw new Exception($"El producto con clave '{clave}' no existe.");
                            }
          
                            int stockActual = Convert.ToInt32(stockObj);
                            if (stockActual < cantidad)
                            {
                                throw new Exception($"Stock insuficiente para el producto '{clave}'. " +
                                $"Stock disponible: {stockActual}, Cantidad solicitada: {cantidad}");
                            }
                        }

                        // Insertar detalle de venta
                        string insertDetalle = @"INSERT INTO DetalleVenta 
                        (ID_Venta, CLAVE_Producto, Cantidad, PrecioUnitario, Subtotal) 
                        VALUES (@idVenta, @clave, @cantidad, @precio, @subtotal)";
                        var parametrosDetalle = new Dictionary<string, object>
                        {
                            { "@idVenta", idVenta },
                            { "@clave", clave },
                            { "@cantidad", cantidad },
                            { "@precio", precioUnitario },
                            { "@subtotal", subtotal }
                        };

                        consultas.Insert(insertDetalle, parametrosDetalle, transaction);

                        // Actualizar stock del producto
                        string updateStock = @"UPDATE Producto 
                        SET STOCK = STOCK - @cantidad 
                        WHERE CLAVE = @clave";
                        var parametrosStock = new Dictionary<string, object>
                        {
                            { "@cantidad", cantidad },
                            { "@clave", clave }
                        };

                        int filasAfectadas = consultas.Update(updateStock, parametrosStock, transaction);
              
                        if (filasAfectadas == 0)
                        {
                            throw new Exception($"No se pudo actualizar el stock del producto '{clave}'.");
                        }
                    }

                    // Confirmar transacción (COMMIT)
                    transaction.Commit();

                    MessageBox.Show($"Venta registrada exitosamente.\n\nID Venta: {idVenta}\nTotal: {totalVenta:C2}",
                    "Venta Registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar todos los campos
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    // Revertir transacción en caso de error (ROLLBACK)
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
     
                    throw new Exception($"Error durante la transacción de venta: {ex.Message}", ex);
                }
                finally
                {
                    // Liberar la transacción
                    if (transaction != null)
                    {  
                        transaction.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la venta:\n\n{ex.Message}", 
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Cerrar la conexión si está abierta
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        /// <summary>
        /// Limpia todos los campos del formulario
        /// </summary>
        private void LimpiarFormulario()
        {
            dtProductosVenta.Clear();
            txtBuscarProducto.Clear();
            txtTotalVenta.Text = "0.00";
            txtBuscarProducto.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToShortTimeString();
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }
    }
}
