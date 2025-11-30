/*
 * Created by SharpDevelop.
 * User: compu1
 * Date: 24/11/2025
 * Time: 08:53 p. m.
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
    /// Description of RegistrarCompra.
    /// </summary>
    public partial class RegistrarCompra : Form
    {
        private clsConsultas consultas;
        private DataTable dtProductosCompra;
        
  public RegistrarCompra()
        {
     //
      // The InitializeComponent() call is required for Windows Forms designer support.
       //
        InitializeComponent();
   
     //
       // TODO: Add constructor code after the InitializeComponent() call.
            //
    consultas = new clsConsultas();
   InicializarDataGridView();
        }

        /// <summary>
        /// Inicializa el DataGridView con las columnas necesarias para la compra
        /// </summary>
        private void InicializarDataGridView()
   {
   // Crear DataTable para almacenar los productos
            dtProductosCompra = new DataTable();
      dtProductosCompra.Columns.Add("IdProducto", typeof(int));
            dtProductosCompra.Columns.Add("Nombre", typeof(string));
    dtProductosCompra.Columns.Add("Cantidad", typeof(int));
            dtProductosCompra.Columns.Add("PrecioVenta", typeof(decimal));
      dtProductosCompra.Columns.Add("Subtotal", typeof(decimal));
       
            // Asignar el DataTable al DataGridView
            dgvProductos.DataSource = dtProductosCompra;
            
    // Configurar el estilo del DataGridView
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
     }

      private void btnRegistrarProducto_Click(object sender, EventArgs e)
        {
      try
 {
 // Validar que los campos no estén vacíos
        if (string.IsNullOrWhiteSpace(txtIDProducto.Text))
    {
            MessageBox.Show("Por favor, ingrese el nombre del producto.", "Campo requerido", 
MessageBoxButtons.OK, MessageBoxIcon.Warning);
              txtIDProducto.Focus();
       return;
    }
    
  if (string.IsNullOrWhiteSpace(txtCantidad.Text))
   {
                  MessageBox.Show("Por favor, ingrese la cantidad.", "Campo requerido", 
     MessageBoxButtons.OK, MessageBoxIcon.Warning);
          txtCantidad.Focus();
return;
           }
    
    if (string.IsNullOrWhiteSpace(txtPrecioVenta.Text))
          {
    MessageBox.Show("Por favor, ingrese el precio de venta.", "Campo requerido", 
      MessageBoxButtons.OK, MessageBoxIcon.Warning);
txtPrecioVenta.Focus();
        return;
     }
   
           // Validar que cantidad y precio sean numéricos
   if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
      {
        MessageBox.Show("La cantidad debe ser un número entero positivo.", "Valor inválido", 
    MessageBoxButtons.OK, MessageBoxIcon.Warning);
      txtCantidad.Focus();
       return;
          }
        
    if (!decimal.TryParse(txtPrecioVenta.Text, out decimal precioVenta) || precioVenta <= 0)
         {
         MessageBox.Show("El precio de venta debe ser un número positivo.", "Valor inválido", 
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
           txtPrecioVenta.Focus();
 return;
   }
     
    string nombreProducto = txtIDProducto.Text.Trim();
      
    // Buscar el producto en la base de datos por nombre
string consulta = @"SELECT CLAVE, NOMBRE, DESCRIPCION, PRECIO, STOCK 
  FROM Producto 
  WHERE NOMBRE LIKE @nombre";
    
  var parametros = new Dictionary<string, object>
            {
      { "@nombre", "%" + nombreProducto + "%" }
       };
      
         DataTable resultado = consultas.Select(consulta, parametros);

         // Verificar si se encontró el producto
           if (resultado.Rows.Count == 0)
       {
          MessageBox.Show($"No se encontró ningún producto con el nombre '{nombreProducto}'.", 
  "Producto no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
          return;
      }
  
          if (resultado.Rows.Count > 1)
 {
      MessageBox.Show("Se encontraron múltiples productos con ese nombre. Por favor, sea más específico.", 
            "Múltiples resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
       return;
     }
   
   // Obtener los datos del producto
    DataRow producto = resultado.Rows[0];
  string claveProducto = producto["CLAVE"].ToString();
           string nombre = producto["NOMBRE"].ToString();
       
      // Verificar si el producto ya está en el DataGridView
    foreach (DataRow row in dtProductosCompra.Rows)
      {
if (row["IdProducto"].ToString() == claveProducto)
   {
         MessageBox.Show("Este producto ya ha sido agregado a la compra.", 
     "Producto duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
     return;
        }
      }
    
     // Calcular el subtotal
    decimal subtotal = cantidad * precioVenta;
 
 // Agregar el producto al DataTable
 DataRow nuevaFila = dtProductosCompra.NewRow();
   nuevaFila["IdProducto"] = claveProducto;
    nuevaFila["Nombre"] = nombre;
    nuevaFila["Cantidad"] = cantidad;
       nuevaFila["PrecioVenta"] = precioVenta;
     nuevaFila["Subtotal"] = subtotal;
           
  dtProductosCompra.Rows.Add(nuevaFila);
          
                // Limpiar los campos
  txtIDProducto.Clear();
            txtCantidad.Clear();
 txtPrecioVenta.Clear();
         txtIDProducto.Focus();
    
          MessageBox.Show($"Producto '{nombre}' agregado correctamente a la compra.", 
             "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
    {
                MessageBox.Show($"Error al agregar el producto: {ex.Message}", 
             "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
   }
        
    private void BtnVolver_Click(object sender, EventArgs e)
        {
    Main frmMain = new Main();
       frmMain.Show();
this.Close();
        }
    }
}
