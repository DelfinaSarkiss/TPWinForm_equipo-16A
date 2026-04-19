using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        // Propiedad pública para acceder al lector de datos
        public SqlDataReader Lector
        {
            get { return lector; }
        }


        // Constructor que lee la cadena de conexión desde App.config
        public AccesoDatos()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CatalogoDB"]?.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("No se encontró la cadena de conexión 'CatalogoDB' en App.config");
            }
            conexion = new SqlConnection(connectionString);
            comando = new SqlCommand();
        }

        // Método que establece la consulta SQL a ejecutar
        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        // Método que ejecuta la consulta SQL y devuelve un SqlDataReader con los resultados
        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Método que ejecuta una acción SQL (INSERT, UPDATE, DELETE) sin devolver resultados
        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Método que agrega un parámetro a la consulta SQL
        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        // Método que cierra la conexión a la base de datos y el lector de datos
        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }
    }
}
