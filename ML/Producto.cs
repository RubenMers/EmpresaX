using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }

        public string Nombre { get; set; }

        public int CodigoDeBarras { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public byte[] LogoTipo { get; set; }

        public List<Object> Productos { get; set; }
    }
}
