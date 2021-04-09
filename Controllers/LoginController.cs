using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult SGA()
        {
            return View();
        }
        public ActionResult Admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            try
            {
                using (Models.sgaEntities db=new Models.sgaEntities())
                {
                    var oUserStu = (from d in db.alumno
                                where d.Alum_Email==email.Trim() && d.Alum_Passw==password.Trim() && d.Alum_Status==1
                                select d).FirstOrDefault();
                    var oUserAd = (from d in db.administrador
                                 where d.Adm_Email == email.Trim() && d.Adm_Passw == password.Trim() && d.Adm_Status==1
                                 select d).FirstOrDefault();
                    var oUserFa = (from d in db.acudiente
                                   where d.Acu_Email == email.Trim() && d.Acu_Passw == password.Trim() && d.Acu_Status==1
                                   select d).FirstOrDefault();
                    var oUserTe = (from d in db.docente
                                   where d.Doc_Email == email.Trim() && d.Doc_Passw == password.Trim() && d.Doc_Status == 1
                                   select d).FirstOrDefault();
                    if (oUserStu == null||oUserAd==null||oUserFa==null||oUserTe==null)
                    {
                        ViewBag.Error = "Contraseña o usuario incorrectos, también puede ser que el usuario este inactivo";
                        return View();                          
                    }
                    if (oUserStu != null)
                    {
                        ViewBag.Rol = "Estudiante";
                        
                    }
                    if (oUserAd != null)
                    {
                        ViewBag.Rol = "Administrador";
                    }
                    if (oUserFa != null)
                    {
                        ViewBag.Rol = "Acudiente";
                    }
                    if (oUserTe != null)
                    {
                        ViewBag.Rol = "Docente";
                    }
                }
                return RedirectToAction("Index", "Home");
            }catch(Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View();
            }

        }
           


    }
}