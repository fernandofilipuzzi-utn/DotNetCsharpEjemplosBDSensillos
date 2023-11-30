using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClassLibrary.Models
{
    public class Lote
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}
