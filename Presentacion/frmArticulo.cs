using Dominio;
using Negocio;
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
        private ArticuloNegocio articuloNegocio;
        private MarcaNegocio marcaNegocio;
        private CategoriaNegocio categoriaNegocio;

        public frmArticulo()
        {
            InitializeComponent();
            articulo = new Articulo();
            articuloNegocio = new ArticuloNegocio();
            marcaNegocio = new MarcaNegocio();
            categoriaNegocio = new CategoriaNegocio();
        }

        public frmArticulo(Articulo articuloExistente)
        {
            InitializeComponent();
            articulo = articuloExistente;
            articuloNegocio = new ArticuloNegocio();
            marcaNegocio = new MarcaNegocio();
            categoriaNegocio = new CategoriaNegocio();
        }

        private void frmArticulo_Load(object sender, EventArgs e)
        {
            try
            {
                List<Marca> marcas = marcaNegocio.Listar();
                cboMarca.DataSource = marcas;
                cboMarca.DisplayMember = "Descripcion";
                cboMarca.ValueMember = "Id";

                List<Categoria> categorias = categoriaNegocio.Listar();
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
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("El código es obligatorio.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El nombre es obligatorio.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    MessageBox.Show("La descripción es obligatoria.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPrecio.Text))
                {
                    MessageBox.Show("El precio es obligatorio.");
                    return;
                }
                decimal precio;
                if (!decimal.TryParse(txtPrecio.Text, out precio))
                {
                    MessageBox.Show("El precio debe ser un número válido.");
                    return;
                }
                if (precio <= 0)
                {
                    MessageBox.Show("El precio debe ser mayor a 0.");
                    return;
                }
                if (cboMarca.SelectedValue == null || cboCategoria.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar marca y categoría.");
                    return;
                }

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = precio;
                articulo.IdMarca = (int)cboMarca.SelectedValue;
                articulo.IdCategoria = (int)cboCategoria.SelectedValue;

                if (articulo.Id == 0)
                {
                    articuloNegocio.Agregar(articulo);
                    MessageBox.Show("Artículo agregado correctamente.");
                }
                else
                {
                    articuloNegocio.Modificar(articulo);
                    MessageBox.Show("Artículo modificado correctamente.");
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
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
