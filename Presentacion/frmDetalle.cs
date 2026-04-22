using Dominio;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace TPWinForm_equipo_16A
{
    public partial class frmDetalle : Form
    {
        private Articulo articulo;
        private int indiceImagen = 0;

        public frmDetalle(Articulo articuloAMostrar)
        {
            InitializeComponent();
            articulo = articuloAMostrar;
        }

        private void frmDetalle_Load(object sender, EventArgs e)
        {
            var culture = new CultureInfo("es-AR");
            Application.CurrentCulture = culture;

            lblCodigo.Text = articulo.Codigo;
            lblNombre.Text = articulo.Nombre;
            lblDescripcion.Text = articulo.Descripcion;
            lblMarca.Text = articulo.NombreMarca;
            lblCategoria.Text = articulo.NombreCategoria;
            lblPrecio.Text = articulo.Precio.ToString("C2", culture);

            ActualizarBotones();
            if (articulo.Imagenes.Count > 0)
            {
                CargarImagen(0);
            }
        }

        private void CargarImagen(int indice)
        {
            try
            {
                if (articulo.Imagenes.Count > 0)
                {
                    pbxImagen.Load(articulo.Imagenes[indice].ImagenUrl);
                }
            }
            catch
            {
                pbxImagen.Image = null;
            }
            ActualizarBotones();
        }

        private void ActualizarBotones()
        {
            if (articulo.Imagenes.Count == 0)
            {
                lblImagenActual.Text = "Sin imágenes";
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;
            }
            else
            {
                lblImagenActual.Text = $"{indiceImagen + 1}/{articulo.Imagenes.Count}";
                btnAnterior.Enabled = indiceImagen > 0;
                btnSiguiente.Enabled = indiceImagen < articulo.Imagenes.Count - 1;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (indiceImagen > 0)
            {
                indiceImagen--;
                CargarImagen(indiceImagen);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (indiceImagen < articulo.Imagenes.Count - 1)
            {
                indiceImagen++;
                CargarImagen(indiceImagen);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}