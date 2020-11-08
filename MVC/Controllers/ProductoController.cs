using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult GetAll()
        {
            ML.Result result = BL.Producto.GetAll();
            ML.Producto producto = new ML.Producto();

            producto.Productos = result.Objects;

            return View(producto);
        }

        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {
            ML.Result result = new ML.Result();
            ML.Producto producto = new ML.Producto();

            if (IdProducto == null)
            {
                ViewBag.Titulo = "Form";
                ViewBag.Accion = "Agregar";

                return View(producto);
            }
            else
            {
                ViewBag.Titulo = "Form";
                ViewBag.Accion = "Actualizar";

                producto.IdProducto = IdProducto.Value;
                result = BL.Producto.GetById(producto);
                

                

                if (result.Correct==true)
                {

                    producto.IdProducto = ((ML.Producto)result.Object).IdProducto;
                    producto.Nombre = ((ML.Producto)result.Object).Nombre;
                    producto.CodigoDeBarras = ((ML.Producto)result.Object).CodigoDeBarras;
                    producto.Cantidad = ((ML.Producto)result.Object).Cantidad;
                    producto.PrecioUnitario = ((ML.Producto)result.Object).PrecioUnitario;
                    producto.LogoTipo = ((ML.Producto)result.Object).LogoTipo;

                    return View(producto);
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Ocurrio un error";
                }

            }
            return View();
        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto, HttpPostedFileBase Imagen)
        {
            ML.Result result = new ML.Result();
            //producto.Productos=result.Objects;

            if (producto.IdProducto == 0)
            {
                producto.LogoTipo = ConvertToBytes(Imagen);
                result = BL.SucursalProducto.GetAllSucursal();
                result = BL.Producto.Add(producto, result.Objects);

                
                
                if (result.Correct==true)
                {
                    ViewBag.Message = "Producto agregado exitosamente";
                    return PartialView("Validation");
                }
                else
                {
                    ViewBag.Message = "No se agrego el registro";
                    return PartialView("Validation");
                }
            }
            else
            {
                producto.LogoTipo= ConvertToBytes(Imagen);
                result = BL.Producto.Update(producto);

                if (result.Correct == true)
                {
                    ViewBag.Message = "Actualizado";
                    return PartialView("Validation");
                }
                else
                {
                    ViewBag.Message = "No se actualizo";
                    return PartialView("Validation");
                }

            }
            
        }

        public byte[] ConvertToBytes(HttpPostedFileBase Imagen)
        {

            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Imagen.InputStream);
            data = reader.ReadBytes((int)Imagen.ContentLength);

            return data;
        }

        public ActionResult Delete(int IdProducto)
        {
            ML.Result result = BL.Producto.Delete(IdProducto);
            ML.Producto producto = new ML.Producto();

            producto.Productos = result.Objects;

            return View(producto);
        }
    }
}