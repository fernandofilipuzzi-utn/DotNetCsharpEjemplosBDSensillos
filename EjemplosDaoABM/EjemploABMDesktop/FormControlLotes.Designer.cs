namespace EjemploABM
{
    partial class FormControlLotes
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddLote = new System.Windows.Forms.ToolStripButton();
            this.btnEdLote = new System.Windows.Forms.ToolStripButton();
            this.btnRmvLote = new System.Windows.Forms.ToolStripButton();
            this.btnPrtLote = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(473, 468);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(248, 28);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cerrar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 34);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(737, 426);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddLote,
            this.btnEdLote,
            this.btnRmvLote,
            this.btnPrtLote});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(737, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAddLote
            // 
            this.btnAddLote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddLote.Image = global::EjemploABM.Properties.Resources.add;
            this.btnAddLote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddLote.Name = "btnAddLote";
            this.btnAddLote.Size = new System.Drawing.Size(23, 22);
            this.btnAddLote.Text = "toolStripButton1";
            // 
            // btnEdLote
            // 
            this.btnEdLote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEdLote.Image = global::EjemploABM.Properties.Resources.edit;
            this.btnEdLote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdLote.Name = "btnEdLote";
            this.btnEdLote.Size = new System.Drawing.Size(23, 22);
            this.btnEdLote.Text = "toolStripButton4";
            // 
            // btnRmvLote
            // 
            this.btnRmvLote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRmvLote.Image = global::EjemploABM.Properties.Resources.del;
            this.btnRmvLote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRmvLote.Name = "btnRmvLote";
            this.btnRmvLote.Size = new System.Drawing.Size(23, 22);
            this.btnRmvLote.Text = "toolStripButton2";
            // 
            // btnPrtLote
            // 
            this.btnPrtLote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrtLote.Image = global::EjemploABM.Properties.Resources.print;
            this.btnPrtLote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrtLote.Name = "btnPrtLote";
            this.btnPrtLote.Size = new System.Drawing.Size(23, 22);
            this.btnPrtLote.Text = "toolStripButton3";
            // 
            // FormControlLotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 501);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormControlLotes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Control de lotes";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton btnAddLote;
        public System.Windows.Forms.ToolStripButton btnPrtLote;
        public System.Windows.Forms.ToolStripButton btnRmvLote;
        public System.Windows.Forms.ToolStripButton btnEdLote;
    }
}

