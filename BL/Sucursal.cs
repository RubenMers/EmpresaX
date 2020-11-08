using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sucursal
    {
        public static ML.Result GetAll()
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
                            ML.Sucursal sucursal = new ML.Sucursal();
                            sucursal.IdSucursal = obj.IdSucursal;
                            sucursal.Nombre = obj.Nombre;

                            result.Objects.Add(sucursal);
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

        public static ML.Result GetByIdEF(ML.Sucursal sucursal)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {


                    var alumnos = context.SucursalGetByProducto(sucursal.IdSucursal).ToList();


                    result.Objects = new List<Object>();

                    if (alumnos != null)
                    {
                        foreach (var obj in alumnos)
                        {
                            ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
                            sucursalProducto.Producto = new ML.Producto();
                            sucursalProducto.Sucursal = new ML.Sucursal();

                            sucursalProducto.IdSucursalProducto = obj.IdSucursalProducto;
                            sucursalProducto.Producto.IdProducto = obj.IdProducto;
                            sucursalProducto.Producto.Nombre = obj.NombreProducto;
                            sucursalProducto.Sucursal.IdSucursal = obj.IdSucursal;
                            sucursalProducto.Sucursal.Nombre = obj.NombreSucursal;
                            sucursalProducto.Cantidad = Convert.ToInt32(obj.Cantidad);

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

        public static ML.Result Add(ML.Sucursal sucursal, List<Object> Objects)
        {

            ML.Result result = new ML.Result();
            try
            {

                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {
                    var IdResult = new ObjectParameter("IdSucursal", typeof(int));
                    var query = context.SucursalAdd(IdResult, sucursal.Nombre);
                    sucursal.IdSucursal = (int)IdResult.Value;

                    foreach (ML.SucursalProducto sucursalProducto in Objects)
                    {
                        sucursalProducto.Sucursal = sucursal;
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

        public static ML.Result Delete(int IdSucursal)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {
                    var query = context.SucursalDelete(IdSucursal);

                    if (query>=1)
                    {
                        
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Object = "No se elimino el registro";
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

        public static ML.Result Update(ML.Sucursal sucursal)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {
                    var query = context.SucursalUpdate(sucursal.IdSucursal,sucursal.Nombre);

                    if (query >= 1)
                    {

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Object = "No se elimino el registro";
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

        public static ML.Result Get(ML.Sucursal sucursal)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EmpresaXEntities context = new DL.EmpresaXEntities())
                {


                    var alumnos = context.SucursalGet(sucursal.IdSucursal).FirstOrDefault();


                    if (alumnos != null)
                    {
                        ML.Sucursal sucursales = new ML.Sucursal();
                        sucursales.IdSucursal = alumnos.IdSucursal;
                        sucursales.Nombre = alumnos.Nombre;
                        result.Object = sucursales;


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
