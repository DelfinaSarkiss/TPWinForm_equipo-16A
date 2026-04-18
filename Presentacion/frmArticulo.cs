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
    public partial class frmArticulo : Form
    {
        private Articulo articulo;
        public frmArticulo()
        {
            InitializeComponent();
            articulo = new Articulo();
        }

        public frmArticulo(Articulo articuloExistente)
        {
            InitializeComponent();
            articulo = articuloExistente;
        }

        private void frmArticulo_Load(object sender, EventArgs e)
        {
            // Datos de prueba para Marcas
            List<Marca> marcas = new List<Marca>();
            marcas.Add(new Marca() { Id = 1, Descripcion = "Samsung" });
            marcas.Add(new Marca() { Id = 2, Descripcion = "Apple" });
            marcas.Add(new Marca() { Id = 3, Descripcion = "Sony" });
            marcas.Add(new Marca() { Id = 4, Descripcion = "Huawei" });
            marcas.Add(new Marca() { Id = 5, Descripcion = "Motorola" });

            cboMarca.DataSource = marcas;
            cboMarca.DisplayMember = "Descripcion";
            cboMarca.ValueMember = "Id";

            // Datos de prueba para Categorías
            List<Categoria> categorias = new List<Categoria>();
            categorias.Add(new Categoria() { Id = 1, Descripcion = "Celulares" });
            categorias.Add(new Categoria() { Id = 2, Descripcion = "Televisores" });
            categorias.Add(new Categoria() { Id = 3, Descripcion = "Media" });
            categorias.Add(new Categoria() { Id = 4, Descripcion = "Audio" });

            cboCategoria.DataSource = categorias;
            cboCategoria.DisplayMember = "Descripcion";
            cboCategoria.ValueMember = "Id";

            if (articulo.Id != 0)
            {
                //modificamos
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtPrecio.Text = articulo.Precio.ToString();

                foreach(Imagen img in articulo.Imagenes)
                {
                    lstImagenes.Items.Add(img.ImagenUrl);
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            articulo.Codigo = txtCodigo.Text;
            articulo.Nombre = txtNombre.Text;
            articulo.Descripcion = txtDescripcion.Text;
            articulo.Precio = Convert.ToDecimal(txtPrecio.Text);

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            if(txtUrlImagen.Text.Length > 0)
            {
                Imagen img = new Imagen();
                img.ImagenUrl = txtUrlImagen.Text;
                articulo.Imagenes.Add(img);
                lstImagenes.Items.Add(img.ImagenUrl);
                txtUrlImagen.Text = "";
            }
        }

        private void btnEliminarImagen_Click(object sender, EventArgs e)
        {
            if(lstImagenes.SelectedIndex != -1)
            {
                articulo.Imagenes.RemoveAt(lstImagenes.SelectedIndex);
                lstImagenes.Items.RemoveAt(lstImagenes.SelectedIndex);
                pbxImagen.Image = null;
            }
        }

        private void lstImagenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstImagenes.SelectedIndex != -1)
            {
                try
                {
                    string url = lstImagenes.SelectedItem.ToString();
                    pbxImagen.Load(url);
                }
                catch(Exception ex) 
                {
                    MessageBox.Show("No se pudo cargar la imagen: " + ex.Message);
                }
                
            }

        }
    }
}
