using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyectoInventario.backEnd.conexionBd;

namespace proyectoInventario.backEnd.Servicios
{
 internal class srvEmpleado
    {
 private clsConsultas consultas;

     public srvEmpleado()
        {
         consultas = new clsConsultas();
    }

   /// <summary>
        /// Registra un nuevo empleado con su usuario asociado
        /// </summary>
    /// <param name="nombre">Nombre del empleado</param>
   /// <param name="apellidoPaterno">Apellido paterno</param>
        /// <param name="apellidoMaterno">Apellido materno (opcional)</param>
        /// <param name="rfc">RFC (opcional)</param>
        /// <param name="nombreUsuario">Nombre de usuario para login</param>
   /// <param name="contrasena">Contraseña del usuario</param>
        /// <param name="idTipoUsuario">ID del tipo de usuario (1=Administrador, 2=Vendedor)</param>
        /// <returns>ID del empleado creado</returns>
        public long RegistrarEmpleadoConUsuario(string nombre, string apellidoPaterno, string apellidoMaterno, 
            string rfc, string nombreUsuario, string contrasena, int idTipoUsuario)
     {
      try
        {
                // Validaciones básicas
        if (string.IsNullOrWhiteSpace(nombre))
           throw new Exception("El nombre del empleado es requerido");

      if (string.IsNullOrWhiteSpace(apellidoPaterno))
         throw new Exception("El apellido paterno es requerido");

           if (string.IsNullOrWhiteSpace(nombreUsuario))
            throw new Exception("El nombre de usuario es requerido");

  if (string.IsNullOrWhiteSpace(contrasena))
                    throw new Exception("La contraseña es requerida");

    // 1. Primero crear el usuario
    long idUsuario = consultas.InsertarUsuario(nombreUsuario, contrasena, idTipoUsuario);

     if (idUsuario <= 0)
        throw new Exception("No se pudo crear el usuario");

     // 2. Luego crear el empleado vinculado al usuario
       long idEmpleado = consultas.InsertarEmpleado(
        nombre, 
          apellidoPaterno, 
     string.IsNullOrWhiteSpace(apellidoMaterno) ? null : apellidoMaterno,
          string.IsNullOrWhiteSpace(rfc) ? null : rfc,
          (int)idUsuario
                );

             if (idEmpleado <= 0)
          throw new Exception("No se pudo crear el empleado");

          return idEmpleado;
            }
            catch (Exception ex)
   {
    Console.WriteLine("Error al registrar empleado con usuario: " + ex.Message);
       throw new Exception("Error al registrar el empleado: " + ex.Message, ex);
   }
        }

    /// <summary>
 /// Obtiene todos los tipos de usuario disponibles
        /// </summary>
     public DataTable ObtenerTiposUsuario()
        {
try
    {
                return consultas.ObtenerTiposUsuario();
            }
 catch (Exception ex)
       {
                Console.WriteLine("Error al obtener tipos de usuario: " + ex.Message);
    throw new Exception("Error al obtener tipos de usuario: " + ex.Message, ex);
     }
        }

        /// <summary>
        /// Lista todos los empleados registrados
    /// </summary>
      public DataTable ListarEmpleados()
        {
        try
         {
       return consultas.ListarEmpleados();
    }
      catch (Exception ex)
            {
    Console.WriteLine("Error al listar empleados: " + ex.Message);
         throw new Exception("Error al listar empleados: " + ex.Message, ex);
            }
        }

   /// <summary>
      /// Actualiza los datos de un empleado
   /// </summary>
        public bool ActualizarEmpleado(int idEmpleado, string nombre, string apellidoPaterno, 
      string apellidoMaterno, string rfc, int? idUsuario)
        {
        try
            {
             if (string.IsNullOrWhiteSpace(nombre))
         throw new Exception("El nombre del empleado es requerido");

     if (string.IsNullOrWhiteSpace(apellidoPaterno))
    throw new Exception("El apellido paterno es requerido");

         int resultado = consultas.ActualizarEmpleado(
        idEmpleado,
     nombre,
     apellidoPaterno,
        string.IsNullOrWhiteSpace(apellidoMaterno) ? null : apellidoMaterno,
                  string.IsNullOrWhiteSpace(rfc) ? null : rfc,
     idUsuario
           );

      return resultado > 0;
    }
            catch (Exception ex)
  {
       Console.WriteLine("Error al actualizar empleado: " + ex.Message);
           throw new Exception("Error al actualizar empleado: " + ex.Message, ex);
}
        }

        /// <summary>
        /// Elimina un empleado
        /// </summary>
        public bool EliminarEmpleado(int idEmpleado)
        {
      try
            {
    int resultado = consultas.EliminarEmpleado(idEmpleado);
       return resultado > 0;
  }
            catch (Exception ex)
         {
      Console.WriteLine("Error al eliminar empleado: " + ex.Message);
        throw new Exception("Error al eliminar empleado: " + ex.Message, ex);
            }
     }
    }
}
