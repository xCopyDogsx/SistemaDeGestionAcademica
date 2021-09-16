using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProyectoFinal.Controllers
{
    public class PanelController : Controller
    {
        // GET: Panel
        public ActionResult Index()
        {
            ViewBag.Activar = "Panel";
            if (Session["User"] != null)
            {
                if (Session["Rol"].Equals("Administrador"))
                {
                    using (sgaEntities db = new sgaEntities())
                    {
                       int Estu = (from es in db.alumno
                                    select es).Count();
                        int Admin = (from ad in db.administrador
                                     select ad).Count();
                        int Doc = (from doc in db.docente
                                   select doc).Count();
                        int Padre = (from pad in db.acudiente
                                     select pad).Count();
                        ViewBag.Estu = Estu;
                        ViewBag.Doc = Doc;
                        ViewBag.Admin = Admin;
                        ViewBag.Padre = Padre;
                    }
                }
            }
            return View();
        }
        public ActionResult Perfil()
        {
           
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                LoginController login = new LoginController();
                return login.SGA();
            }
        }
        public ActionResult Salir()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}