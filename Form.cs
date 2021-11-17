using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Capa_Entidad;
using Capa_Negocio;

//Tarea 5 de programación 3 por Daniel Pujols

namespace Tarea5_CRUD_DanielPujols
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        SqlConnection Con = new SqlConnection(@"Server=DESKTOP-QE63DOI; Database=Tarea5; Integrated Security= true");
        ClassEntidad objent = new ClassEntidad();
        ClassNegocio objneg = new ClassNegocio();

        public Form1()
        {
            InitializeComponent();
        }

        void mantenimiento(String accion)
        {
            try
            {
                Con.Open();
                objent.Codigo = txtCodigo.Text;
                objent.Nombre = txtNombre.Text;
                objent.Edad = Convert.ToInt32(txtEdad.Text);
                objent.Telefono = txtTelefono.Text;
                objent.Accion = accion;
                string men = objneg.N_mantenimiento_clientes(objent);
                MessageBox.Show(men, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Con.Close();
            }
            catch(OverflowException ex)
            {
                MessageBox.Show("Problemas"+ex);
            }
            
        }

        void limpiar()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtEdad.Text = "";
            txtTelefono.Text = "";
            txtBuscar.Text = "";
            dgvClientes.DataSource = objneg.N_listar_clientes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvClientes.DataSource = objneg.N_listar_clientes();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                if (MessageBox.Show("¿Deseas Registrar a : " + txtNombre.Text + "?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    mantenimiento("1");
                    limpiar();
                }
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                if (MessageBox.Show("¿Deseas Modificar a : " + txtNombre.Text + "?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    mantenimiento("2");
                    limpiar();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                if (MessageBox.Show("¿Deseas Eliminar a : " + txtNombre.Text + "?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    mantenimiento("3");
                    limpiar();
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                objent.Nombre = txtBuscar.Text;
                DataTable dt = new DataTable();
                dt = objneg.N_buscar_cliente(objent);
                dgvClientes.DataSource = dt;
            }
            else
            {
                dgvClientes.DataSource = objneg.N_listar_clientes();
            }
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = dgvClientes.CurrentCell.RowIndex;
            txtCodigo.Text = dgvClientes[0,fila].Value.ToString();
            txtNombre.Text = dgvClientes[1, fila].Value.ToString();
            txtEdad.Text = dgvClientes[2, fila].Value.ToString();
            txtTelefono.Text = dgvClientes[3, fila].Value.ToString();
        }
    }
}
