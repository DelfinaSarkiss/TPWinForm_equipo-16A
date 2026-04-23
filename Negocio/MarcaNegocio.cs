using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Marca> lista = new List<Marca>();

            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM MARCAS ORDER BY Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca marca = new Marca
                    {
                        Id = datos.Lector.GetInt32(0),
                        Descripcion = datos.Lector.GetString(1)
                    };
                    lista.Add(marca);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO MARCAS (Descripcion) VALUES (@Descripcion)");
                datos.setearParametro("@Descripcion", marca.Descripcion);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE MARCAS SET Descripcion = @Descripcion WHERE Id = @Id");
                datos.setearParametro("@Id", marca.Id);
                datos.setearParametro("@Descripcion", marca.Descripcion);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM ARTICULOS WHERE IdMarca = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read() && datos.Lector.GetInt32(0) > 0)
                {
                    datos.cerrarConexion();
                    throw new Exception("No se puede eliminar la marca porque hay artículos asociados.");
                }
                datos.cerrarConexion();

                datos.setearConsulta("DELETE FROM MARCAS WHERE Id = @Id");
                datos.limpiarParametros();
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}