using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC3_LLATAS_PARIGUANA_ANDY_MARCELO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InicializarDataGridView();
        }
        private void InicializarDataGridView()
        {
            // Tenia un error y me ayude con GPT, Mecorrigio asi Profesor con esta funcion y la inicia con los componentes
            // Esta creando en el data grid las columnas al iniciar el form
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("Genero", "Género");
            dataGridView1.Columns.Add("Nombres", "Nombres");
            dataGridView1.Columns.Add("Precio", "Precio");
            dataGridView1.Columns.Add("Idioma", "Idioma");
            dataGridView1.Columns.Add("Anio", "Año");
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtID.Text, out int id))
            {
                var videojuego = Conexion.BuscarPorID(id);

                if (videojuego != null)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(videojuego);
                }
                else
                {
                    MessageBox.Show("No se encontró el registro.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un ID válido.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var videojuegos = Conexion.Mostrar();
            dataGridView1.Rows.Clear();

            foreach (var videojuego in videojuegos)
            {
                dataGridView1.Rows.Add(videojuego);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtGenero.Text) ||
                    string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    !decimal.TryParse(txtPrecio.Text, out decimal precio) ||
                    string.IsNullOrWhiteSpace(txtIdioma.Text) ||
                    !int.TryParse(txtAnio.Text, out int año))
                {
                    MessageBox.Show("Por favor, complete todos los campos con datos válidos.");
                    return;
                }

                Conexion.Registrar(txtGenero.Text, txtNombre.Text, precio, txtIdioma.Text, año);

                MessageBox.Show("Registro agregado exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
        }
    }
}
