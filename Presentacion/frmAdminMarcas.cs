using Dominio;
using Negocio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace TPWinForm_equipo_16A
{
    public partial class frmAdminMarcas : Form
    {
        private MarcaNegocio marcaNegocio;
        private int? idSeleccionado;

        public frmAdminMarcas()
        {
            InitializeComponent();
            marcaNegocio = new MarcaNegocio();
        }

        private void frmAdminMarcas_Load(object sender, EventArgs e)
        {
            Text = "Administrar Marcas";
            ModoNuevo();
            CargarLista();
        }

        private void CargarLista()
        {
            try
            {
                dgvMarcas.DataSource = marcaNegocio.Listar().ToList();
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

        private void dgvMarcas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMarcas.SelectedRows.Count > 0)
            {
                idSeleccionado = ((Marca)dgvMarcas.SelectedRows[0].DataBoundItem).Id;
                txtDescripcion.Text = ((Marca)dgvMarcas.SelectedRows[0].DataBoundItem).Descripcion;
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

            bool existe = marcaNegocio.Listar().Any(m => m.Descripcion.ToLower() == descripcion.ToLower() && m.Id != idSeleccionado);
            if (existe)
            {
                MessageBox.Show("Ya existe una marca con ese nombre.");
                return;
            }

            try
            {
                if (idSeleccionado.HasValue)
                {
                    marcaNegocio.Modificar(new Marca { Id = idSeleccionado.Value, Descripcion = descripcion });
                    MessageBox.Show("Marca modificada.");
                }
                else
                {
                    marcaNegocio.Agregar(new Marca { Descripcion = descripcion });
                    MessageBox.Show("Marca agregada.");
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
                DialogResult result = MessageBox.Show($"¿Eliminar marca: {nombre}?", "Confirmar", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    marcaNegocio.Eliminar(idSeleccionado.Value);
                    MessageBox.Show("Marca eliminada.");
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