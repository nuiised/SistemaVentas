
using System.Collections.Generic;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class ProductoBL
    {
        public List<Producto> Buscar(string nombre)
        {
            if (nombre == null)
            {
                nombre = "";
            }

            return ProductoDAO.Instancia.Listar(nombre.Trim());
        }

        // Devuelve "" si todo salió bien,
        // o el mensaje de error.
        public string Registrar(Producto p)
        {
            if (string.IsNullOrWhiteSpace(p.Nombre))
            {
                return "Ingrese el nombre del producto.";
            }

            if (string.IsNullOrWhiteSpace(p.Categoria))
            {
                return "Seleccione una categoría.";
            }

            if (p.Precio <= 0)
            {
                return "El precio debe ser mayor a 0.";
            }

            ProductoDAO.Instancia.Insertar(p);

            return "";
        }
    }
}
