using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoInventario.backEnd.POCOS
{
    /// <summary>
    /// Clase POCO para la tabla Producto
    /// </summary>
    internal class Producto
    {
    public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public short StockActual { get; set; }
        public byte Descontinuado { get; set; }
        public short StockMinimo { get; set; }
        public int IdProveedores { get; set; }
        public int IdCategoria { get; set; }

        // Propiedades de navegación (opcional)
        public Proveedor Proveedor { get; set; }
        public Categoria Categoria { get; set; }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        public Producto(int idProducto, string nombre, decimal precioCompra, decimal precioVenta, 
        short stockActual, byte descontinuado, short stockMinimo, int idProveedores, 
        int idCategoria, string descripcion = null)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Descripcion = descripcion;
            PrecioCompra = precioCompra;
            PrecioVenta = precioVenta;
            StockActual = stockActual;
            Descontinuado = descontinuado;
            StockMinimo = stockMinimo;
            IdProveedores = idProveedores;
            IdCategoria = idCategoria;
        }
    }
}
