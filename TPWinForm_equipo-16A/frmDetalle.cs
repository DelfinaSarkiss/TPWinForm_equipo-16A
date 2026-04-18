using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWinForm_equipo_16A
{
    public partial class frmDetalle : Form
    {
        private Articulo articulo;
        public frmDetalle(Articulo articuloAMostrar)
        {
            InitializeComponent();
            articulo = articuloAMostrar;
        }

        private void frmDetalle_Load(object sender, EventArgs e)
        {
            lblCodigo.Text = articulo.Codigo;
            lblNombre.Text = articulo.Nombre;
            lblDescripcion.Text = articulo.Descripcion;
            lblMarca.Text = articulo.NombreMarca;
            lblCategoria.Text = articulo.NombreCategoria;
            lblPrecio.Text = articulo.Precio.ToString();

            if(articulo.Imagenes.Count > 0)
            {
                try
                {
                    pbxImagen.Load(articulo.Imagenes[0].ImagenUrl);
                }
                catch(Exception ex) 
                {
                    MessageBox.Show("No se pudo cargar la imagen: " + ex.Message);
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
