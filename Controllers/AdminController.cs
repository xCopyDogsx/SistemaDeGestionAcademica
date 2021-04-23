using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
          
                Response.Redirect("~/Home/");
            }
            if (Request.HttpMethod == "POST")
            {
                if (Session["User"] == null)
                {
                    Response.Redirect("~/Home/");
                }
            }
        }

        public ActionResult Estudiantes()
        {
            if (Session["User"] == null)
            {
               HomeController home = new HomeController();
                ActionResult resultado = home.Index();
                return resultado;
            }else if(Session["Rol"].Equals("Adminstrador"))
            {
                return View();
            }
            else
            {
                HomeController home = new HomeController();
                ActionResult resultado = home.Index();
                return resultado;
            }
            
        }
        public ActionResult Docentes()
        {
            if (Session["User"] == null)
            {
                HomeController home = new HomeController();
                return home.Index();
            }
            else if (Session["Rol"].Equals("Adminstrador"))
            {
                return View();
            }
            else
            {
                HomeController home = new HomeController();
                return home.Index();
            }
        }
        public ActionResult Periodos()
        {
            if (Session["User"] == null)
            {
                HomeController home = new HomeController();
                return home.Index();
            }
            else if (Session["Rol"].Equals("Adminstrador"))
            {
                return View();
            }
            else
            {
                HomeController home = new HomeController();
                return home.Index();
            }
        }
       public ActionResult Materias()
        {
            if (Session["User"] == null)
            {
                HomeController home = new HomeController();
                return home.Index();
            }
            else if (Session["Rol"].Equals("Adminstrador"))
            {
                return View();
            }
            else
            {
                HomeController home = new HomeController();
                return home.Index();
            }
        }
        public ActionResult Admins()
        {
            if (Session["User"] == null)
            {
                HomeController home = new HomeController();
                return home.Index();
            }
            else if (Session["Rol"].Equals("Adminstrador"))
            {
                return View();
            }
            else
            {
                HomeController home = new HomeController();
                return home.Index();
            }
        }

    }
}