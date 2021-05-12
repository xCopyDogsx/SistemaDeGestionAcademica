using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Models.ViewModels;
using System.Linq.Dynamic;
using System.Web.Services;

namespace ProyectoFinal.Controllers
{
    public class AdminController : Controller
    {
        /*Logistica de la tabla*/
        public string draw = "";
        public string start = "";
        public string length = "";
        public string sortColumn = "";
        public string sortColumnDir = "";
        public string searchValue = "";
        public int pageSize, skip, recordsTotal;
        /*Variables normales*/
        private Helpers help = new Helpers();
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
 /*---------Administración estudiantes----------------*/
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
        [HttpPost]
        public ActionResult JsonEstudiantes()
        {
            List<TableAdminEstudiantesVM> lst = new List<TableAdminEstudiantesVM>();
            //logistica datatable
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            pageSize = length != null ? Convert.ToInt32(length) : 0;
            skip = start != null ? Convert.ToInt32(start) : 0;
            recordsTotal = 0;
            //Conexion con la base de datos
            using (sgaEntities db = new sgaEntities())
            {
                IQueryable<TableAdminEstudiantesVM> query =(from Est in db.alumno
                       select new TableAdminEstudiantesVM
                       {
                           Alum_ID = Est.Alum_ID,
                           Alum_Doc = Est.Alum_Doc,
                           Alum_Nom = Est.Alum_Nom,
                           Alum_Apel = Est.Alum_Apel,
                           Alum_Tel = Est.Alum_Tel,
                           Alum_Email = Est.Alum_Email, 
                           Alum_Status = Est.Alum_Status
                       });    
                if (searchValue != "")
                    query = query.Where(Est => Est.Alum_Doc.Contains(searchValue));
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    query = query.OrderBy(sortColumn + " " + sortColumnDir);
                }
                if (!sortColumn.Equals("Alum_Status2")&&!sortColumn.Equals("Acciones"))
                {
                    recordsTotal = query.Count();
                }
                else
                {
                    ViewBag.Error = "No se puede ordenar por este tipo de elemento";
                }
                if (recordsTotal!=0)
                {
                    lst = query.Skip(skip).Take(pageSize).ToList();
                }
                foreach(TableAdminEstudiantesVM buscador in lst)
                {
                    if (buscador.Alum_Status == 1)
                    {
                        buscador.Alum_Status2 = "<span class=\"badge badge-success\">Activo</span>";
                    }
                    else
                    {
                        buscador.Alum_Status2 = "<span class=\"badge badge-danger\">Inactivo</span>";
                    }
                    buscador.Acciones = "<button class=\"btn btn-primary btn-sm btnEditEst\" onclick=\"fntEditEst("+buscador.Alum_ID+ ")\" title=\"Editar\"><i class=\"fas fa-pencil-alt\" aria-hidden=\"true\"></i></button> ";
                    buscador.Acciones += "<button class=\"btn btn-danger btn-sm btnElimEst\" onclick=\"fntDelEst("+buscador.Alum_ID+ ")\" title=\"Eliminar\"><i class=\"far fa-trash-alt\" aria-hidden=\"true\"></i></button> ";
                }

            }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });
        }
        public bool Elim(int id)
        {
            using (sgaEntities db = new sgaEntities())
            {
                try
                {
                    var query = (from p in db.alumno
                                 where p.Alum_ID == id
                                 select p).Single();
                    db.alumno.Remove(query);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }          
            }
            return true;
        } 
        [HttpPost]
        public ActionResult EliminarEst(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                if (id < 0)
                         {
                return Json(new { Success = false, msg = "Por favor revise el ID a eliminar" });
                     }
                if (Elim(id))
                {
                    return Json(new { Success = true, msg = "El estudiante se ha eliminado con éxito" });
                }
                else
                {
                    return Json(new { Success = false, msg = "No se pudo eliminar" });
                }    
            }
            return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
        }
        [HttpPost]
        public ActionResult InsertEst(string strIdentificacion,string strNombre,string strApellido,string strEmail,string Telefono,long AcuId,string strPassword,int StatusRedy)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
            {
                var oValid = (from d in db.acudiente
                                where d.Acu_ID==AcuId
                                select d).FirstOrDefault();
                    var oValidEm = (from d in db.alumno
                                  where d.Alum_Email == strEmail
                                  select d).FirstOrDefault();
                    var oValidEm2 = (from d in db.acudiente
                                    where d.Acu_Email == strEmail
                                    select d).FirstOrDefault();
                    var oValidEm3 = (from d in db.docente
                                     where d.Doc_Email == strEmail
                                     select d).FirstOrDefault();
                    var oValidEm4 = (from d in db.administrador
                                     where d.Adm_Email == strEmail
                                     select d).FirstOrDefault();
                    if (oValidEm != null||oValidEm2!=null||oValidEm3!=null||oValidEm4!=null)
                      {
                        return Json(new { Success = false, msg = "El correo electronico ya existe en nuestra base de datos." });
                          }
                    if (oValid == null)
                         {
                    return Json(new { Success = false, msg = "El ID de dicho acudiente no existe, verifique por favor." });
                            }
                    try
                    {
                        alumno estu = new alumno();
                        estu.Alum_Doc = strIdentificacion;
                        estu.Alum_Nom = strNombre;
                        estu.Alum_Apel = strApellido;
                        estu.Alum_Tel = Telefono;
                        estu.Alum_Email = strEmail;
                        estu.Alum_Status = StatusRedy;
                        strPassword = help.GetSHA256(strPassword);
                        estu.Alum_Passw = strPassword;
                        estu.Acu_ID = AcuId;
                        db.alumno.Add(estu);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo ingresar en la BD motivo: "+e.Message });
                    }
                }
                return Json(new { Success = true, msg = "Estudiante almacenado con éxito. " });
             }
            else
             {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
                 }
        }
        [HttpPost]
        public ActionResult SelEst(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
                    
                    var oValidAl = (from d in db.alumno
                                    where d.Alum_ID == id
                                    select d).FirstOrDefault();
                    if (oValidAl==null)
                    {
                        return Json(new { Success = false, msg = "Error al precargar datos." });
                     }

                    return Json(new { Success = true, idpersona = oValidAl.Alum_ID, identificacion=oValidAl.Alum_Doc, 
                        nombres=oValidAl.Alum_Nom,
                        apellidos=oValidAl.Alum_Apel,telefono=oValidAl.Alum_Tel,
                        email_user=oValidAl.Alum_Email,acu_id=oValidAl.Acu_ID
                    });
                }
                
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult EditEst(int id,string strIdentificacion, string strNombre, string strApellido, string strEmail, string Telefono, long AcuId, string strPassword, int StatusRedy)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
                    var oValid = (from d in db.acudiente
                                  where d.Acu_ID == AcuId
                                  select d).FirstOrDefault();
                
                    var oValidEm2 = (from d in db.acudiente
                                     where d.Acu_Email == strEmail
                                     select d).FirstOrDefault();
                    var oValidEm3 = (from d in db.docente
                                     where d.Doc_Email == strEmail
                                     select d).FirstOrDefault();
                    var oValidEm4 = (from d in db.administrador
                                     where d.Adm_Email == strEmail
                                     select d).FirstOrDefault();
                    if (oValidEm2 != null || oValidEm3 != null || oValidEm4 != null)
                    {
                        return Json(new { Success = false, msg = "El correo electronico ya existe en nuestra base de datos." });
                    }
                    if (oValid == null)
                    {
                        return Json(new { Success = false, msg = "El ID de dicho acudiente no existe, verifique por favor." });
                    }
                    try
                    {
                        var std = db.alumno.First<alumno>();
                        
                        std.Alum_Doc = strIdentificacion;
                        std.Alum_Nom = strNombre;
                        std.Alum_Apel = strApellido;
                        std.Alum_Tel = Telefono;
                        std.Alum_Email = strEmail;
                        std.Alum_Status = StatusRedy;
                        strPassword = help.GetSHA256(strPassword);
                        std.Alum_Passw = strPassword;
                        std.Acu_ID = AcuId;
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo actualizar en la BD motivo: " + e.Message });
                    }
                }
                return Json(new { Success = true, msg = "Estudiante actualizado con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }






        /*---------Administración docentes----------------*/
        public ActionResult Docentes()
        {
            if (Session["User"] == null)
            {
                HomeController home = new HomeController();
                return home.Index();
            }
            else if (Session["Rol"].Equals("Adminstrador"))
            {
                ViewBag.Activar = "Docentes";
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
                ViewBag.Activar = "Periodos";
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
                ViewBag.Activar = "Materias";
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
                ViewBag.Activar = "Admins";
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