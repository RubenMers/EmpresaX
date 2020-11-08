using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DetalleVenta
    {
        public static ML.Result Add(int IdVenta,int IdSucursalProducto, int Cantidad)//aqui
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {
                   
                    //var IdResult = new ObjectParameter("IdDetalleVenta", typeof(int));
                    var alumnos = context.DetalleVentaAdd(IdVenta, Cantidad, IdSucursalProducto);
                    
                    //detalleVenta.IdDetalleVenta = (int)IdResult.Value;

                    //if (alumnos >= 1)
                    //{
                    //    result.Correct = true;
                    //}
                    //else
                    //{
                    //    result.Correct = false;
                    //    result.ErrorMessage = "No se insertó el registro";
                    //}

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
