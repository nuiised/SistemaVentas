using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
namespace CapaDatos
{
    public sealed class Conexion
    {
        private static Conexion _instancia;
        // LocalDB: en C# la barra se escribe doble (\\)
        private readonly string _cadena =
        "Server=DESKTOP-MNHHEC5\\SQLEXPRESS;Database=BDVentas;Integrated Security=true;TrustServerCertificate=true";
        // Constructor PRIVADO: nadie puede hacer "new Conexion()" desde afuera
    private Conexion() { }
        // Única puerta de entrada
        public static Conexion Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new Conexion();
                return _instancia;
            }
        }
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_cadena);
        }
    }
}