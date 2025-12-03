using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace proyectoInventario.backEnd.conexionBd
{
    internal class clsConexionBd
    {
        private MySqlConnection conexion;
        private string cadenaConexion;

        /// <summary>
        /// Constructor que inicializa la cadena de conexión a la base de datos MySQL
        /// </summary>
        public clsConexionBd()
        {
            // Configurar la cadena de conexión
            // Ajusta los valores según tu configuración de BD
            cadenaConexion = "Server=localhost;Database=VENTAS;Uid=root;Pwd=root;";
            conexion = new MySqlConnection(cadenaConexion);
        }

        /// <summary>
        /// Abre la conexión a la base de datos
        /// </summary>
        public bool AbrirConexion()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                    return true;
                }
                return false;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al abrir conexión: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Cierra la conexión a la base de datos
        /// </summary>
        public bool CerrarConexion()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                    return true;
                }
                return false;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al cerrar conexión: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Obtiene el objeto de conexión actual
        /// </summary>
        public MySqlConnection ObtenerConexion()
        {
            return conexion;
        }

        /// <summary>
        /// Ejecuta una consulta SQL (SELECT) y retorna un DataReader
        /// </summary>
        public MySqlDataReader EjecutarConsulta(string consulta)
        {
            try
            {
                if (AbrirConexion())
                {
                    MySqlCommand comando = new MySqlCommand(consulta, conexion);
                    return comando.ExecuteReader();
                }
                return null;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al ejecutar consulta: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Ejecuta una consulta de acción (INSERT, UPDATE, DELETE)
        /// </summary>
        public bool EjecutarAccion(string consulta)
        {
            try
            {
                if (AbrirConexion())
                {
                    MySqlCommand comando = new MySqlCommand(consulta, conexion);
                    int filasAfectadas = comando.ExecuteNonQuery();
                    CerrarConexion();
                    return filasAfectadas > 0;
                }
                return false;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al ejecutar acción: " + ex.Message);
                return false;
            }
        }
    }
}
