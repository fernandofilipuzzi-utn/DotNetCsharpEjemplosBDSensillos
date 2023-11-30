using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelsLibClass.Models
{
    public interface IGestionEnviosDao
    {
        void AgregarNuevoProducto(Producto nuevoProducto);
        List<Producto> ListarProductos();
        List<Producto> ListarProductoPorNombre(string nombreProducto);

        void AgregarNuevoLote(Lote nuevoLote);
        void BorrarTodosLosLotes();
        List<Lote> ListarLotes();
        Lote BuscarLotePorId(int ID);
        Lote ListarLotePorNumero(int numeroLote);
        
    }
}
