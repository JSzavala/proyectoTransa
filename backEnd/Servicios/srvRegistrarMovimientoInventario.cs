using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyectoInventario.backEnd.conexionBd;
using proyectoInventario.backEnd.POCOS;
using MySql.Data.MySqlClient;

namespace proyectoInventario.backEnd.Servicios
{
    internal class srvRegistrarMovimientoInventario
    {
        private clsConexionBd conexionBd;
        private clsConsultas consultas;

        /// <summary>
        /// Constructor que inicializa la conexión a la base de datos
        /// </summary>
        public srvRegistrarMovimientoInventario()
        {
            conexionBd = new clsConexionBd();
            consultas = new clsConsultas();
        }

        /// <summary>
        /// Registra una venta con sus detalles usando transacciones
        /// NOTA: En la nueva BD, las ventas se registran en la tabla Venta y DetalleVenta
        /// </summary>
        /// <param name="factura">Objeto Factura con los datos principales (se adapta a Venta)</param>
        /// <param name="detalles">Lista de DetalleFactura con los productos</param>
        /// <param name="idEmpleado">ID del empleado que realiza la venta</param>
        /// <returns>ID de la venta generada, o -1 si hubo error</returns>
        public long RegistrarVenta(int idEmpleado, List<DetalleFactura> detalles)
        {
            MySqlTransaction transaction = null;
            long idVentaGenerada = -1;

            try
            {
                // Validar que haya detalles
                if (detalles == null || detalles.Count == 0)
                {
                    throw new Exception("Debe agregar al menos un producto a la venta");
                }

                // Abrir conexión
                if (!conexionBd.AbrirConexion())
                {
                    throw new Exception("No se pudo abrir la conexión a la base de datos");
                }

                // Iniciar transacción
                transaction = conexionBd.ObtenerConexion().BeginTransaction();

                // 1. Calcular el total
                decimal total = CalcularTotalVenta(detalles);

                // 2. Insertar la venta
                idVentaGenerada = InsertarVenta(idEmpleado, total, transaction);

                if (idVentaGenerada <= 0)
                {
                    throw new Exception("Error al insertar la venta");
                }

                // 3. Insertar los detalles de la venta
                foreach (var detalle in detalles)
                {
                    long idDetalle = InsertarDetalleVenta((int)idVentaGenerada, detalle, transaction);

                    if (idDetalle <= 0)
                    {
                        throw new Exception($"Error al insertar el detalle del producto: {detalle.IdProducto}");
                    }

                    // 4. Actualizar el stock del producto (restar cantidad)
                    bool stockActualizado = ActualizarStockProducto(detalle.IdProducto, -detalle.Cantidad, transaction);

                    if (!stockActualizado)
                    {
                        throw new Exception($"Error al actualizar stock del producto: {detalle.IdProducto}");
                    }
                }

                // Confirmar transacción
                transaction.Commit();

                return idVentaGenerada;
            }
            catch (MySqlException ex)
            {
                // Revertir transacción en caso de error
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception rollbackEx)
                    {
                        Console.WriteLine("Error al hacer rollback: " + rollbackEx.Message);
                    }
                }

                Console.WriteLine("Error en RegistrarVenta: " + ex.Message);
                throw new Exception("Error al registrar la venta: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                // Revertir transacción en caso de error
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception rollbackEx)
                    {
                        Console.WriteLine("Error al hacer rollback: " + rollbackEx.Message);
                    }
                }

                Console.WriteLine("Error en RegistrarVenta: " + ex.Message);
                throw;
            }
            finally
            {
                // Cerrar conexión
                conexionBd.CerrarConexion();
            }
        }

        /// <summary>
        /// Inserta una venta en la base de datos
        /// </summary>
        /// <param name="idEmpleado">ID del empleado que realiza la venta</param>
        /// <param name="total">Total de la venta</param>
        /// <param name="transaction">Transacción activa</param>
        /// <returns>ID de la venta generada</returns>
        private long InsertarVenta(int idEmpleado, decimal total, MySqlTransaction transaction)
        {
            string consulta = @"INSERT INTO Venta (ID_Empleado, Total, Estado) 
            VALUES (@idEmpleado, @total, 'Pagada')";
            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@idEmpleado", idEmpleado },
                { "@total", total }
            };
            return consultas.Insert(consulta, parametros, transaction);
        }

        /// <summary>
        /// Inserta un detalle de venta en la base de datos
        /// </summary>
        /// <param name="idVenta">ID de la venta</param>
        /// <param name="detalle">Objeto DetalleFactura a insertar</param>
        /// <param name="transaction">Transacción activa</param>
        /// <returns>ID del detalle generado</returns>
        private long InsertarDetalleVenta(int idVenta, DetalleFactura detalle, MySqlTransaction transaction)
        {
            // Calcular subtotal
            decimal subtotal = detalle.Cantidad * detalle.PrecioUnitario;

            string consulta = @"INSERT INTO DetalleVenta (ID_Venta, CLAVE_Producto, Cantidad, PrecioUnitario, Subtotal) 
            VALUES (@idVenta, @claveProducto, @cantidad, @precioUnitario, @subtotal)";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@idVenta", idVenta },
                { "@claveProducto", detalle.IdProducto }, // NOTA: IdProducto ahora contiene la CLAVE
                { "@cantidad", detalle.Cantidad },
                { "@precioUnitario", detalle.PrecioUnitario },
                { "@subtotal", subtotal }
            };

            return consultas.Insert(consulta, parametros, transaction);
        }

        /// <summary>
        /// Actualiza el stock de un producto
        /// </summary>
        /// <param name="claveProducto">CLAVE del producto</param>
        /// <param name="cantidad">Cantidad a sumar/restar al stock (positivo para compras, negativo para ventas)</param>
        /// <param name="transaction">Transacción activa</param>
        /// <returns>True si se actualizó correctamente</returns>
        private bool ActualizarStockProducto(int claveProducto, int cantidad, MySqlTransaction transaction)
        {
            string consulta = @"UPDATE Producto 
            SET STOCK = STOCK + @cantidad 
            WHERE CLAVE = @clave";

            using (MySqlCommand comando = new MySqlCommand(consulta, conexionBd.ObtenerConexion(), transaction))
            {
                comando.Parameters.AddWithValue("@cantidad", cantidad);
                comando.Parameters.AddWithValue("@clave", claveProducto.ToString());

                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        /// <summary>
        /// Calcula el total de la venta basándose en los detalles
        /// </summary>
        /// <param name="detalles">Lista de detalles de venta</param>
        /// <returns>Total calculado</returns>
        public decimal CalcularTotalVenta(List<DetalleFactura> detalles)
        {
            decimal total = 0;

            if (detalles != null)
            {
                foreach (var detalle in detalles)
                {
                    total += detalle.Subtotal;
                }
            }
            return total;
        }

        /// <summary>
        /// Valida los datos de una venta antes de registrarla
        /// </summary>
        /// <param name="detalles">Detalles a validar</param>
        /// <returns>Mensaje de error o cadena vacía si todo está correcto</returns>
        public string ValidarVenta(List<DetalleFactura> detalles)
        {
            if (detalles == null || detalles.Count == 0)
            {
                return "Debe agregar al menos un producto a la venta";
            }

            foreach (var detalle in detalles)
            {
                if (detalle.Cantidad <= 0)
                {
                    return "La cantidad de los productos debe ser mayor a cero";
                }

                if (detalle.PrecioUnitario <= 0)
                {
                    return "El precio unitario debe ser mayor a cero";
                }

                if (string.IsNullOrEmpty(detalle.IdProducto.ToString()))
                {
                    return "Debe seleccionar un producto válido";
                }
            }

            return string.Empty;
        }
    }
}
