using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ModelsLibClass.Models
{
    public class Lote
    {
        public int ID { get; set; }
        public int Numero { get; set; }
        public DateTime FechaProduccion { get; set; }
        public List<Producto> Productos { get; set; }

        public Lote()
        {
            Productos = new List<Producto>();
        }

        public Lote(int id, int numero, DateTime fechaProduccion)
        {
            ID = id;
            Numero = numero;
            FechaProduccion = fechaProduccion;
            Productos = new List<Producto>();
        }
    }
}
