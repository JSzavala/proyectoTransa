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

namespace proyectoInventario
{
	/// <summary>
	/// Description of RegistrarVenta.
	/// </summary>
	public partial class RegistrarVenta : Form
	{
        private string connectionString = "Server=localhost;Database=VENTAS;Uid=root;Pwd=1234;";

        public RegistrarVenta(RolUsuario rolUsuario)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
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
		}

        private void btnCalcularPrecio_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDProducto.Text))
            {
                MessageBox.Show("Ingrese la clave del producto.");
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida.");
                return;
            }

            if (!decimal.TryParse(txtDescuento.Text, out decimal descuento))
            {
                MessageBox.Show("Ingrese un descuento válido.");
                return;
            }

            // Validar rango del descuento (0 a 100)
            if (descuento < 0 || descuento > 100)
            {
                MessageBox.Show("El descuento debe estar entre 0% y 100%.");
                return;
            }

            using (MySqlConnection cn = new MySqlConnection(connectionString))
            {
                cn.Open();
                

                string query = "SELECT PRECIO FROM Producto WHERE CLAVE = @clave";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@clave", txtIDProducto.Text);

                object result = cmd.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("El producto no existe.");
                    return;
                }

                decimal precio = Convert.ToDecimal(result);
                decimal subtotal = precio * cantidad;

                // Aquí se aplica el descuento porcentual correctamente
                decimal montoDescuento = subtotal * (descuento / 100m);
                decimal total = subtotal - montoDescuento;

                txtTotal.Text = total.ToString("0.00");
            }
        }


        private void RecalcularTotal()
        {
            if (string.IsNullOrWhiteSpace(txtIDProducto.Text))
                return;

            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                return;

            if (!decimal.TryParse(txtDescuento.Text, out decimal descuento))
                return;

            // Validar descuento dentro de rango
            if (descuento < 0 || descuento > 100)
                return;

            using (MySqlConnection cn = new MySqlConnection(connectionString))
            {
                cn.Open();

                string query = "SELECT PRECIO FROM Producto WHERE CLAVE = @clave";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@clave", txtIDProducto.Text);

                object result = cmd.ExecuteScalar();
                if (result == null) return;

                decimal precio = Convert.ToDecimal(result);
                decimal subtotal = precio * cantidad;

                decimal montoDescuento = subtotal * (descuento / 100m);
                decimal total = subtotal - montoDescuento;

                txtTotal.Text = total.ToString("0.00");
            }
        }


        private void txtCantidad_TextChanged(object sender, EventArgs e) => RecalcularTotal();
        private void txtDescuento_TextChanged(object sender, EventArgs e) => RecalcularTotal();
        private void txtIDProducto_TextChanged(object sender, EventArgs e) => RecalcularTotal();

        private void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDProducto.Text) ||
            string.IsNullOrWhiteSpace(txtCantidad.Text) ||
            string.IsNullOrWhiteSpace(txtTotal.Text))
            {
                MessageBox.Show("Complete todos los campos.");
                return;
            }

            int cantidad = int.Parse(txtCantidad.Text);
            decimal total = decimal.Parse(txtTotal.Text);

            using (MySqlConnection cn = new MySqlConnection(connectionString))
            {
                cn.Open();
                MySqlTransaction trans = cn.BeginTransaction();

                try
                {
                    // 1. Insertar Venta
                    string insertVenta = "INSERT INTO Venta (ID_Empleado, Total, Estado) VALUES (1, @total, 'Pagada')";
                    MySqlCommand cmdVenta = new MySqlCommand(insertVenta, cn, trans);
                    cmdVenta.Parameters.AddWithValue("@total", total);
                    cmdVenta.ExecuteNonQuery();

                    long idVenta = cmdVenta.LastInsertedId;

                    // 2. Obtener precio unitario del producto
                    string getPrecio = "SELECT PRECIO FROM Producto WHERE CLAVE = @clave";
                    MySqlCommand cmdPrecio = new MySqlCommand(getPrecio, cn, trans);
                    cmdPrecio.Parameters.AddWithValue("@clave", txtIDProducto.Text);
                    decimal precioUnitario = Convert.ToDecimal(cmdPrecio.ExecuteScalar());

                    // 3. Insertar DetalleVenta
                    string insertDetalle =
                        "INSERT INTO DetalleVenta (ID_Venta, CLAVE_Producto, Cantidad, PrecioUnitario, Subtotal) " +
                        "VALUES (@idv, @prod, @cant, @precio, @sub)";
                    MySqlCommand cmdDet = new MySqlCommand(insertDetalle, cn, trans);
                    cmdDet.Parameters.AddWithValue("@idv", idVenta);
                    cmdDet.Parameters.AddWithValue("@prod", txtIDProducto.Text);
                    cmdDet.Parameters.AddWithValue("@cant", cantidad);
                    cmdDet.Parameters.AddWithValue("@precio", precioUnitario);
                    cmdDet.Parameters.AddWithValue("@sub", precioUnitario * cantidad);
                    cmdDet.ExecuteNonQuery();

                    trans.Commit();

                    MessageBox.Show("Venta registrada correctamente.");
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToShortTimeString();
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }
    }
}
