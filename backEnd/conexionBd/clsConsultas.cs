using MySql.Data.MySqlClient;
using proyectoInventario.backEnd.POCOS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoInventario.backEnd.conexionBd
{
    internal class clsConsultas
    {
        private clsConexionBd conexionBd;

        /// <summary>
        /// Constructor que inicializa la conexión a la base de datos
        /// </summary>
        public clsConsultas()
        {
                conexionBd = new clsConexionBd();
        }

         /// <summary>
         /// Método genérico para SELECT - Consulta datos de la base de datos
         /// </summary>
         /// <param name="consulta">Consulta SQL SELECT con parámetros (@parametro)</param>
         /// <param name="parametros">Diccionario con los parámetros y sus valores</param>
         /// <returns>DataTable con los resultados de la consulta</returns>
        public DataTable Select(string consulta, Dictionary<string, object> parametros = null)
        {
            DataTable tabla = new DataTable();

            try
            {
                if (conexionBd.AbrirConexion())
                {
                    using (MySqlCommand comando = new MySqlCommand(consulta, conexionBd.ObtenerConexion()))
                    {
                        // Agregar parámetros de forma segura
                        if (parametros != null)
                        {
                            foreach (var parametro in parametros)
                            {
                                comando.Parameters.AddWithValue(parametro.Key, parametro.Value ?? DBNull.Value);
                            }
                        }

                        using (MySqlDataAdapter adaptador = new MySqlDataAdapter(comando))
                        {
                            adaptador.Fill(tabla);
                        }
                    }

                    conexionBd.CerrarConexion();
                }
            }
            catch (MySqlException ex)
            {
                 Console.WriteLine("Error en SELECT: " + ex.Message);
                throw new Exception("Error al ejecutar consulta SELECT: " + ex.Message, ex);
            }

            return tabla;
        }

        /// <summary>
        /// Método genérico para INSERT - Inserta datos en la base de datos
        /// </summary>
        /// <param name="consulta">Consulta SQL INSERT con parámetros (@parametro)</param>
        /// <param name="parametros">Diccionario con los parámetros y sus valores</param>
        /// <returns>ID del último registro insertado (si aplica)</returns>
        public long Insert(string consulta, Dictionary<string, object> parametros)
        {
            long idGenerado = 0;

            try
            {
                if (conexionBd.AbrirConexion())
                {
                    using (MySqlCommand comando = new MySqlCommand(consulta, conexionBd.ObtenerConexion()))
                    {
                        // Agregar parámetros de forma segura
                        if (parametros != null)
                        {
                            foreach (var parametro in parametros)
                            {
                                comando.Parameters.AddWithValue(parametro.Key, parametro.Value ?? DBNull.Value);
                            }
                        }

                        comando.ExecuteNonQuery();
                        idGenerado = comando.LastInsertedId;
                    }

                    conexionBd.CerrarConexion();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error en INSERT: " + ex.Message);
                throw new Exception("Error al ejecutar INSERT: " + ex.Message, ex);
            }

          return idGenerado;
        }

        /// <summary>
        /// Método genérico para INSERT con transacción - Inserta datos en la base de datos
        /// </summary>
        /// <param name="consulta">Consulta SQL INSERT con parámetros (@parametro)</param>
        /// <param name="parametros">Diccionario con los parámetros y sus valores</param>
        /// <param name="transaction">Transacción activa</param>
        /// <returns>ID del último registro insertado (si aplica)</returns>
        public long Insert(string consulta, Dictionary<string, object> parametros, MySqlTransaction transaction)
        {
            long idGenerado = 0;

            try
            {
                using (MySqlCommand comando = new MySqlCommand(consulta, transaction.Connection, transaction))
                {
                    // Agregar parámetros de forma segura
                    if (parametros != null)
                    {
                        foreach (var parametro in parametros)
                        {
                            comando.Parameters.AddWithValue(parametro.Key, parametro.Value ?? DBNull.Value);
                        }
                    }

                    comando.ExecuteNonQuery();
                    idGenerado = comando.LastInsertedId;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error en INSERT (transacción): " + ex.Message);
                throw new Exception("Error al ejecutar INSERT: " + ex.Message, ex);
            }

            return idGenerado;
        }

        /// <summary>
        /// Método genérico para UPDATE - Actualiza datos en la base de datos
        /// </summary>
        /// <param name="consulta">Consulta SQL UPDATE con parámetros (@parametro)</param>
        /// <param name="parametros">Diccionario con los parámetros y sus valores</param>
        /// <returns>Número de filas afectadas</returns>
        public int Update(string consulta, Dictionary<string, object> parametros)
        {
            int filasAfectadas = 0;

            try
            {
                if (conexionBd.AbrirConexion())
                {
                    using (MySqlCommand comando = new MySqlCommand(consulta, conexionBd.ObtenerConexion()))
                    {
                        // Agregar parámetros de forma segura
                        if (parametros != null)
                        {
                            foreach (var parametro in parametros)
                            {
                                comando.Parameters.AddWithValue(parametro.Key, parametro.Value ?? DBNull.Value);
                            }
                        }

                        filasAfectadas = comando.ExecuteNonQuery();
                    }

                conexionBd.CerrarConexion();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error en UPDATE: " + ex.Message);
                throw new Exception("Error al ejecutar UPDATE: " + ex.Message, ex);
            }
            return filasAfectadas;
        }

        /// <summary>
        /// Método genérico para UPDATE con transacción - Actualiza datos en la base de datos
        /// </summary>
        /// <param name="consulta">Consulta SQL UPDATE con parámetros (@parametro)</param>
        /// <param name="parametros">Diccionario con los parámetros y sus valores</param>
        /// <param name="transaction">Transacción activa</param>
        /// <returns>Número de filas afectadas</returns>
        public int Update(string consulta, Dictionary<string, object> parametros, MySqlTransaction transaction)
        {
            int filasAfectadas = 0;

            try
            {
                using (MySqlCommand comando = new MySqlCommand(consulta, transaction.Connection, transaction))
                {
                    // Agregar parámetros de forma segura
                    if (parametros != null)
                    {
                        foreach (var parametro in parametros)
                        {
                            comando.Parameters.AddWithValue(parametro.Key, parametro.Value ?? DBNull.Value);
                        }
                    }

                    filasAfectadas = comando.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error en UPDATE (transacción): " + ex.Message);
                throw new Exception("Error al ejecutar UPDATE: " + ex.Message, ex);
            }

            return filasAfectadas;
        }

        /// <summary>
        /// Método genérico para DELETE - Elimina datos de la base de datos
        /// </summary>
        /// <param name="consulta">Consulta SQL DELETE con parámetros (@parametro)</param>
        /// <param name="parametros">Diccionario con los parámetros y sus valores</param>
        /// <returns>Número de filas eliminadas</returns>
        public int Delete(string consulta, Dictionary<string, object> parametros)
        {
            int filasEliminadas = 0;

            try
            {
                if (conexionBd.AbrirConexion())
                {
                    using (MySqlCommand comando = new MySqlCommand(consulta, conexionBd.ObtenerConexion()))
                    {
                        // Agregar parámetros de forma segura
                        if (parametros != null)
                        {
                            foreach (var parametro in parametros)
                            {
                                comando.Parameters.AddWithValue(parametro.Key, parametro.Value ?? DBNull.Value);
                            }
                        }
                    filasEliminadas = comando.ExecuteNonQuery();
                    }
                    conexionBd.CerrarConexion();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error en DELETE: " + ex.Message);
                throw new Exception("Error al ejecutar DELETE: " + ex.Message, ex);
            }

            return filasEliminadas;
        }
        
        // tomando en cuenta que tenos en la bd lo tipos de usirios
        // los utotentificamos si existe de que tipo es y sin envimos un mensaje de error
        public Usuario AutenticarUsuario(string nombreUsuario, string contrasena)
        {
            string consulta = "CALL sp_AutenticarUsuario(@p_NombreUsuario, @p_Contrasena)";
            var parametros = new Dictionary<string, object>
            {
                {"@p_NombreUsuario", nombreUsuario},
                {"@p_Contrasena", contrasena}
            };

            DataTable resultado = Select(consulta, parametros);

            if (resultado.Rows.Count > 0)
            {
                DataRow fila = resultado.Rows[0];
                return new Usuario(
                    Convert.ToInt32(fila["ID_Usuario"]),
                    fila["NombreUsuario"].ToString(),
                    nombreUsuario,
                    contrasena,
                    fila["NombreTipo"].ToString() == "Administrador" ? RolUsuario.Dueña : RolUsuario.Empleado,
                    true
                );
            }

            return null; // si no encontró usuario
        }

        // se realiza la actualización en el producto para marcarlo como descontinuado
        // NOTA: En la nueva BD no existe el campo Descontinuado
    // Se puede implementar eliminando el producto o manejándolo de otra forma
        public bool DescontinuarProducto(string claveProducto)
  {
       // Opción 1: Eliminar el producto (según nueva BD)
            string consulta = "DELETE FROM Producto WHERE CLAVE = @clave";
   var parametros = new Dictionary<string, object>
   {
 {"@clave", claveProducto}
            };

            int filas = Delete(consulta, parametros);
            return filas > 0;
            
        // NOTA: Si se desea mantener un registro histórico, considere:
       // 1. Crear una tabla ProductosDescontinuados
    // 2. Mover el producto a esa tabla antes de eliminarlo
     // 3. O agregar un campo ACTIVO a la tabla Producto
      }

        /// <summary>
        /// Método genérico para SELECT con JOIN y WHERE - Consulta datos de múltiples tablas relacionadas
        /// </summary>
        /// <param name="consulta">Consulta SQL SELECT con JOIN y WHERE, usando parámetros (@parametro)</param>
        /// <param name="parametros">Diccionario con los parámetros y sus valores</param>
        /// <returns>DataTable con los resultados de la consulta</returns>
        public DataTable SelectWithJoin(string consulta, Dictionary<string, object> parametros = null)
        {
            DataTable tabla = new DataTable();

            try
            {
                if (conexionBd.AbrirConexion())
                {
                    using (MySqlCommand comando = new MySqlCommand(consulta, conexionBd.ObtenerConexion()))
                    {
                        // Agregar parámetros de forma segura
                        if (parametros != null)
                        {
                            foreach (var parametro in parametros)
                            {
                                comando.Parameters.AddWithValue(parametro.Key, parametro.Value ?? DBNull.Value);
                            }
                        }

                        using (MySqlDataAdapter adaptador = new MySqlDataAdapter(comando))
                        {
                            adaptador.Fill(tabla);
                        }
                    }

                    conexionBd.CerrarConexion();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error en SELECT con JOIN: " + ex.Message);
                throw new Exception("Error al ejecutar consulta SELECT con JOIN: " + ex.Message, ex);
            }

            return tabla;

        }

        public DataTable ObtenerVentasPorFecha(string mes, string anio)
        {
        // Validamos que vengan datos antes de ir a la BD
       if (string.IsNullOrEmpty(mes) || string.IsNullOrEmpty(anio))
   {
     return null;
            }

            //Creo aqui hay problemas con el nombre de algunas columnas, por si luego a 
       //alguien no le funciona es por eso
  string sql = @"
         SELECT 
             P.Nombre AS 'Titulo Libro',
    P.Descripcion AS 'Genero/Desc',
          SUM(DV.Cantidad) AS 'Unidades Vendidas',
                    SUM(DV.Subtotal) AS 'Total Ingresos'
 FROM Venta V
       INNER JOIN DetalleVenta DV ON V.ID_Venta = DV.ID_Venta
         INNER JOIN Producto P ON DV.CLAVE_Producto = P.CLAVE
      WHERE MONTH(V.FechaHoraVenta) = @Mes 
           AND YEAR(V.FechaHoraVenta) = @Anio
         GROUP BY P.CLAVE, P.Nombre, P.Descripcion
     ORDER BY SUM(DV.Cantidad) DESC";

            // 2. Preparamos los parámetros usando tu estructura
      var parametros = new Dictionary<string, object>
       {
           { "@Mes", mes },
     { "@Anio", anio }
            };

    // 3. Ejecutamos usando el método de instancia actual
          return SelectWithJoin(sql, parametros);
        }

  /// <summary>
        /// Obtiene todos los registros de auditoría de productos
        /// </summary>
    /// <returns>DataTable con todos los cambios registrados en AuditoriaProducto</returns>
        public DataTable ObtenerTodosLosCambios()
 {
            string consulta = @"
         SELECT 
        ID_Auditoria AS 'ID',
        CLAVE_Producto AS 'Clave Producto',
           FechaHoraCambio AS 'Fecha/Hora',
              TipoCambio AS 'Tipo de Cambio',
   CampoModificado AS 'Campo',
           ValorAnterior AS 'Valor Anterior',
           ValorNuevo AS 'Valor Nuevo',
  UsuarioDB AS 'Usuario BD'
             FROM AuditoriaProducto
             ORDER BY FechaHoraCambio DESC";

            return Select(consulta);
        }

        /// <summary>
        /// Obtiene solo los registros de auditoría con tipo UPDATE
/// </summary>
   /// <returns>DataTable con los cambios tipo UPDATE de AuditoriaProducto</returns>
        public DataTable ObtenerCambiosUpdate()
{
     string consulta = @"
         SELECT 
     ID_Auditoria AS 'ID',
 CLAVE_Producto AS 'Clave Producto',
      FechaHoraCambio AS 'Fecha/Hora',
    TipoCambio AS 'Tipo de Cambio',
    CampoModificado AS 'Campo',
  ValorAnterior AS 'Valor Anterior',
  ValorNuevo AS 'Valor Nuevo',
  UsuarioDB AS 'Usuario BD'
         FROM AuditoriaProducto
          WHERE TipoCambio = @TipoCambio
       ORDER BY FechaHoraCambio DESC";

            var parametros = new Dictionary<string, object>
       {
  { "@TipoCambio", "UPDATE" }
  };

    return Select(consulta, parametros);
 }

 /// <summary>
        /// Obtiene el reporte de ventas por empleado usando la función de BD fn_TotalVentasPorRango
      /// </summary>
        /// <param name="fechaInicio">Fecha inicial del período a consultar</param>
  /// <param name="fechaFin">Fecha final del período a consultar</param>
        /// <returns>DataTable con ID_Empleado, Nombre completo, Total de ventas y Monto total</returns>
        public DataTable ObtenerVentasPorEmpleado(DateTime fechaInicio, DateTime fechaFin)
  {
            // Consulta que obtiene ventas por empleado y usa la función de BD para validación
          string consulta = @"
       SELECT 
        e.ID_Empleado AS 'ID Empleado',
     CONCAT(e.Nombre, ' ', e.ApellidoPaterno, ' ', IFNULL(e.ApellidoMaterno, '')) AS 'Nombre Completo',
  COUNT(v.ID_Venta) AS 'Total Ventas',
          SUM(v.Total) AS 'Monto Total',
      fn_TotalVentasPorRango(@fechaInicio, @fechaFin) AS 'Total General'
      FROM Empleado e
    INNER JOIN Venta v ON e.ID_Empleado = v.ID_Empleado
       WHERE v.FechaHoraVenta BETWEEN @fechaInicio AND @fechaFin
   AND v.Estado = 'Pagada'
  GROUP BY e.ID_Empleado, e.Nombre, e.ApellidoPaterno, e.ApellidoMaterno
    ORDER BY SUM(v.Total) DESC";

            var parametros = new Dictionary<string, object>
    {
           { "@fechaInicio", fechaInicio },
  { "@fechaFin", fechaFin }
 };

            return SelectWithJoin(consulta, parametros);
        }

        /// <summary>
        /// Obtiene todos los tipos de usuario disponibles
        /// </summary>
        /// <returns>DataTable con ID_TipoUsuario y NombreTipo</returns>
        public DataTable ObtenerTiposUsuario()
        {
  string consulta = "SELECT ID_TipoUsuario, NombreTipo FROM TipoUsuario";
     return Select(consulta);
  }

        /// <summary>
        /// Inserta un nuevo usuario en la base de datos
        /// </summary>
/// <param name="nombreUsuario">Nombre de usuario único</param>
        /// <param name="contrasena">Contraseña del usuario</param>
        /// <param name="idTipoUsuario">ID del tipo de usuario (1=Administrador, 2=Vendedor)</param>
        /// <returns>ID del usuario creado</returns>
        public long InsertarUsuario(string nombreUsuario, string contrasena, int idTipoUsuario)
   {
            string consulta = @"INSERT INTO Usuario (NombreUsuario, Contrasena, ID_TipoUsuario, Activo) 
    VALUES (@nombreUsuario, @contrasena, @idTipoUsuario, TRUE)";
     
          var parametros = new Dictionary<string, object>
   {
         { "@nombreUsuario", nombreUsuario },
           { "@contrasena", contrasena },
       { "@idTipoUsuario", idTipoUsuario }
    };

  return Insert(consulta, parametros);
    }

  /// <summary>
        /// Inserta un nuevo empleado usando el stored procedure sp_InsertarEmpleado
/// </summary>
        /// <param name="nombre">Nombre del empleado</param>
        /// <param name="apellidoPaterno">Apellido paterno del empleado</param>
        /// <param name="apellidoMaterno">Apellido materno del empleado (opcional)</param>
        /// <param name="rfc">RFC del empleado (opcional pero único)</param>
    /// <param name="idUsuario">ID del usuario asociado (puede ser null)</param>
        /// <returns>ID del empleado creado</returns>
        public long InsertarEmpleado(string nombre, string apellidoPaterno, string apellidoMaterno, string rfc, int? idUsuario)
        {
            string consulta = "CALL sp_InsertarEmpleado(@p_Nombre, @p_ApellidoPaterno, @p_ApellidoMaterno, @p_RFC, @p_ID_Usuario)";
        
            var parametros = new Dictionary<string, object>
            {
                { "@p_Nombre", nombre },
                { "@p_ApellidoPaterno", apellidoPaterno },
                { "@p_ApellidoMaterno", apellidoMaterno },
                { "@p_RFC", rfc },
                { "@p_ID_Usuario", idUsuario }
            };

            // Ejecutar el procedimiento y obtener el ID generado
            try
            {
                if (conexionBd.AbrirConexion())
                {
                    using (MySqlCommand comando = new MySqlCommand(consulta, conexionBd.ObtenerConexion()))
                    {
                        foreach (var parametro in parametros)
                        {
                            comando.Parameters.AddWithValue(parametro.Key, parametro.Value ?? DBNull.Value);
                        }

                        comando.ExecuteNonQuery();
      
                        // Obtener el último ID insertado
                        MySqlCommand cmdLastId = new MySqlCommand("SELECT LAST_INSERT_ID()", conexionBd.ObtenerConexion());
                        long idGenerado = Convert.ToInt64(cmdLastId.ExecuteScalar());
  
                        conexionBd.CerrarConexion();
                        return idGenerado;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al insertar empleado: " + ex.Message);
                throw new Exception("Error al insertar empleado: " + ex.Message, ex);
            }

            return 0;
        }

        /// <summary>
        /// Actualiza un empleado existente usando el stored procedure sp_ActualizarEmpleado
        /// </summary>
        public int ActualizarEmpleado(int idEmpleado, string nombre, string apellidoPaterno, string apellidoMaterno, string rfc, int? idUsuario)
        {
            string consulta = "CALL sp_ActualizarEmpleado(@p_ID_Empleado, @p_Nombre, @p_ApellidoPaterno, @p_ApellidoMaterno, @p_RFC, @p_ID_Usuario)";
       
        var parametros = new Dictionary<string, object>
        {
 { "@p_ID_Empleado", idEmpleado },
    { "@p_Nombre", nombre },
                { "@p_ApellidoPaterno", apellidoPaterno },
      { "@p_ApellidoMaterno", apellidoMaterno },
   { "@p_RFC", rfc },
    { "@p_ID_Usuario", idUsuario }
            };

            try
 {
       if (conexionBd.AbrirConexion())
    {
       using (MySqlCommand comando = new MySqlCommand(consulta, conexionBd.ObtenerConexion()))
   {
foreach (var parametro in parametros)
            {
     comando.Parameters.AddWithValue(parametro.Key, parametro.Value ?? DBNull.Value);
  }

         int filasAfectadas = comando.ExecuteNonQuery();
        conexionBd.CerrarConexion();
   return filasAfectadas;
         }
    }
            }
  catch (MySqlException ex)
    {
    Console.WriteLine("Error al actualizar empleado: " + ex.Message);
             throw new Exception("Error al actualizar empleado: " + ex.Message, ex);
       }

            return 0;
        }

        /// <summary>
        /// Elimina un empleado usando el stored procedure sp_EliminarEmpleado
      /// </summary>
        public int EliminarEmpleado(int idEmpleado)
        {
 string consulta = "CALL sp_EliminarEmpleado(@p_ID_Empleado)";
         
         var parametros = new Dictionary<string, object>
       {
 { "@p_ID_Empleado", idEmpleado }
            };

    try
            {
  if (conexionBd.AbrirConexion())
   {
       using (MySqlCommand comando = new MySqlCommand(consulta, conexionBd.ObtenerConexion()))
          {
   comando.Parameters.AddWithValue("@p_ID_Empleado", idEmpleado);
       int filasAfectadas = comando.ExecuteNonQuery();
   conexionBd.CerrarConexion();
            return filasAfectadas;
             }
                }
     }
   catch (MySqlException ex)
         {
       Console.WriteLine("Error al eliminar empleado: " + ex.Message);
                throw new Exception("Error al eliminar empleado: " + ex.Message, ex);
        }

   return 0;
        }

        /// <summary>
        /// Lista todos los empleados usando el stored procedure sp_ListarEmpleados
      /// </summary>
        public DataTable ListarEmpleados()
    {
       string consulta = "CALL sp_ListarEmpleados()";
     return Select(consulta);
        }

   /// <summary>
        /// Obtiene todos los usuarios activos con su información completa
        /// </summary>
      /// <returns>DataTable con ID_Usuario, NombreCompleto, NombreUsuario, NombreTipo</returns>
        public DataTable ObtenerUsuariosActivos()
      {
            string consulta = @"
       SELECT 
    U.ID_Usuario AS 'ID',
     CASE 
     WHEN E.ID_Empleado IS NOT NULL THEN 
        CONCAT(E.Nombre, ' ', E.ApellidoPaterno, ' ', IFNULL(E.ApellidoMaterno, ''))
  ELSE 
       U.NombreUsuario
          END AS 'Nombre Completo',
      U.NombreUsuario AS 'Nombre de Usuario',
          T.NombreTipo AS 'Tipo de Usuario'
       FROM Usuario U
          INNER JOIN TipoUsuario T ON U.ID_TipoUsuario = T.ID_TipoUsuario
        LEFT JOIN Empleado E ON U.ID_Usuario = E.ID_Usuario
       WHERE U.Activo = TRUE
                ORDER BY U.ID_Usuario DESC";

          return Select(consulta);
   }

        /// <summary>
        /// Desactiva un usuario (marca Activo = FALSE) buscándolo por nombre de usuario
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario a desactivar</param>
        /// <returns>Número de filas afectadas</returns>
        public int DesactivarUsuario(string nombreUsuario)
        {
string consulta = @"UPDATE Usuario 
   SET Activo = FALSE 
         WHERE NombreUsuario = @nombreUsuario";

      var parametros = new Dictionary<string, object>
     {
 { "@nombreUsuario", nombreUsuario }
       };

            return Update(consulta, parametros);
        }

    }
}
