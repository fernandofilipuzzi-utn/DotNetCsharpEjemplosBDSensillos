using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Collections;
using ModelsLibClass.Models;
using DaoImplSqlServer.Utils;
using ModelsLibClass.Utils;

namespace EjemploABM
{
    public partial class FormPrincipal : Form
    {
        //IGestionEnviosDao gestionEnvios = new GestionEnviosSQLServerImplDao();
        IGestionEnviosDao gestionEnvios = DatabaseProviderDao.GetInstancia();

        FormControlLotes formControlLotes = new FormControlLotes();
        FormEdicionLote formEdicionLote = null;

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }

        #region control de lotes

        private void controlDeLotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formControlLotes = new FormControlLotes();
            formControlLotes.btnAddLote.Click += this.altaDeProductosToolStripMenuItem_Click;
            formControlLotes.btnEdLote.Click += this.formControlLotes_editarLote_Click;
            formControlLotes.btnRmvLote.Click += this.formControlLotes_eliminarTodosLotes_Click;
            formControlLotes.btnPrtLote.Click += this.imprimir_Click;
            System.Drawing.Printing.PrintEventHandler beginPrint = new System.Drawing.Printing.PrintEventHandler(printDocument_BeginPrint);
            printDocument1.BeginPrint += beginPrint;
            System.Drawing.Printing.PrintPageEventHandler printPage = new System.Drawing.Printing.PrintPageEventHandler(printDocument_PrintPage);
            printDocument1.PrintPage += printPage;

            try
            {
                formControlLotes.ActualizarVista(gestionEnvios.ListarLotes());
                DialogResult resultFormControlLotes = formControlLotes.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}+{ex.StackTrace.ToString()}", "Error");
            }
            finally
            {
                //liberando eventos y memoria
                formControlLotes.btnAddLote.Click -= this.altaDeProductosToolStripMenuItem_Click;
                formControlLotes.btnEdLote.Click -= this.formControlLotes_editarLote_Click;
                formControlLotes.btnRmvLote.Click -= this.formControlLotes_eliminarTodosLotes_Click;
                formControlLotes.btnPrtLote.Click -= this.imprimir_Click;
                printDocument1.BeginPrint -= beginPrint;
                printDocument1.PrintPage -= printPage;
                formControlLotes.Dispose();
            }
        }
        #endregion

        #region edicion de lotes
        private void altaDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formControlLotes.Selected = null;
            formControlLotes_editarLote_Click(sender, e);
        }

        private void formControlLotes_editarLote_Click(object sender, EventArgs e)
        {
            if (formControlLotes != null)
            {
                //preparo el formulario de edidicion de lotes
                formEdicionLote = new FormEdicionLote();
                formEdicionLote.button2.Click += formControlLotes_actualizarLote_Click;
                formEdicionLote.button3.Click += formControlLotes_agregarProductos_Click;

                try
                {
                    formEdicionLote.ActualizarComboBoxProductos(gestionEnvios.ListarProductos());

                    //busco el lote a editar.
                    Lote lote = null;
                    if (formControlLotes.Selected != null)
                        lote = gestionEnvios.BuscarLotePorId(formControlLotes.Selected.ID);

                    //muestro el formulario con los datos de este loteno
                    if (lote != null)
                    {
                        formEdicionLote.IdLote = lote.ID;
                        formEdicionLote.textBox1.Text = lote.Numero.ToString();
                        formEdicionLote.dateTimePicker1.Value = lote.FechaProduccion;
                        formEdicionLote.listBox1.Items.Clear();
                        foreach (Producto p in lote.Productos)
                        {
                            formEdicionLote.listBox1.Items.Add(p.Nombre);
                        }
                        formEdicionLote.button2.Text = "Modificar";
                    }

                    //llamo al dialogo de lotes
                    formEdicionLote.ShowDialog();

                    if (formControlLotes != null)
                        formControlLotes.ActualizarVista(gestionEnvios.ListarLotes());
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"{ex.Message}+{ex.StackTrace.ToString()}", "Error");
                }
                finally
                {

                    formEdicionLote.button2.Click -= formControlLotes_actualizarLote_Click;
                    formEdicionLote.button3.Click -= formControlLotes_agregarProductos_Click;
                    formEdicionLote.Dispose();
                }
            }
        }

        private void formControlLotes_actualizarLote_Click(object sender, EventArgs e)
        {
            int idLote = formEdicionLote.IdLote;
            int nroLote = Convert.ToInt32(formEdicionLote.textBox1.Text);
            DateTime date = formEdicionLote.dateTimePicker1.Value;
            Lote lote = new Lote(idLote, nroLote, date);

            foreach (string nombreProducto in formEdicionLote.listBox1.Items)
            {
                //buscar el producto con ese nombre
                foreach (Producto producto in gestionEnvios.ListarProductos())
                {
                    if (producto.Nombre == nombreProducto)
                    {
                        lote.Productos.Add(producto);
                        break;
                    }
                }
            }
            gestionEnvios.AgregarNuevoLote(lote);
        }

        private void formControlLotes_eliminarTodosLotes_Click(object sender, EventArgs e)
        {
            gestionEnvios.BorrarTodosLosLotes();
            if (formControlLotes != null)
                formControlLotes.ActualizarVista(gestionEnvios.ListarLotes());
        }

        #endregion

        #region edición de productos.

        private void formControlLotes_agregarProductos_Click(object sender, EventArgs e)
        {
            FormEdicionProducto formProducto = new FormEdicionProducto();
            
            if (formProducto.ShowDialog() == DialogResult.OK)
            {
                string nombreProductoNuevo = formProducto.textBox1.Text;
                Producto productoNuevo = new Producto(0, nombreProductoNuevo);
                productoNuevo.Imagen = formProducto.pictureBox1.Image;

                //verificado que no esté
                bool encontrado = false;
                foreach (Producto producto in gestionEnvios.ListarProductos())
                {
                    if (producto.Nombre == nombreProductoNuevo)
                    {
                        encontrado = true;
                        break;
                    }
                }

                //agrego el producto si no lo encontro
                if (encontrado == false)
                {
                    gestionEnvios.AgregarNuevoProducto(productoNuevo);
                }
            }

            //actualiza el combo de productos en la ventana de edicion de lotes
            if (formEdicionLote != null)
                formEdicionLote.ActualizarComboBoxProductos(gestionEnvios.ListarProductos());
        }


        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region impresión de lotes

        private void imprimir_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                printDocument1.Print();
            }
        }

        int linea = 0;
        int lotes = 0;
        int lineasAImprimir = 0;
        List<Lote> lotesAImprimir = new List<Lote>();
        private void printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            linea = 0;
            lotesAImprimir = gestionEnvios.ListarLotes();
            lotes = 0;
            lineasAImprimir=lotesAImprimir.Count;
            foreach (Lote l in lotesAImprimir)
            {
                lineasAImprimir += l.Productos.Count;
            }
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float alto = e.PageBounds.Height;
            float ancho = e.PageBounds.Width;

            Graphics graphics = e.Graphics;


            int altoLinea = 70;


            Brush brushString = new SolidBrush(Color.Black);
            Pen pen = new Pen(brushString);
            Font font = new Font("Verdana", 10);

            int lineaEnPagina = 0; //linea en la página

            //cabecera
            float y = (lineaEnPagina * altoLinea + 2) + 50;

            //numero
            float xNumero = 50;
            graphics.DrawString("Número de Lote", font, brushString, new PointF(xNumero, y));

            //fecha
            float xFecha = xNumero + 150;
            graphics.DrawString("Fecha de Envío", font, brushString, new PointF(xFecha, y));

            graphics.DrawLine(pen, new Point((int)xNumero, (int)y + altoLinea), new Point((int)(ancho - 2 * xNumero), (int)y + altoLinea));



            while (lotes < lotesAImprimir.Count && lineaEnPagina < 10)
            {
                Lote lote = lotesAImprimir[lotes];

                y = ((lineaEnPagina + 1) * altoLinea + 2) + 50;

                //numero
                xNumero = 50;
                int numero = lote.Numero;
                string numeroFormateado = String.Format("{0,6}", numero);
                graphics.DrawString(numeroFormateado, font, brushString, new PointF(xNumero, y));

                //fecha
                xFecha = xNumero + 150;
                DateTime fecha = lote.FechaProduccion;
                //string fechaFormateada = String.Format("{0,9:d}", fecha);
                string fechaFormateada = String.Format("{0:dd/MM/yy}", fecha);
                graphics.DrawString(fechaFormateada, font, brushString, new PointF(xFecha, y));

                lineaEnPagina++;
                linea++;
                lotes++;

                foreach (Producto producto in lote.Productos)
                {
                    y = ((lineaEnPagina + 1) * altoLinea + 2) + 50;

                    //numero
                    int xIdProd = 50+50;
                    int idProd = producto.ID;
                    string idFormateado = String.Format("{0,6}", idProd);
                    graphics.DrawString(idFormateado, font, brushString, new PointF(xIdProd, y + 30));

                    //nombre
                    int xNombreProd = xIdProd + 150;
                    string nombreProd = producto.Nombre;
                    graphics.DrawString(nombreProd, font, brushString, new PointF(xNombreProd, y+30));

                    //imagen
                    int xImagenProd = xNombreProd + 150;
                    if (producto.Imagen != null)
                    {
                        Bitmap imagenProd = new Bitmap(producto.Imagen, 50, 50);
                        graphics.DrawImage(imagenProd, new PointF(xImagenProd, y));
                    }
                    else
                    {
                        graphics.DrawRectangle(pen, xImagenProd, y, 50,50);
                    }

                    lineaEnPagina++;
                    linea++;
                }

                graphics.DrawLine(pen,
                                    new Point((int)xNumero,
                                    (int)y + altoLinea),
                                    new Point((int)(ancho - 2 * xNumero),
                                    (int)y + altoLinea));
            }

            if (linea < lineasAImprimir)
                e.HasMorePages = true;  
        }

        #endregion
    }
}
