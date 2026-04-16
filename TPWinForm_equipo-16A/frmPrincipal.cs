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
            frmArticulo frm = new frmArticulo();
            frm.ShowDialog();
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            // Por ahora creamos un artículo de prueba para testear
            Articulo articuloPrueba = new Articulo();
            articuloPrueba.Codigo = "S01";
            articuloPrueba.Nombre = "Galaxy S10";
            articuloPrueba.Descripcion = "Una canoa cara";
            articuloPrueba.Precio = 69999;

            Imagen img = new Imagen();
            img.ImagenUrl = "https://images.samsung.com/is/image/samsung/co-galaxy-s10-sm-g970-sm-g970fzyjcoo-frontcanaryyellow-thumb-149016542";
            articuloPrueba.Imagenes.Add(img);

            frmDetalle frm = new frmDetalle(articuloPrueba);
            frm.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Eliminar artículo");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Buscar: " + txtBuscar.Text);
        }
    }
}
