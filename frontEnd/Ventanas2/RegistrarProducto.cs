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
        /// Carga todos los productos desde la base de datos
        /// </summary>
        private void CargarProductos()
  {
            try
     {
      string consulta = "SELECT CLAVE, NOMBRE, DESCRIPCION, PRECIO, STOCK FROM Producto ORDER BY CLAVE";
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
                string consulta = @"INSERT INTO Producto (CLAVE, NOMBRE, DESCRIPCION, PRECIO, STOCK) 
           VALUES (@clave, @nombre, @descripcion, @precio, @stock)";
         
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
    }
}
