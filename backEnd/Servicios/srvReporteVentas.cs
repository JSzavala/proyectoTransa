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

        /// <summary>
        /// Obtiene el reporte de ventas de un empleado específico entre dos fechas
        /// </summary>
        /// <param name="idEmpleado">ID del empleado a consultar</param>
        /// <param name="fechaInicio">Fecha inicial del período a consultar</param>
        /// <param name="fechaFin">Fecha final del período a consultar</param>
        /// <returns>DataTable con los detalles de ventas del empleado</returns>
        public DataTable ObtenerVentasDeEmpleadoPorFecha(int idEmpleado, DateTime fechaInicio, DateTime fechaFin)
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
      v.ID_Venta,
       v.FechaHoraVenta,
      v.Total,
      v.Estado,
                 e.ID_Empleado,
       CONCAT(e.Nombre, ' ', e.ApellidoPaterno, ' ', IFNULL(e.ApellidoMaterno, '')) AS NombreEmpleado
       FROM 
     Venta v
       INNER JOIN 
   Empleado e ON v.ID_Empleado = e.ID_Empleado
     WHERE 
e.ID_Empleado = @idEmpleado
            AND v.FechaHoraVenta BETWEEN @fechaInicio AND @fechaFin
           AND v.Estado = 'Pagada'
    ORDER BY 
    v.FechaHoraVenta DESC";

        var parametros = new Dictionary<string, object>
     {
                { "@idEmpleado", idEmpleado },
    { "@fechaInicio", fechaInicio },
   { "@fechaFin", fechaFin }
       };

       return consultas.SelectWithJoin(consulta, parametros);
 }
   catch (Exception ex)
            {
           Console.WriteLine("Error al obtener ventas del empleado: " + ex.Message);
     throw new Exception("Error al obtener las ventas del empleado: " + ex.Message, ex);
          }
     }

   /// <summary>
        /// Obtiene el total de ventas de todos los empleados entre dos fechas
        /// </summary>
        /// <param name="fechaInicio">Fecha inicial del período a consultar</param>
        /// <param name="fechaFin">Fecha final del período a consultar</param>
        /// <returns>Monto total de todas las ventas en el período</returns>
        public decimal ObtenerTotalVentasGeneralPorFecha(DateTime fechaInicio, DateTime fechaFin)
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
        IFNULL(SUM(v.Total), 0) AS TotalGeneral
    FROM 
  Venta v
         WHERE 
              v.FechaHoraVenta BETWEEN @fechaInicio AND @fechaFin
    AND v.Estado = 'Pagada'";

      var parametros = new Dictionary<string, object>
              {
         { "@fechaInicio", fechaInicio },
          { "@fechaFin", fechaFin }
   };

          DataTable resultado = consultas.Select(consulta, parametros);

      if (resultado.Rows.Count > 0)
      {
     return Convert.ToDecimal(resultado.Rows[0]["TotalGeneral"]);
      }

                return 0;
       }
         catch (Exception ex)
          {
                Console.WriteLine("Error al obtener total general de ventas: " + ex.Message);
    throw new Exception("Error al obtener el total general de ventas: " + ex.Message, ex);
            }
        }
    }
}
