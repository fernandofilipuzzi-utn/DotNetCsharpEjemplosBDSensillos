using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ModelsLibClass.Models;

namespace EjemploABM
{
    public partial class FormEdicionLote : Form
    {

        public int IdLote;

        public FormEdicionLote()
        {
            InitializeComponent();

            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //controla si se seleccionó algún producto
            if (comboBox1.SelectedIndex > -1)
            {
                //verifica si fue agregado
                bool agregado = false;
                foreach (string producto in listBox1.Items)
                {
                    if (comboBox1.SelectedItem!=null &&
                        producto == ((string)(comboBox1.SelectedItem)))
                    {
                        agregado = true;
                        break;
                    }
                }

                //lo agrega si no fue agregado
                if (agregado == false)
                    listBox1.Items.Add(comboBox1.SelectedItem);
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = comboBox1.SelectedIndex > -1;
        }

        //codigo utilitario

        public void ActualizarComboBoxProductos(List<Producto> productos)
        {
            this.comboBox1.Items.Clear();
            foreach (Producto producto in productos)
            {
                this.comboBox1.Items.Add(producto.Nombre);
            }
        }

        private void FormEdicionLote_Load(object sender, EventArgs e)
        {

        }

    }
}
