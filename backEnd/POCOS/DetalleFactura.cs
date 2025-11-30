using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoInventario.backEnd.POCOS
{
    /// <summary>
    /// Clase POCO para la tabla detalleFactura
    /// </summary>
    internal class DetalleFactura
    {
        public int IdDetalleFactura { get; set; }
        public short Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }

        // Propiedades de navegación (opcional)
        public Factura Factura { get; set; }
        public Producto Producto { get; set; }

        /// <summary>
        /// Propiedad calculada para obtener el subtotal
        /// </summary>
        public decimal Subtotal
        {
            get { return Cantidad * PrecioUnitario; }
        }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        public DetalleFactura(int idDetalleFactura, short cantidad, decimal precioUnitario, 
        int idFactura, int idProducto)
        {
            IdDetalleFactura = idDetalleFactura;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            IdFactura = idFactura;
            IdProducto = idProducto;
        }
    }
}
