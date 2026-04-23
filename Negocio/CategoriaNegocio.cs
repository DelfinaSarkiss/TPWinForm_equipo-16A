using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Categoria> lista = new List<Categoria>();

            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM CATEGORIAS ORDER BY Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria
                    {
                        Id = datos.Lector.GetInt32(0),
                        Descripcion = datos.Lector.GetString(1)
                    };
                    lista.Add(categoria);
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

        public void Agregar(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO CATEGORIAS (Descripcion) VALUES (@Descripcion)");
                datos.setearParametro("@Descripcion", categoria.Descripcion);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE CATEGORIAS SET Descripcion = @Descripcion WHERE Id = @Id");
                datos.setearParametro("@Id", categoria.Id);
                datos.setearParametro("@Descripcion", categoria.Descripcion);
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
                datos.setearConsulta("SELECT COUNT(*) FROM ARTICULOS WHERE IdCategoria = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read() && datos.Lector.GetInt32(0) > 0)
                {
                    datos.cerrarConexion();
                    throw new Exception("No se puede eliminar la categoría porque hay artículos asociados.");
                }
                datos.cerrarConexion();

                datos.setearConsulta("DELETE FROM CATEGORIAS WHERE Id = @Id");
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
