using Dominio;
using Negocio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace TPWinForm_equipo_16A
{
    public partial class frmAdminEntidades : Form
    {
        private string tipoEntidad;
        private object negocio;
        private int? idSeleccionado;

        public frmAdminEntidades(string tipo)
        {
            InitializeComponent();
            tipoEntidad = tipo;

            if (tipo == "Marcas")
            {
                negocio = new MarcaNegocio();
            }
            else
            {
                negocio = new CategoriaNegocio();
            }
        }

        private void frmAdminEntidades_Load(object sender, EventArgs e)
        {
            Text = $"Administrar {tipoEntidad}";
            ModoNuevo();
            CargarLista();
        }

        private void CargarLista()
        {
            try
            {
                if (tipoEntidad == "Marcas")
                {
                    dgvEntidades.DataSource = ((MarcaNegocio)negocio).Listar().ToList();
                }
                else
                {
                    dgvEntidades.DataSource = ((CategoriaNegocio)negocio).Listar().ToList();
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

        private void dgvEntidades_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEntidades.SelectedRows.Count > 0)
            {
                idSeleccionado = tipoEntidad == "Marcas" ? ((Marca)dgvEntidades.SelectedRows[0].DataBoundItem).Id : ((Categoria)dgvEntidades.SelectedRows[0].DataBoundItem).Id;
                string descripcion = tipoEntidad == "Marcas" ? ((Marca)dgvEntidades.SelectedRows[0].DataBoundItem).Descripcion : ((Categoria)dgvEntidades.SelectedRows[0].DataBoundItem).Descripcion;
                txtDescripcion.Text = descripcion;
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

            try
            {
                if (tipoEntidad == "Marcas")
                {
                    if (idSeleccionado.HasValue)
                    {
                        ((MarcaNegocio)negocio).Modificar(new Marca { Id = idSeleccionado.Value, Descripcion = descripcion });
                    }
                    else
                    {
                        ((MarcaNegocio)negocio).Agregar(new Marca { Descripcion = descripcion });
                    }
                }
                else
                {
                    if (idSeleccionado.HasValue)
                    {
                        ((CategoriaNegocio)negocio).Modificar(new Categoria { Id = idSeleccionado.Value, Descripcion = descripcion });
                    }
                    else
                    {
                        ((CategoriaNegocio)negocio).Agregar(new Categoria { Descripcion = descripcion });
                    }
                }

                MessageBox.Show(idSeleccionado.HasValue ? $"{tipoEntidad} modificada." : $"{tipoEntidad} agregada.");
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
                DialogResult result = MessageBox.Show($"¿Eliminar {tipoEntidad}: {nombre}?", "Confirmar", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (tipoEntidad == "Marcas")
                    {
                        ((MarcaNegocio)negocio).Eliminar(idSeleccionado.Value);
                    }
                    else
                    {
                        ((CategoriaNegocio)negocio).Eliminar(idSeleccionado.Value);
                    }
                    MessageBox.Show($"{tipoEntidad} eliminada.");
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