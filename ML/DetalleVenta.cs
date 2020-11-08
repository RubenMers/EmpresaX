﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }

        public ML.Producto Producto { get; set; }

        public ML.Venta Venta { get; set; }

        public ML.SucursalProducto SucursalProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal Total { get; set; }

        public List<object> DetalleVentas { get; set; }
    }
}
