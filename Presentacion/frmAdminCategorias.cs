using Dominio;
using Negocio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace TPWinForm_equipo_16A
{
    public partial class frmAdminCategorias : Form
    {
        private CategoriaNegocio categoriaNegocio;
        private int? idSeleccionado;

        public frmAdminCategorias()
        {
            InitializeComponent();
            categoriaNegocio = new CategoriaNegocio();
        }

        private void frmAdminCategorias_Load(object sender, EventArgs e)
        {
            Text = "Administrar Categorías";
            ModoNuevo();
            CargarLista();
        }

        private void CargarLista()
        {
            try
            {
                dgvCategorias.DataSource = categoriaNegocio.Listar().ToList();
                foreach (DataGridViewColumn col in dgvCategorias.Columns)
                {
                    col.HeaderText = col.HeaderText.ToUpper();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ModoNuevo();
        }

        private void ModoNuevo()
        {
            txtDescripcion.Text = "";
            txtDescripcion.Focus();
            idSeleccionado = null;
            lblEstado.Text = "Modo: Nuevo";
            lblEstado.ForeColor = System.Drawing.Color.Blue;
        }

        private void ModoEdicion()
        {
            txtDescripcion.Focus();
            lblEstado.Text = "Modo: Editando";
            lblEstado.ForeColor = System.Drawing.Color.Green;
        }

        private void dgvCategorias_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count > 0)
            {
                idSeleccionado = ((Categoria)dgvCategorias.SelectedRows[0].DataBoundItem).Id;
                txtDescripcion.Text = ((Categoria)dgvCategorias.SelectedRows[0].DataBoundItem).Descripcion;
                ModoEdicion();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string descripcion = txtDescripcion.Text.Trim();
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                MessageBox.Show("Ingrese una descripción.");
                return;
            }

            bool existe = categoriaNegocio.Listar().Any(c => c.Descripcion.ToLower() == descripcion.ToLower() && c.Id != idSeleccionado);
            if (existe)
            {
                MessageBox.Show("Ya existe una categoría con ese nombre.");
                return;
            }

            try
            {
                if (idSeleccionado.HasValue)
                {
                    categoriaNegocio.Modificar(new Categoria { Id = idSeleccionado.Value, Descripcion = descripcion });
                    MessageBox.Show("Categoría modificada.");
                }
                else
                {
                    categoriaNegocio.Agregar(new Categoria { Descripcion = descripcion });
                    MessageBox.Show("Categoría agregada.");
                }
                CargarLista();
                ModoNuevo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!idSeleccionado.HasValue)
            {
                MessageBox.Show("Seleccione una fila para eliminar.");
                return;
            }

            try
            {
                string nombre = txtDescripcion.Text;
                DialogResult result = MessageBox.Show($"¿Eliminar categoría: {nombre}?", "Confirmar", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    categoriaNegocio.Eliminar(idSeleccionado.Value);
                    MessageBox.Show("Categoría eliminada.");
                    CargarLista();
                    ModoNuevo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }
    }
}