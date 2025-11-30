using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoInventario.backEnd.POCOS
{
    /// <summary>
    /// Clase POCO para la tabla Proveedores
    /// </summary>
    internal class Proveedor
    {
        public int IdProveedores { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        public Proveedor(int idProveedores, string nombre, string telefono, string email, string direccion = null)
        {
            IdProveedores = idProveedores;
            Nombre = nombre;
            Telefono = telefono;
            Email = email;
            Direccion = direccion;
        }
    }
}
