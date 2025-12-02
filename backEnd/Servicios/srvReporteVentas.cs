using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyectoInventario.backEnd.conexionBd;

namespace proyectoInventario.backEnd.Servicios
{
    internal class srvReporteVentas
    {
        private clsConsultas consultas;

        /// <summary>
        /// Constructor que inicializa la clase de consultas
        /// </summary>
        public srvReporteVentas()
        {
            consultas = new clsConsultas();
        }

        /// <summary>
        /// Obtiene el reporte de ventas por empleado entre dos fechas
        /// </summary>
        /// <param name="fechaInicio">Fecha inicial del período a consultar</param>
        /// <param name="fechaFin">Fecha final del período a consultar</param>
        /// <returns>DataTable con ID_Empleado, Nombre completo del empleado y Total de ventas</returns>
        public DataTable ObtenerVentasPorEmpleadoPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                // Validar que la fecha de inicio sea menor o igual a la fecha fin
                if (fechaInicio > fechaFin)
                {
                    throw new Exception("La fecha de inicio no puede ser mayor a la fecha fin");
                }

                string consulta = @"
                SELECT 
                e.ID_Empleado,
                CONCAT(e.Nombre, ' ', e.ApellidoPaterno, ' ', IFNULL(e.ApellidoMaterno, '')) AS NombreCompleto,
                COUNT(v.ID_Venta) AS TotalVentas,
                SUM(v.Total) AS MontoTotal
                FROM 
                Empleado e
                INNER JOIN 
                Venta v ON e.ID_Empleado = v.ID_Empleado
                WHERE 
                v.FechaHoraVenta BETWEEN @fechaInicio AND @fechaFin
                AND v.Estado = 'Pagada'
                GROUP BY 
                e.ID_Empleado, e.Nombre, e.ApellidoPaterno, e.ApellidoMaterno
                ORDER BY 
                MontoTotal DESC";

                var parametros = new Dictionary<string, object>
                {
                    { "@fechaInicio", fechaInicio },
                    { "@fechaFin", fechaFin }
                };

                return consultas.SelectWithJoin(consulta, parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener reporte de ventas por empleado: " + ex.Message);
                throw new Exception("Error al obtener el reporte de ventas por empleado: " + ex.Message, ex);
            }
        }
    }
}
