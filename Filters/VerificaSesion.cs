
using ProyectoFinal.Controllers;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace ProyectoFinal.Filters
{
    public class VerificaSesion : ActionFilterAttribute
    {
        private alumno oUserStu;
        private docente oUserTe;
        private administrador oUserAdmin;
        private acudiente oUserFa;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {   
            try
            {
                base.OnActionExecuting(filterContext);
                oUserTe = (docente)HttpContext.Current.Session["User"];
                oUserAdmin = (administrador)HttpContext.Current.Session["User"];
                oUserStu = (alumno)HttpContext.Current.Session["User"];
                oUserFa = (acudiente)HttpContext.Current.Session["User"];
                if (oUserTe == null && oUserStu == null && oUserAdmin == null && oUserFa==null)
                {
                    if(filterContext.Controller is LoginController == false)
                    {

                        // filterContext.HttpContext.Response.Redirect("~/Login/SGA");
                        //filterContext.HttpContext.Response.Redirect("./SGA");
                        //RedirectToAction("SGA", "Login");
                    }

                }

            }
            catch (Exception)
            {
               // filterContext.Result = new RedirectResult("~/Login/SGA");
            }
        }

    }
}