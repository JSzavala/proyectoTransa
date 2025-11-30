using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoInventario.backEnd.POCOS
{
    /// <summary>
    /// Enumeración para el tipo de factura
    /// </summary>
    public enum TipoFactura
    {
        Compra,
        Venta
    }

    /// <summary>
    /// Clase POCO para la tabla Factura
    /// </summary>
    internal class Factura
    {
        public int IdFacturaVenta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public TipoFactura TipoFactura { get; set; }

        // Propiedad de navegación para los detalles de la factura
        public List<DetalleFactura> Detalles { get; set; }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        public Factura(int idFacturaVenta, DateTime fecha, decimal total, TipoFactura tipoFactura)
        {
           IdFacturaVenta = idFacturaVenta;
            Fecha = fecha;
            Total = total;
            TipoFactura = tipoFactura;
            Detalles = new List<DetalleFactura>();
        }
    }
}
