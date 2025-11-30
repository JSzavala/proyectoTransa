using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoInventario.backEnd.POCOS
{
    /// <summary>
    /// Clase POCO para la tabla Categoria
    /// </summary>
    internal class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        public Categoria(int idCategoria, string nombre, string descripcion = null)
        {
            IdCategoria = idCategoria;
            Nombre = nombre;
            Descripcion = descripcion;
        }
  }
}
