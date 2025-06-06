using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProductos.Datos;
using WebProductos.Models;

namespace WebProductos.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            //Declarar la lista vacia
            List<E_Producto> lista = new List<E_Producto>();
            try
            {
                //Crear un objeto de la capa de datos para poder usar sus metodos
                D_Producto datos = new D_Producto();

                //Obtenemos la lista de productos de la capa de datos
                lista = datos.ObtenerTodos();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            //pasamos la lista como modelo hacia la vista
            return View("Principal", lista);
        }

        public ActionResult IrAgregar()
        {
            return View("VistaAgregar");
        }

        public ActionResult Agregar(E_Producto objProducto)
        {
            try
            {
                //Crear un objeto de la capa de datos para poder usar sus metodos
                D_Producto datos = new D_Producto();

                ////Validaciones generando erros y usando try catch
                //if (objProducto.Descripcion.Count() <= 3)
                //    throw new Exception("La descripción debe ser de almenos 4 caracteres");

                //if(objProducto.Precio < 0)
                //    throw new Exception("El precio debe ser mayor a 0");

                //variable para saber si hay errores
                bool hayErrores = false;
                string mensajeValidaciones = "";
                //Validando la longitud de la descripcion
                if (objProducto.Descripcion.Count() <= 3)
                {
                    hayErrores = true;
                    mensajeValidaciones += "La <b>descripción</b> debe ser de almenos 4 caracteres <br>";
                }
                //Validar el precio
                if (objProducto.Precio < 0)
                {
                    hayErrores = true;
                    mensajeValidaciones += "El <b>precio</b> debe ser mayor a 0 <br>";
                }

                if (hayErrores == true)
                {
                    //Si hay errores creamos un TempData para mostrar los errores
                    TempData["validaciones"] = mensajeValidaciones;
                    //Regreso a la vista de agregar para mostrar el mensaje de validaciones
                    return View("VistaAgregar");
                }
                else
                {
                    //No hay errores entonces Agregamos el producto
                    //Mando a llamar al metodo Agregar de la capa de datos
                    datos.AgregarProducto(objProducto);
                    //Creo un TempData con un mensaje
                    TempData["mensaje"] = $"El producto {objProducto.Descripcion} se registro correctamente";
                    //Regreso a la vista principal
                    return RedirectToAction("Index");
                }   
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult IrEditar(int idProducto)
        {
            //Crear un objeto de la capa de datos para poder usar sus metodos
            D_Producto datos = new D_Producto();
            //Obtener el producto desde la capa de datos
            E_Producto producto = datos.ObtenerProductoPorId(idProducto);
            //Pasamos el objeto producto a la vista como modelo
            return View("VistaEditar", producto);
        }

        public ActionResult Editar(E_Producto producto)
        {
            //Crear un objeto de la capa de datos para poder usar sus metodos
            D_Producto datos = new D_Producto();
            datos.EditarProducto(producto);
            TempData["mensaje"] = $"El producto con ID:{producto.IdProducto} se modifico correctamente";
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int idProducto)
        {
            //Crear un objeto de la capa de datos para poder usar sus metodos
            D_Producto datos = new D_Producto();
            datos.EliminarProducto(idProducto);
            TempData["mensaje"] = $"El producto con ID:{idProducto} se elimino correctamente";
            return RedirectToAction("Index");
        }
    }
}