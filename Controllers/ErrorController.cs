using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [HttpGet]
        public ActionResult UnauthorizedOperation(String operacion, String modulo, String msjeErrorExcepcion)
        {
            ViewBag.operacion = operacion;
            ViewBag.modulo = modulo;
            ViewBag.msjeErrorExcepcion = msjeErrorExcepcion;
            return View();
        }
        public ActionResult Index(int error = 0)
        {
            switch (error)
            {
                case 505:
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Esto es muy vergonzoso, esperemos que no vuelva a pasar ..";
                    ViewBag.Code = "505";
                    break;

                case 404:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "La URL que está intentando ingresar no existe";
                    ViewBag.Code = "404";
                    break;
                case 403:
                    ViewBag.Title = "¡Entraste donde no era!";
                    ViewBag.Description = "Al link que quieres entrar necesita de los permisos necesarios para continuar.";
                    ViewBag.Code = "403";
                    break;
                default:
                    ViewBag.Title = "Error interno";
                    ViewBag.Description = "Vaya, has encontrado un error en el servidor :c o posiblemente no tienes los permisos necesarios";
                    ViewBag.Code = "500";
                    break;
            }

            return View("~/views/error/ErrorPage.cshtml");
        }
    }
}