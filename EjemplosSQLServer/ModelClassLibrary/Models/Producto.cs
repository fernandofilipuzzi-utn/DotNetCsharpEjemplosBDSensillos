using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClassLibrary.Models
{
    public class Producto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public Image Imagen { get; set; }
    }
}
