using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ModelsLibClass.Models
{
    public class Producto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public Image Imagen { get; set; }

        public Producto(int id, string nombre) 
        {
            ID = id;
            Nombre = nombre;
        }

        public Producto(int id, string nombre, Image imagen)
        {
            ID = id;
            Nombre = nombre;
            Imagen = imagen;
        }
    }
}
