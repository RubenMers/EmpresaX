using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Venta
    {
        public static ML.Result AddEF(ML.Venta venta, List<Object> Objects)
        {
            ML.Result result = new ML.Result();
            ML.DetalleVenta detalle = new ML.DetalleVenta();
            detalle.Venta = new ML.Venta();
            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {
                    //var query = context.VentaAddSP(venta.Total, venta.Cliente.IdCliente);


                    var IdResult = new ObjectParameter("IdVenta", typeof(int));
                    int ventas = context.VentaAdd(IdResult, venta.Total, venta.Cliente.IdCliente);
                    venta.IdVenta = (int)IdResult.Value;

                    int IdUsuario = Convert.ToInt32(venta.Cliente.IdCliente);
                    double Total = Convert.ToDouble(detalle.Cantidad);
                    //int Ventas = 1;

                    //var IdResult = new ObjectParameter("IdVenta", typeof(int));
                    //int IdVenta = Convert.ToInt32(context.VentaAdd(IdResult,venta.Total, IdUsuario));

                    Console.WriteLine("El IdVenta es: " + venta.IdVenta);

                    foreach (ML.SucursalProducto productoItem in Objects)
                    {

                        BL.DetalleVenta.Add(venta.IdVenta, productoItem.IdSucursalProducto, productoItem.DetalleVenta.Cantidad);
                    }

                    if (ventas >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
                    }

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result AddEFSPP(ML.Venta pedido, List<object> Objetcs)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {
                    int IdUsuario = Convert.ToInt32(pedido.Cliente.IdCliente);
                    decimal Total = Convert.ToDecimal(pedido.Total);

                    var IdResult = new ObjectParameter("IdVenta", typeof(int));
                    int ventas = context.VentaAdd(IdResult, pedido.Total, pedido.Cliente.IdCliente);
                    pedido.IdVenta = (int)IdResult.Value;


                    int IdPedido = Convert.ToInt32(context.VentaAdd(IdResult, Total, IdUsuario));

                    foreach (ML.SucursalProducto productoItem in Objetcs)
                    {

                        BL.DetalleVenta.Add(IdPedido, productoItem.Producto.IdProducto, productoItem.DetalleVenta.Cantidad);
                    }

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
