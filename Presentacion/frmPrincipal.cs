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
    public partial class frmPrincipal : Form
    {
        private List<Articulo> listaArticulos;
        private ArticuloNegocio negocio;
        public frmPrincipal()
        {
            InitializeComponent();
            negocio = new ArticuloNegocio();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmArticulo frm = new frmArticulo();
            frm.ShowDialog();
            cargarArticulos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if(dgvArticulos.SelectedRows.Count > 0)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.SelectedRows[0].DataBoundItem;
                frmArticulo frm = new frmArticulo(seleccionado);
                frm.ShowDialog();
                cargarArticulos();
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
                    try
                    {
                        negocio.Eliminar(seleccionado.Id);
                        MessageBox.Show("Artículo eliminado: " + seleccionado.Nombre);
                        listaArticulos = negocio.Listar();
                        dgvArticulos.DataSource = listaArticulos;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar: " + ex.Message);
                    }
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
            cargarArticulos();
        }

        private void cargarArticulos()
        {
            try
            {
                listaArticulos = negocio.Listar();
                dgvArticulos.DataSource = listaArticulos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar artículos: " + ex.Message);
            }
        }
    }
}
