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
            List<periodo> lst = null;
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
               
                    using (sgaEntities bdx = new sgaEntities())
                    {
                        lst = (from per in bdx.periodo
                               select per).ToList();
                    }
                    List<SelectListItem> items = lst.ConvertAll(d => {
                        return new SelectListItem()
                        {
                            Text = d.Per_Nom,
                            Value = d.Per_Nom,
                            Selected = false
                        };
                    });

                    ViewBag.Items = items;
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
        [HttpPost]
        public ActionResult GeneraGraf(string period)
        {
            int activos=0, inactivos=0;
            using (sgaEntities db = new sgaEntities())
            {
                var query = (from per in db.periodo
                             where per.Per_Nom.Equals(period)
                             select per).FirstOrDefault();
                if (query == null) return Json(new { Success = false, msg = "Ese periodo no existe" });
                activos += (from es in db.alumno
                           where es.Alum_Status==1
                           select es).Count();
                activos += (from ad in db.administrador
                            where ad.Adm_Status == 1
                            select ad).Count();
                activos += (from doc in db.docente
                            where doc.Doc_Status == 1
                            select doc).Count();
                activos += (from acu in db.acudiente
                            where acu.Acu_Status == 1
                            select acu).Count();
                inactivos += (from es in db.alumno
                            where es.Alum_Status != 1
                            select es).Count();
                inactivos += (from ad in db.administrador
                            where ad.Adm_Status != 1
                            select ad).Count();
                inactivos += (from doc in db.docente
                            where doc.Doc_Status != 1
                            select doc).Count();
                inactivos += (from acu in db.acudiente
                            where acu.Acu_Status != 1
                            select acu).Count();
            }
            return Json(new { Success = true, activos, inactivos });
        }
    }
}