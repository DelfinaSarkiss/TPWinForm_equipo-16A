using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdMarca { get; set; }
        public string NombreMarca { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }

        public decimal Precio { get; set; }
        public List<Imagen> Imagenes { get; set; }

        public Articulo()
        {
            Imagenes = new List<Imagen>();
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
