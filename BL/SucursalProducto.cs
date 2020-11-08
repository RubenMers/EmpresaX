using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SucursalProducto
    {
        public static ML.Result AddEF(ML.SucursalProducto sucursalProducto)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {

                    var query = context.SucursalProductoAdd(sucursalProducto.Producto.IdProducto, sucursalProducto.Sucursal.IdSucursal);



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

        public static ML.Result AddProductos(ML.SucursalProducto sucursalProducto)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {

                    var query = context.SucursalProductoAdd(sucursalProducto.Producto.IdProducto, sucursalProducto.Sucursal.IdSucursal);



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
        
        public static ML.Result GetAllSucursal()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {
                    var query = context.SucursalGetAll().ToList();
                    result.Objects = new List<Object>();

                    if (result.Objects != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
                            sucursalProducto.Sucursal = new ML.Sucursal();
                            sucursalProducto.Sucursal.IdSucursal = obj.IdSucursal;
                            sucursalProducto.Sucursal.Nombre = obj.Nombre;

                            result.Objects.Add(sucursalProducto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Object = "No hay registros";
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

        public static ML.Result GetAllProducto()
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
                            ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
                            sucursalProducto.Producto = new ML.Producto();
                            sucursalProducto.Producto.IdProducto = obj.IdProducto;
                            sucursalProducto.Producto.Nombre = obj.Nombre;

                            result.Objects.Add(sucursalProducto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Object = "No hay registros";
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

        public static ML.Result GetByIdProducto(ML.Sucursal sucursal)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {


                    var query = context.SucursalGetByProducto(sucursal.IdSucursal).ToList();


                    result.Objects = new List<Object>();

                    if (result.Objects != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
                            sucursalProducto.Sucursal = new ML.Sucursal();
                            sucursalProducto.Sucursal.IdSucursal = obj.IdSucursal;
                            sucursalProducto.Sucursal.Nombre = obj.NombreSucursal;
                            sucursalProducto.Producto = new ML.Producto();
                            sucursalProducto.Producto.IdProducto = Convert.ToInt32(obj.IdProducto);
                            sucursalProducto.Producto.Nombre = obj.NombreProducto;
                            sucursalProducto.Producto.PrecioUnitario = Convert.ToDecimal(obj.PrecioUnitario);
                            sucursalProducto.Producto.LogoTipo = Convert.FromBase64String(obj.LogoTipo);
                            sucursalProducto.Cantidad = Convert.ToInt32(obj.Cantidad);
                            sucursalProducto.DetalleVenta = new ML.DetalleVenta();
                            sucursalProducto.DetalleVenta.Cantidad = 1;
                            sucursalProducto.IdSucursalProducto = obj.IdSucursalProducto;

                            result.Objects.Add(sucursalProducto);
                        }





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

        public static ML.Result SucursalProductoGetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {


                    var query = context.SucursalProductoGetAll().ToList();


                    result.Objects = new List<Object>();

                    if (result.Objects != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
                            sucursalProducto.Sucursal = new ML.Sucursal();
                            sucursalProducto.Sucursal.IdSucursal = obj.IdSucursal;
                            sucursalProducto.Sucursal.Nombre = obj.NombreSucursal;
                            sucursalProducto.Producto = new ML.Producto();
                            sucursalProducto.Producto.IdProducto = Convert.ToInt32(obj.IdProducto);
                            sucursalProducto.Producto.Nombre = obj.NombreProducto;
                            sucursalProducto.Cantidad = Convert.ToInt32(obj.Cantidad);
                            sucursalProducto.IdSucursalProducto = obj.IdSucursalProducto;

                            result.Objects.Add(sucursalProducto);
                        }





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
