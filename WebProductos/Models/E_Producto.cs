using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProductos.Models
{
    public class E_Producto
    {
        //Propiedades simples
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Disponible { get; set; }
        public string Ventas { get; set; }
        public int NumeroVentas { get; set; }
        public string Sucursal { get; set; }
        public string DireccionSucursal { get; set; }
    }
}