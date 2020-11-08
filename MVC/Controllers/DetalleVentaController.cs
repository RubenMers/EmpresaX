using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class DetalleVentaController : Controller
    {
        // GET: DetalleVenta
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            result.Objects = (List<Object>)Session["Carrito"];

            ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
            sucursalProducto.DetalleVenta = new ML.DetalleVenta();
            sucursalProducto.DetalleVenta.Venta = new ML.Venta();
            //ML.Venta venta = new ML.Venta();
            //venta.Cliente = new ML.Cliente();
            //venta.Cliente.IdCliente = 1;
            sucursalProducto.DetalleVenta.Venta.Cliente = new ML.Cliente();
            sucursalProducto.DetalleVenta.Venta.Cliente.IdCliente = 1;
            
            
            double total = 0;

            if (result.Objects == null)
            {
                return View("GetAll", result);
            }
            else
            {
                foreach (ML.SucursalProducto sucursalProductos in result.Objects.ToList())
                {
                    ML.Venta venta = new ML.Venta();
                    total = total + sucursalProductos.DetalleVenta.Cantidad;
                }

                //sucursalProductos.DetalleVenta.Venta.Total = (Int32)total;
                sucursalProducto.DetalleVenta.Venta.Total = (Int32)total;

                var AddVenta = BL.Venta.AddEF(sucursalProducto.DetalleVenta.Venta,result.Objects);
            }
            return View("GetAll", result);

    }
    }
}