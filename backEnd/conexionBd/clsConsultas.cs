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


    }
}
