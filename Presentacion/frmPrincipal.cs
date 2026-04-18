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
    public partial class frmPrincipal : Form
    {
        private List<Articulo> listaArticulos;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmArticulo frm = new frmArticulo();
            frm.ShowDialog();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if(dgvArticulos.SelectedRows.Count > 0)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.SelectedRows[0].DataBoundItem;
                frmArticulo frm = new frmArticulo(seleccionado);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un artículo para modificar.");
            }
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if(dgvArticulos.SelectedRows.Count > 0)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.SelectedRows[0].DataBoundItem;
                frmDetalle frm = new frmDetalle(seleccionado);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un artículo para ver su detalle.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(dgvArticulos.SelectedRows.Count > 0)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.SelectedRows[0].DataBoundItem;
                DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar el artículo: " + seleccionado.Nombre + "?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if(resultado == DialogResult.Yes)
                {
                    MessageBox.Show("Artículo eliminado: " + seleccionado.Nombre);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un artículo para eliminar.");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (nudMinimo.Text == "") nudMinimo.Value = 0;
            if (nudMaximo.Text == "") nudMaximo.Value = 0;

            decimal minimo = nudMinimo.Value;
            decimal maximo = nudMaximo.Value;

            if (minimo > maximo && maximo != 0)
            {
                MessageBox.Show("El valor mínimo no puede ser mayor al máximo.");
                return;
            }

            List<Articulo> filtrada = listaArticulos.FindAll(a =>
                (txtBuscar.Text.Length == 0 || a.Nombre.ToLower().Contains(txtBuscar.Text.ToLower())) &&
                (minimo == 0 || a.Precio >= minimo) &&
                (maximo == 0 || a.Precio <= maximo)
            );

            dgvArticulos.DataSource = filtrada;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            listaArticulos = new List<Articulo>();
            Articulo a1 = new Articulo() { Id = 1, Codigo = "S01", Nombre = "Galaxy S10", Descripcion = "Una canoa cara", IdMarca = 1, IdCategoria = 1, NombreMarca = "Samsung", NombreCategoria = "Celulares", Precio = 69999 };
            a1.Imagenes.Add(new Imagen() { ImagenUrl = "https://images.samsung.com/is/image/samsung/co-galaxy-s10-sm-g970-sm-g970fzyjcoo-frontcanaryyellow-thumb-149016542" });
            listaArticulos.Add(a1);
            listaArticulos.Add(new Articulo() { Id = 2, Codigo = "M03", Nombre = "Moto G Play 7ma Gen", Descripcion = "Ya siete de estos?", IdMarca = 1, IdCategoria = 5, Precio = 15699 });
            listaArticulos.Add(new Articulo() { Id = 3, Codigo = "S99", Nombre = "Play 4", Descripcion = "Ya no se cuantas versiones hay", IdMarca = 3, IdCategoria = 3, Precio = 35000 });
            listaArticulos.Add(new Articulo() { Id = 4, Codigo = "S56", Nombre = "Bravia 55", Descripcion = "Alta tele", IdMarca = 3, IdCategoria = 2, Precio = 49500 });
            listaArticulos.Add(new Articulo() { Id = 5, Codigo = "A23", Nombre = "Apple TV", Descripcion = "lindo loro", IdMarca = 2, IdCategoria = 3, Precio = 7850 });

            dgvArticulos.DataSource = listaArticulos;
        }
    }
}
