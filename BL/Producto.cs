using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {
                    var query = context.ProductoGetAll().ToList();

                    result.Objects = new List<Object>();



                    if (result.Objects != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.Nombre;
                            producto.CodigoDeBarras = Convert.ToInt32(obj.CodigoDeBarras);
                            producto.Cantidad = Convert.ToInt32(obj.Cantidad);
                            producto.PrecioUnitario = Convert.ToInt32(obj.PrecioUnitario);
                            producto.LogoTipo = Convert.FromBase64String(obj.LogoTipo);

                            result.Objects.Add(producto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay datos!";
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Add(ML.Producto producto, List<Object> Objects)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {
                    var IdResult = new ObjectParameter("IdProducto", typeof(int));
                    var query = context.ProductoAdd(IdResult, producto.Nombre, producto.CodigoDeBarras, producto.Cantidad, producto.PrecioUnitario, Convert.ToBase64String(producto.LogoTipo));
                    producto.IdProducto = (int)IdResult.Value;

                    foreach (ML.SucursalProducto sucursalProducto in Objects)
                    {
                        sucursalProducto.Producto = producto;
                        BL.SucursalProducto.AddEF(sucursalProducto);
                    }

                    if (query >= 1)
                    {
                        result.Correct = true;
                        result.Object = "Registro agregado correctamente";
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo agregar el registro";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {
                    var query = context.ProductoDelete(IdProducto);

                    if (query >= 1)
                    {
                        result.Correct = true;
                        result.Object = "Registro eliminado con exito";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {
                    var query = context.ProductoUpdate(producto.IdProducto,producto.Nombre, producto.CodigoDeBarras, Convert.ToInt32(producto.Cantidad), producto.PrecioUnitario, Convert.ToBase64String(producto.LogoTipo));

                    if (query >= 1)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.Object = "No se modifico el registro";
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

        public static ML.Result GetById(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {
                    var query = context.ProductoGetById(producto.IdProducto).FirstOrDefault();

                    

                    if (query != null)
                    {
                        //ML.Producto productos = new ML.Producto();
                        producto.IdProducto = query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.CodigoDeBarras = Convert.ToInt32(query.CodigoDeBarras);
                        producto.Cantidad = Convert.ToInt32(query.Cantidad);
                        producto.PrecioUnitario = Convert.ToInt32(query.PrecioUnitario);
                        producto.LogoTipo = Convert.FromBase64String(query.LogoTipo);
                        //result.Object = producto;
                        result.Object=producto;


                    }
                    else
                    {
                        result.Correct = false;
                        result.Object = "No hay datos";
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

        public static ML.Result GetByIdEFProducto(ML.SucursalProducto sucursalProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {


                    var query = context.ProductoGetByIdProducto(sucursalProducto.IdSucursalProducto).FirstOrDefault();


                    result.Objects = new List<Object>();

                    if (query != null)
                    {

                        //ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
                        sucursalProducto.Producto = new ML.Producto();
                        sucursalProducto.Sucursal = new ML.Sucursal();
                        sucursalProducto.DetalleVenta = new ML.DetalleVenta();
                        sucursalProducto.IdSucursalProducto = query.IdSucursalProducto;
                        sucursalProducto.Producto.IdProducto = query.IdProducto;
                        sucursalProducto.Producto.Nombre = query.NombreProducto;
                        sucursalProducto.Producto.PrecioUnitario = Convert.ToDecimal(query.PrecioUnitario);
                        sucursalProducto.Producto.LogoTipo = Convert.FromBase64String(query.LogoTipo);
                        sucursalProducto.Sucursal.IdSucursal = query.IdSucursal;
                        sucursalProducto.Sucursal.Nombre = query.NombreSucursal;
                        sucursalProducto.Cantidad = Convert.ToInt32(query.Cantidad);
                        sucursalProducto.DetalleVenta.Cantidad = 1;
                        result.Object = sucursalProducto;






                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
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
