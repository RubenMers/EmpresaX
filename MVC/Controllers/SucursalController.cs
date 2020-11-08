using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult GetAll()
        {
            ML.Result result = BL.Sucursal.GetAll();
            ML.Sucursal sucursal = new ML.Sucursal();
            sucursal.Sucursales = result.Objects;
            return View(sucursal);
        }

        public ActionResult GetAllAdministrador()
        {
            ML.Result result = BL.Sucursal.GetAll();
            ML.Sucursal sucursal = new ML.Sucursal();
            sucursal.Sucursales = result.Objects;
            return View(sucursal);
        }

        public ActionResult Productos(ML.Sucursal sucursal)
        {
            ML.Result result = BL.SucursalProducto.GetByIdProducto(sucursal);

            ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
            sucursalProducto.sucursalProductos = result.Objects;

            return View(sucursalProducto);
        }

        public ActionResult ProductosAdministrador(ML.Sucursal sucursal)
        {
            ML.Result result = BL.SucursalProducto.GetByIdProducto(sucursal);

            ML.SucursalProducto sucursalProducto = new ML.SucursalProducto();
            sucursalProducto.sucursalProductos = result.Objects;

            return View(sucursalProducto);
        }

        [HttpGet]
        public ActionResult Form(int? IdSucursal)
        {
            //ML.Result result = new ML.Result();
            ML.Sucursal sucursal = new ML.Sucursal();

            if (IdSucursal == null)
            {
                ViewBag.Message = "Registrar Producto";

                return View(sucursal);
            }
            else
            {
                ViewBag.Titulo = "Actualizar Producto";
                ViewBag.Accion = "Actualizar";

                sucursal.IdSucursal = IdSucursal.Value;
                var result = BL.Sucursal.Get(sucursal);

                if (result.Object != null)
                {

                    sucursal.IdSucursal = ((ML.Sucursal)result.Object).IdSucursal;
                    sucursal.Nombre = ((ML.Sucursal)result.Object).Nombre;


                    return View(sucursal);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("Validation");
                }
            }
            
        }
        [HttpPost]
        public ActionResult Form(ML.Sucursal sucursal)
        {
            ML.Result result = new ML.Result();

            if (sucursal.IdSucursal == 0)
            {
                result = BL.SucursalProducto.GetAllProducto();
                result = BL.Sucursal.Add(sucursal, result.Objects);
                if (result.Correct)
                {
                    ViewBag.Message = "Sucursal agregada correctamente";
                    return PartialView("Validation");
                }
                else
                {
                    ViewBag.Message = "La sucursal agregada no se ha podido agregar!";
                    return PartialView("Validation");
                }
            }
            else
            {
                result = BL.Sucursal.Update(sucursal);
                if (result.Correct)
                {
                    ViewBag.Message = "Sucursal actualizada correctamente";
                    return PartialView("Validation");
                }
                else
                {
                    ViewBag.Message = "La sucursal  no se ha podido actualizar!";
                    return PartialView("Validation");
                }
            }
            return View();
        }

        public ActionResult AddCarrito(int IdSucursalProducto)
        {
            
            ML.SucursalProducto detalle = new ML.SucursalProducto();
            detalle.Producto = new ML.Producto();
            detalle.DetalleVenta = new ML.DetalleVenta();
            
            if (Session["Carrito"] == null)
            {

                detalle.IdSucursalProducto = IdSucursalProducto;

                
                var result = BL.Producto.GetByIdEFProducto(detalle);

                result.Objects = new List<Object>();
                result.Objects.Add(result.Object);
                Session["Carrito"] = result.Objects;

                return View("AddCarrito", result);
            }
            else
            {
                
                detalle.IdSucursalProducto = IdSucursalProducto;
                var result = BL.Producto.GetByIdEFProducto(detalle);

                result.Objects = (List<Object>)Session["Carrito"];

                int pos = 0;
                bool comprobar = false;

                foreach (ML.SucursalProducto productos in result.Objects.ToList())
                {
                    if (productos.IdSucursalProducto == IdSucursalProducto)
                    {
                        comprobar = true;
                        pos = productos.IdSucursalProducto;
                    }
                    else
                    {
                        comprobar = false;
                    }
                }


                if (comprobar == true)
                {
                    foreach (ML.SucursalProducto productos in result.Objects.ToList())
                    {
                        productos.Producto = new ML.Producto();
                        //productos.DetalleVenta = new ML.DetalleVenta();
                        //productos.DetalleVenta.Venta = new ML.Venta();

                        if (productos.IdSucursalProducto == pos)
                        {
                            
                                productos.DetalleVenta.Cantidad++;
                            


                            productos.DetalleVenta.Total = (Int32)(productos.DetalleVenta.Cantidad * productos.Producto.PrecioUnitario);
                            //detalle.Venta.Total = Convert.ToInt32((detalle.Cantidad * detalle.Producto.Precio));
                            break;
                        }
                    }
                }
                else
                {
                    result.Objects.Add(result.Object);
                    Session["Carrito"] = result.Objects;
                }

                return View("AddCarrito", result);

            }
        }

        public ActionResult Delete(int IdSucursal)
        {
            ML.Result result = BL.Sucursal.Delete(IdSucursal);
            

            if (result.Correct == true)
            {
                @ViewBag.Message = "Borrado correctamente";
                
            }
            else
            {
                
                @ViewBag.Message = "No se borro el registro";
            }
            return PartialView("ValidationModal");
        }
    }
}