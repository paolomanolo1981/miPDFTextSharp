using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppPDF
{
    public class Productos
    {
        public string NombreProducto { get; set; }
        public double? PrecioUnidad{ get; set; }
        public int? UnidadesEnExistencia { get; set; }
    }
}