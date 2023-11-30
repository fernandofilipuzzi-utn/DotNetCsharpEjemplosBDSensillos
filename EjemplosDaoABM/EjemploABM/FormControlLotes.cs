using ModelsLibClass.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;


namespace EjemploABM
{
    public partial class FormControlLotes : Form
    {
        public FormControlLotes()
        {
            InitializeComponent();
            ActualizarDataGridView(new List<Lote>());
        }

        //métodos utilitarios

        public void ActualizarVista(List<Lote> lotes)
        {
            ActualizarDataGridView(lotes);
            Selected = null;
        }
                
        protected void ActualizarDataGridView(List<Lote> lotes)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.Columns.Clear();

            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = 3;

            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Número";
            dataGridView1.Columns[2].HeaderText = "Fecha de Envío";
            dataGridView1.Columns[2].ValueType = typeof(DateTime);

            //selección por renglón
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView1.RowCount = lotes.Count + 1;
            int anchoCelda = dataGridView1.Size.Width / 3;

            dataGridView1.Columns[0].Width = anchoCelda;
            dataGridView1.Columns[1].Width = anchoCelda;
            dataGridView1.Columns[2].Width = anchoCelda;

            for (int renglon = 0; renglon < lotes.Count; renglon++)
            {
                dataGridView1[0, renglon].Value =  lotes[renglon].ID;
                dataGridView1[1, renglon].Value =  lotes[renglon].Numero;
                dataGridView1[2, renglon].Value =  lotes[renglon].FechaProduccion;                                            
            }
        }

        private Lote selected = null;
        public Lote Selected
        {
            get {
                return selected;
            }
            set
            {
                if (value == null)
                {
                    dataGridView1.ClearSelection();
                    btnEdLote.Enabled = false;
                }
                else 
                {
                    btnEdLote.Enabled = true;
                }
                selected=value;
            }
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int row = e.RowIndex;

            if (row != -1)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[row].Cells[0].Value);
                int numero = Convert.ToInt32(dataGridView1.Rows[row].Cells[1].Value);
                Selected = new Lote(id, numero, DateTime.Now);
            }
            else
                Selected = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
