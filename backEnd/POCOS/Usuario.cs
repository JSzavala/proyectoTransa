using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoInventario.backEnd.POCOS
{
    /// <summary>
    /// Enumeración para el rol de usuario
    /// </summary>
    public enum RolUsuario
    {
        Dueña,
        Empleado
    }

    /// <summary>
    /// Clase POCO para la tabla Usuario
    /// </summary>
    internal class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string UsuarioNombre { get; set; }
        public string Contrasena { get; set; }
        public RolUsuario Rol { get; set; }
        public bool Activo { get; set; }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        public Usuario(int idUsuario, string nombre, string usuarioNombre, string contrasena, RolUsuario rol, bool activo)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            UsuarioNombre = usuarioNombre;
            Contrasena = contrasena;
            Rol = rol; 
            Activo = activo;
        }
    }
}
