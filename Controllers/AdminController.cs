using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Models.ViewModels;
using System.Linq.Dynamic;

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
        public DateTime fecha = DateTime.Now;
        /*Variables normales*/
        private readonly Helpers help = new Helpers();
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
                        var std = db.alumno.FirstOrDefault(edit=>edit.Alum_ID==id);
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
        [HttpPost]
        public ActionResult JsonDocentes()
        {
            List<TableAdminDocentesVM> lst = new List<TableAdminDocentesVM>();
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
                IQueryable<TableAdminDocentesVM> query = (from Est in db.docente
                                                             select new TableAdminDocentesVM
                                                             {
                                                                 Doc_ID = Est.Doc_ID,
                                                                 Doc_Doc = Est.Doc_Doc,
                                                                 Doc_Nom = Est.Doc_Nom,
                                                                 Doc_Apel = Est.Doc_Apel,
                                                                 Doc_Tel = Est.Doc_Tel,
                                                                 Doc_Email = Est.Doc_Email,
                                                                 Doc_Status = Est.Doc_Status
                                                             });
                if (searchValue != "")
                    query = query.Where(Est => Est.Doc_Doc.Contains(searchValue));
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    query = query.OrderBy(sortColumn + " " + sortColumnDir);
                }
                if (!sortColumn.Equals("Doc_Status2") && !sortColumn.Equals("Acciones"))
                {
                    recordsTotal = query.Count();
                }
                else
                {
                    ViewBag.Error = "No se puede ordenar por este tipo de elemento";
                }
                if (recordsTotal != 0)
                {
                    lst = query.Skip(skip).Take(pageSize).ToList();
                }
                foreach (TableAdminDocentesVM buscador in lst)
                {
                    if (buscador.Doc_Status == 1)
                    {
                        buscador.Doc_Status2 = "<span class=\"badge badge-success\">Activo</span>";
                    }
                    else
                    {
                        buscador.Doc_Status2 = "<span class=\"badge badge-danger\">Inactivo</span>";
                    }
                    buscador.Acciones = "<button class=\"btn btn-primary btn-sm \" onclick=\"fntEditDoc(" + buscador.Doc_ID + ")\" title=\"Editar\"><i class=\"fas fa-pencil-alt\" aria-hidden=\"true\"></i></button> ";
                    buscador.Acciones += "<button class=\"btn btn-info btn-sm \" onclick=\"fntVerDoc(" + buscador.Doc_ID + ")\" title=\"Ver estudiante a cargo\"><i class=\"fas fa-eye\" aria-hidden=\"true\"></i></button> ";
                    buscador.Acciones += "<button class=\"btn btn-danger btn-sm \" onclick=\"fntDelDoc(" + buscador.Doc_ID + ")\" title=\"Eliminar\"><i class=\"far fa-trash-alt\" aria-hidden=\"true\"></i></button> ";
                }

            }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });
        }
        public bool ElimD(int id)
        {
            using (sgaEntities db = new sgaEntities())
            {
                try
                {
                    var query = (from p in db.docente
                                 where p.Doc_ID == id
                                 select p).Single();
                    db.docente.Remove(query);
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
        public ActionResult EliminarDoc(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                if (id < 0)
                {
                    return Json(new { Success = false, msg = "Por favor revise el ID a eliminar" });
                }
                if (ElimD(id))
                {
                    return Json(new { Success = true, msg = "El docente se ha eliminado con éxito" });
                }
                else
                {
                    return Json(new { Success = false, msg = "No se pudo eliminar" });
                }
            }
            return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
        }
        [HttpPost]
        public ActionResult InsertDoc(string strIdentificacion, string strNombre, string strApellido, string strEmail, string Telefono, string strPassword, int StatusRedy)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {

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
                    if (oValidEm != null || oValidEm2 != null || oValidEm3 != null || oValidEm4 != null)
                    {
                        return Json(new { Success = false, msg = "El correo electronico ya existe en nuestra base de datos." });
                    }
                    try
                    {
                        docente estu = new docente();
                        estu.Doc_Doc = strIdentificacion;
                        estu.Doc_Nom = strNombre;
                        estu.Doc_Apel = strApellido;
                        estu.Doc_Tel = Telefono;
                        estu.Doc_Email = strEmail;
                        estu.Doc_Status = StatusRedy;
                        strPassword = help.GetSHA256(strPassword);
                        estu.Doc_Passw = strPassword;
                        db.docente.Add(estu);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo ingresar en la BD motivo: " + e.Message });
                    }
                }
                return Json(new { Success = true, msg = "Docente almacenado con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult SelDoc(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {

                    var oValidAl = (from d in db.docente
                                    where d.Doc_ID == id
                                    select d).FirstOrDefault();
                    if (oValidAl == null)
                    {
                        return Json(new { Success = false, msg = "Error al precargar datos." });
                    }

                    return Json(new
                    {
                        Success = true,
                        idpersona = oValidAl.Doc_ID,
                        identificacion = oValidAl.Doc_Doc,
                        nombres = oValidAl.Doc_Nom,
                        apellidos = oValidAl.Doc_Apel,
                        telefono = oValidAl.Doc_Tel,
                        email_user = oValidAl.Doc_Email
                    });
                }
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult EditDoc(int id, string strIdentificacion, string strNombre, string strApellido, string strEmail, string Telefono, string strPassword, int StatusRedy)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
                   
                    var oValidEm2 = (from d in db.acudiente
                                     where d.Acu_Email == strEmail
                                     select d).FirstOrDefault();
                    var oValidEm3 = (from d in db.alumno
                                     where d.Alum_Email == strEmail
                                     select d).FirstOrDefault();
                    var oValidEm4 = (from d in db.administrador
                                     where d.Adm_Email == strEmail
                                     select d).FirstOrDefault();
                    if (oValidEm2 != null || oValidEm3 != null || oValidEm4 != null)
                    {
                        return Json(new { Success = false, msg = "El correo electronico ya existe en nuestra base de datos." });
                    }
                    
                    try
                    {
                        var std = db.docente.FirstOrDefault(edit => edit.Doc_ID == id);
                        std.Doc_Doc = strIdentificacion;
                        std.Doc_Nom = strNombre;
                        std.Doc_Apel = strApellido;
                        std.Doc_Tel = Telefono;
                        std.Doc_Email = strEmail;
                        std.Doc_Status = StatusRedy;
                        strPassword = help.GetSHA256(strPassword);
                        std.Doc_Passw = strPassword;
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo actualizar en la BD motivo: " + e.Message });
                    }
                }
                return Json(new { Success = true, msg = "Docente actualizado con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult SelDocWhitAs(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {

                    var oValidAl = (from d in db.docente
                                    where d.Doc_ID == id
                                    select d).FirstOrDefault();
                    var materias = (from mat in db.materia
                                  join mat_cls in db.materia_clase on mat.Mat_ID equals mat_cls.Mat_ID
                                  join clas in db.clase on mat_cls.Clas_ID equals clas.Clas_ID
                                  join docente_cl in db.docente_clase on clas.Clas_ID equals docente_cl.Clas_ID
                                  join doc in db.docente on docente_cl.Doc_ID equals doc.Doc_ID
                                  where doc.Doc_ID == id && mat_cls.Doc_ID==doc.Doc_ID
                                  select mat.Mat_Nom).ToList();
                    var cursos = (from curs in db.curso
                                  join clas in db.clase on curs.Curs_ID equals clas.Curs_ID
                                  join docente_cl in db.docente_clase on clas.Clas_ID equals docente_cl.Clas_ID
                                  join doc in db.docente on docente_cl.Doc_ID equals doc.Doc_ID
                                  where doc.Doc_ID == id
                                  select curs.Curs_Nom).ToList();
                    if (oValidAl == null)
                    {
                        return Json(new { Success = false, msg = "Error al precargar datos." });
                    }
                    if (materias == null || materias.Count()==0)
                    {
                        return Json(new { Success = false, msg = "El docente no tiene materias asignadas para impartir, por favor asigne el docente a una clase y a una materia." });
                    }
                    if (cursos == null || cursos.Count() == 0)
                    {
                        return Json(new { Success = false, msg = "El docente no tiene materias asignadas para impartir, por favor asigne el docente a una clase y a una materia." });
                    }
                    return Json(new
                    {
                        Success = true,
                        idpersona = oValidAl.Doc_Doc,
                        nombres = oValidAl.Doc_Nom,
                        apellidos = oValidAl.Doc_Apel,
                        cursos=cursos,
                        materias = materias
                    });
                }
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }

        /*-----------Administración de acudientes------------- */
        public ActionResult Acudientes()
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
        [HttpPost]
        public ActionResult JsonAcudientes()
        {
            List<TableAdminAcudientesVM> lst = new List<TableAdminAcudientesVM>();
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
                IQueryable<TableAdminAcudientesVM> query = (from Est in db.acudiente
                                                          select new TableAdminAcudientesVM
                                                          {
                                                              Acu_ID = Est.Acu_ID,
                                                              Acu_Doc = Est.Acu_Doc,
                                                              Acu_Nom = Est.Acu_Nom,
                                                              Acu_Apel = Est.Acu_Apel,
                                                              Acu_Tel = Est.Acu_Tel,
                                                              Acu_Email = Est.Acu_Email,
                                                              Acu_Status = Est.Acu_Status
                                                          });
                if (searchValue != "")
                    query = query.Where(Est => Est.Acu_Doc.Contains(searchValue));
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    query = query.OrderBy(sortColumn + " " + sortColumnDir);
                }
                if (!sortColumn.Equals("Acu_Status2") && !sortColumn.Equals("Acciones"))
                {
                    recordsTotal = query.Count();
                }
                else
                {
                    ViewBag.Error = "No se puede ordenar por este tipo de elemento";
                }
                if (recordsTotal != 0)
                {
                    lst = query.Skip(skip).Take(pageSize).ToList();
                }
                foreach (TableAdminAcudientesVM buscador in lst)
                {
                    if (buscador.Acu_Status == 1)
                    {
                        buscador.Acu_Status2 = "<span class=\"badge badge-success\">Activo</span>";
                    }
                    else
                    {
                        buscador.Acu_Status2 = "<span class=\"badge badge-danger\">Inactivo</span>";
                    }
                    buscador.Acciones = "<button class=\"btn btn-primary btn-sm \" onclick=\"fntEditAcu(" + buscador.Acu_ID + ")\" title=\"Editar\"><i class=\"fas fa-pencil-alt\" aria-hidden=\"true\"></i></button> ";
                    buscador.Acciones += "<button class=\"btn btn-info btn-sm \" onclick=\"fntVerAcu(" + buscador.Acu_ID + ")\" title=\"Ver docente\"><i class=\"fas fa-eye\" aria-hidden=\"true\"></i></button> ";
                    buscador.Acciones += "<button class=\"btn btn-danger btn-sm \" onclick=\"fntDelAcu(" + buscador.Acu_ID + ")\" title=\"Eliminar\"><i class=\"far fa-trash-alt\" aria-hidden=\"true\"></i></button> ";
                }

            }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });
        }
        public bool ElimA(int id)
        {
            using (sgaEntities db = new sgaEntities())
            {
                try
                {
                    var query = (from p in db.acudiente
                                 where p.Acu_ID == id
                                 select p).Single();
                    db.acudiente.Remove(query);
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
        public ActionResult EliminarAcu(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                if (id < 0)
                {
                    return Json(new { Success = false, msg = "Por favor revise el ID a eliminar" });
                }
                if (ElimA(id))
                {
                    return Json(new { Success = true, msg = "El acudiente se ha eliminado con éxito" });
                }
                else
                {
                    return Json(new { Success = false, msg = "No se pudo eliminar" });
                }
            }
            return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
        }
        [HttpPost]
        public ActionResult InsertAcu(string strIdentificacion, string strNombre, string strApellido, string strEmail, string Telefono, string strPassword, int StatusRedy)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
                    int contador = (from d in db.acudiente
                                    select d.Acu_ID
                                    ).Count();
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
                    if (oValidEm != null ||  oValidEm3 != null || oValidEm4 != null)
                    {
                        return Json(new { Success = false, msg = "El correo electronico ya existe en nuestra base de datos." });
                    }
                    try
                    {
                        acudiente estu = new acudiente();
                        estu.Acu_ID = contador + 1;
                        estu.Acu_Doc = strIdentificacion;
                        estu.Acu_Nom = strNombre;
                        estu.Acu_Apel = strApellido;
                        estu.Acu_Tel = Telefono;
                        estu.Acu_Email = strEmail;
                        estu.Acu_Status = StatusRedy;
                        strPassword = help.GetSHA256(strPassword);
                        estu.Acu_Passw = strPassword;
                        db.acudiente.Add(estu);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        e.InnerException.ToString();
                        return Json(new { Success = false, msg = "No se pudo ingresar en la BD motivo: " + e.InnerException.Message });
                    }
                }
                return Json(new { Success = true, msg = "Acudiente almacenado con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult SelAcu(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {

                    var oValidAl = (from d in db.acudiente
                                    where d.Acu_ID == id
                                    select d).FirstOrDefault();
                    if (oValidAl == null)
                    {
                        return Json(new { Success = false, msg = "Error al precargar datos." });
                    }

                    return Json(new
                    {
                        Success = true,
                        idpersona = oValidAl.Acu_ID,
                        identificacion = oValidAl.Acu_Doc,
                        nombres = oValidAl.Acu_Nom,
                        apellidos = oValidAl.Acu_Apel,
                        telefono = oValidAl.Acu_Tel,
                        email_user = oValidAl.Acu_Email
                    });
                }
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult EditAcu(int id, string strIdentificacion, string strNombre, string strApellido, string strEmail, string Telefono, string strPassword, int StatusRedy)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {

                    var oValidEm2 = (from d in db.docente
                                     where d.Doc_Email == strEmail
                                     select d).FirstOrDefault();
                    var oValidEm3 = (from d in db.alumno
                                     where d.Alum_Email == strEmail
                                     select d).FirstOrDefault();
                    var oValidEm4 = (from d in db.administrador
                                     where d.Adm_Email == strEmail
                                     select d).FirstOrDefault();
                    if (oValidEm2 != null || oValidEm3 != null || oValidEm4 != null)
                    {
                        return Json(new { Success = false, msg = "El correo electronico ya existe en nuestra base de datos." });
                    }

                    try
                    {
                        var std = db.acudiente.FirstOrDefault(edit => edit.Acu_ID == id);
                        std.Acu_Doc = strIdentificacion;
                        std.Acu_Nom = strNombre;
                        std.Acu_Apel = strApellido;
                        std.Acu_Tel = Telefono;
                        std.Acu_Email = strEmail;
                        std.Acu_Status = StatusRedy;
                        strPassword = help.GetSHA256(strPassword);
                        std.Acu_Passw = strPassword;
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo actualizar en la BD motivo: " + e.Message });
                    }
                }
                return Json(new { Success = true, msg = "Acudiente actualizado con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult SelAcuWhitAs(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {

                    var oValidAl = (from d in db.alumno
                                    join acu in db.acudiente on d.Acu_ID equals acu.Acu_ID
                                    where acu.Acu_ID == id
                                    select d.Alum_Nom).FirstOrDefault();
                    var oValidAl2 = (from d in db.alumno
                                    join acu in db.acudiente on d.Acu_ID equals acu.Acu_ID
                                    where acu.Acu_ID == id
                                    select d.Alum_Apel).FirstOrDefault();
                    var identi = (from d in db.alumno
                                  join acu in db.acudiente on d.Acu_ID equals acu.Acu_ID
                                  where acu.Acu_ID == id
                                  select d.Alum_Doc).FirstOrDefault();
                    if (oValidAl == null|| oValidAl2==null || identi==null)
                    {
                        return Json(new { Success = false, msg = "Error al precargar datos, por favor revise si este acudiente tiene un alumno asignado." });
                    }
                    
                    return Json(new
                    {
                        Success = true,
                        nombres = oValidAl+" "+oValidAl2,
                        documento = identi
  
                    });
                }
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }


        /*------Administración de los superUsuarios (Admins) ------*/

        public ActionResult Admins()
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
        public long RetornaID()
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
            {
                    string correo = Session["correo"].ToString();
                    var oValidAl = (from d in db.administrador
                                    where d.Adm_Email == correo
                                    select d.Adm_ID).FirstOrDefault();
                    if (oValidAl != 0)
                    {
                        return oValidAl;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else
            {
                return 0;
            }
        }
        [HttpPost]
        public ActionResult JsonAdministradores()
        {
            List<TableAdminAdministradoresVM> lst = new List<TableAdminAdministradoresVM>();
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
                IQueryable<TableAdminAdministradoresVM> query = (from Est in db.administrador
                                                            select new TableAdminAdministradoresVM
                                                            {
                                                                Adm_ID = Est.Adm_ID,
                                                                Adm_Doc = Est.Adm_Doc,
                                                                Adm_Nom = Est.Adm_Nom,
                                                                Adm_Apel = Est.Adm_Apel,
                                                                Adm_Tel = Est.Adm_Tel,
                                                                Adm_Email = Est.Adm_Email,
                                                                Adm_Status = Est.Adm_Status
                                                            });
                if (searchValue != "")
                    query = query.Where(Est => Est.Adm_Doc.Contains(searchValue));
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    query = query.OrderBy(sortColumn + " " + sortColumnDir);
                }
                if (!sortColumn.Equals("Adm_Status2") && !sortColumn.Equals("Acciones"))
                {
                    recordsTotal = query.Count();
                }
                else
                {
                    ViewBag.Error = "No se puede ordenar por este tipo de elemento";
                }
                if (recordsTotal != 0)
                {
                    lst = query.Skip(skip).Take(pageSize).ToList();
                }
                foreach (TableAdminAdministradoresVM buscador in lst)
                {
                    if (buscador.Adm_Status == 1)
                    {
                        buscador.Adm_Status2 = "<span class=\"badge badge-success\">Activo</span>";
                    }
                    else
                    {
                        buscador.Adm_Status2 = "<span class=\"badge badge-danger\">Inactivo</span>";
                    }
                    long idi = RetornaID();
                    if (idi == buscador.Adm_ID)
                    {
                        buscador.Acciones = "<button class=\"btn btn-primary btn-sm \" disabled=\"disabled\" title=\"Editar\"><i class=\"fas fa-pencil-alt\" aria-hidden=\"true\"></i></button> ";
                        buscador.Acciones += "<button class=\"btn btn-danger btn-sm \" disabled=\"disabled\" title=\"Eliminar\"><i class=\"far fa-trash-alt\" aria-hidden=\"true\"></i></button> ";
                    }else{
                        buscador.Acciones = "<button class=\"btn btn-primary btn-sm \" onclick=\"fntEditAdm(" + buscador.Adm_ID + ")\" title=\"Editar\"><i class=\"fas fa-pencil-alt\" aria-hidden=\"true\"></i></button> ";
                        buscador.Acciones += "<button class=\"btn btn-danger btn-sm \" onclick=\"fntDelAdm(" + buscador.Adm_ID + ")\" title=\"Eliminar\"><i class=\"far fa-trash-alt\" aria-hidden=\"true\"></i></button> ";
                   } 
                }

            }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });
        }
        public bool ElimAd(int id)
        {
            using (sgaEntities db = new sgaEntities())
            {
                try
                {
                    var query = (from p in db.administrador
                                 where p.Adm_ID == id
                                 select p).Single();
                    db.administrador.Remove(query);
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
        public ActionResult EliminarAdm(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                if (id < 0)
                {
                    return Json(new { Success = false, msg = "Por favor revise el ID a eliminar" });
                }
                if (ElimAd(id))
                {
                    return Json(new { Success = true, msg = "El administrador se ha eliminado con éxito" });
                }
                else
                {
                    return Json(new { Success = false, msg = "No se pudo eliminar" });
                }
            }
            return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
        }
        [HttpPost]
        public ActionResult InsertAdm(string strIdentificacion, string strNombre, string strApellido, string strEmail, string Telefono, string strPassword, int StatusRedy)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
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
                    if (oValidEm != null || oValidEm3 != null || oValidEm4 != null)
                    {
                        return Json(new { Success = false, msg = "El correo electronico ya existe en nuestra base de datos." });
                    }
                    try
                    {
                        administrador estu = new administrador();
                        estu.Adm_Doc = strIdentificacion;
                        estu.Adm_Nom = strNombre;
                        estu.Adm_Apel = strApellido;
                        estu.Adm_Tel = Telefono;
                        estu.Adm_Email = strEmail;
                        estu.Adm_Status = StatusRedy;
                        strPassword = help.GetSHA256(strPassword);
                        estu.Adm_Passw = strPassword;
                        db.administrador.Add(estu);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        e.InnerException.ToString();
                        return Json(new { Success = false, msg = "No se pudo ingresar en la BD motivo: " + e.InnerException.Message });
                    }
                }
                return Json(new { Success = true, msg = "Administrador almacenado con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult SelAdm(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {

                    var oValidAl = (from d in db.administrador
                                    where d.Adm_ID == id
                                    select d).FirstOrDefault();
                    if (oValidAl == null)
                    {
                        return Json(new { Success = false, msg = "Error al precargar datos." });
                    }

                    return Json(new
                    {
                        Success = true,
                        idpersona = oValidAl.Adm_ID,
                        identificacion = oValidAl.Adm_Doc,
                        nombres = oValidAl.Adm_Nom,
                        apellidos = oValidAl.Adm_Apel,
                        telefono = oValidAl.Adm_Tel,
                        email_user = oValidAl.Adm_Email
                    });
                }
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult EditAdm(int id, string strIdentificacion, string strNombre, string strApellido, string strEmail, string Telefono, string strPassword, int StatusRedy)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {

                    var oValidEm2 = (from d in db.docente
                                     where d.Doc_Email == strEmail
                                     select d).FirstOrDefault();
                    var oValidEm3 = (from d in db.alumno
                                     where d.Alum_Email == strEmail
                                     select d).FirstOrDefault();
                    var oValidEm4 = (from d in db.acudiente
                                     where d.Acu_Email == strEmail
                                     select d).FirstOrDefault();
                    if (oValidEm2 != null || oValidEm3 != null || oValidEm4 != null)
                    {
                        return Json(new { Success = false, msg = "El correo electronico ya existe en nuestra base de datos." });
                    }

                    try
                    {
                        var std = db.administrador.FirstOrDefault(edit => edit.Adm_ID == id);
                        std.Adm_Doc = strIdentificacion;
                        std.Adm_Nom = strNombre;
                        std.Adm_Apel = strApellido;
                        std.Adm_Tel = Telefono;
                        std.Adm_Email = strEmail;
                        std.Adm_Status = StatusRedy;
                        strPassword = help.GetSHA256(strPassword);
                        std.Adm_Passw = strPassword;
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo actualizar en la BD motivo: " + e.Message });
                    }
                }
                return Json(new { Success = true, msg = "Administrador actualizado con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }

        /*-----Administración de los periodos academicos---*/

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
        [HttpPost]
        public ActionResult JsonPeriodos()
        {
            List<TableAdminPeriodosVM> lst = new List<TableAdminPeriodosVM>();
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
                IQueryable<TableAdminPeriodosVM> query = (from Est in db.periodo
                                                                 select new TableAdminPeriodosVM
                                                                 {
                                                                    Per_ID = Est.Per_ID,
                                                                    Per_Nom = Est.Per_Nom,
                                                                    Per_Ini = Est.Per_Ini,
                                                                    Per_Fin = Est.Per_Fin
                                                                 });
                if (searchValue != "")
                    query = query.Where(Est => Est.Per_Nom.Contains(searchValue));
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    query = query.OrderBy(sortColumn + " " + sortColumnDir);
                }
                if (!sortColumn.Equals("Acciones"))
                {
                    recordsTotal = query.Count();
                }
                else
                {
                    ViewBag.Error = "No se puede ordenar por este tipo de elemento";
                }
                if (recordsTotal != 0)
                {
                    lst = query.Skip(skip).Take(pageSize).ToList();
                }
                foreach (TableAdminPeriodosVM buscador in lst)
                {
                    buscador.Inicio = buscador.Per_Ini.ToString("dd/MM/yyyy");
                    buscador.Final = buscador.Per_Fin.ToString("dd/MM/yyyy");
                    buscador.Acciones = "<button class=\"btn btn-primary btn-sm \" onclick=\"fntEditPer(" + buscador.Per_ID + ")\" title=\"Editar\"><i class=\"fas fa-pencil-alt\" aria-hidden=\"true\"></i></button> ";
                    buscador.Acciones += "<button class=\"btn btn-danger btn-sm \" onclick=\"fntDelPer(" + buscador.Per_ID + ")\" title=\"Eliminar\"><i class=\"far fa-trash-alt\" aria-hidden=\"true\"></i></button> ";
                 
                }
            }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });
        }
        [HttpPost]
        public ActionResult SelPer(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {

                    var oValidAl = (from d in db.periodo
                                    where d.Per_ID == id
                                    select d).FirstOrDefault();
                    if (oValidAl == null)
                    {
                        return Json(new { Success = false, msg = "Error al precargar datos." });
                    }

                    return Json(new
                    {
                        Success = true,
                        idpersona = oValidAl.Per_ID,
                        nombres = oValidAl.Per_Nom,
                        inicio = oValidAl.Per_Ini.ToString("yyyy-MM-dd"),
                        final = oValidAl.Per_Fin.ToString("yyyy-MM-dd")
                    });
                }
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        public bool ElimPe(int id)
        {
            using (sgaEntities db = new sgaEntities())
            {
                try
                {
                    var query = (from p in db.periodo
                                 where p.Per_ID == id
                                 select p).Single();
                    db.periodo.Remove(query);
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
        public ActionResult EliminarPer(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                if (id < 0)
                {
                    return Json(new { Success = false, msg = "Por favor revise el ID a eliminar" });
                }
                if (ElimPe(id))
                {
                    return Json(new { Success = true, msg = "El periodo se ha eliminado con éxito" });
                }
                else
                {
                    return Json(new { Success = false, msg = "No se pudo eliminar" });
                }
            }
            return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
        }
        [HttpPost]
        public ActionResult InsertPer(string strInicio, string strFinal, string strNombre)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
                    var oValidEm = (from d in db.periodo
                                    where d.Per_Nom == strNombre
                                    select d).FirstOrDefault();
                   
                    if (oValidEm != null)
                    {
                        return Json(new { Success = false, msg = "El nombre del periodo ya existe en nuestra base de datos." });
                    }
                    try
                    {
                        periodo estu = new periodo();
                        estu.Per_Nom = strNombre;
                        estu.Per_Ini = DateTime.ParseExact(strInicio, "yyyy-MM-dd", null);
                        estu.Per_Fin = DateTime.ParseExact(strFinal, "yyyy-MM-dd", null);
                        db.periodo.Add(estu);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        e.InnerException.ToString();
                        return Json(new { Success = false, msg = "No se pudo ingresar en la BD motivo: " + e.InnerException.Message });
                    }
                }
                return Json(new { Success = true, msg = "Periodo almacenado con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult EditPer(int id, string strIni, string strFinal, string strNombre)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
                    try
                    {
                        var std = db.periodo.FirstOrDefault(edit => edit.Per_ID == id);
                        std.Per_Nom = strNombre;
                        std.Per_Ini = DateTime.ParseExact(strIni, "yyyy-MM-dd", null);
                        std.Per_Fin = DateTime.ParseExact(strFinal, "yyyy-MM-dd", null);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo actualizar en la BD motivo: " + e.Message });
                    }
                }
                return Json(new { Success = true, msg = "Periodo actualizado con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }

        /*----Administración de las materias---*/
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
        [HttpPost]
        public ActionResult JsonMaterias()
        {
            List<TableAdminMateriasVM> lst = new List<TableAdminMateriasVM>();
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
                IQueryable<TableAdminMateriasVM> query = (from Est in db.materia
                                                                 select new TableAdminMateriasVM
                                                                 {
                                                                     Mat_ID = Est.Mat_ID,
                                                                     Mat_Nom = Est.Mat_Nom,
                                                                     Mat_Desc = Est.Mat_Desc
                                                                 });
                if (searchValue != "")
                    query = query.Where(Est => Est.Mat_Nom.Contains(searchValue));
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    query = query.OrderBy(sortColumn + " " + sortColumnDir);
                }
                if (!sortColumn.Equals("Acciones"))
                {
                    recordsTotal = query.Count();
                }
                else
                {
                    ViewBag.Error = "No se puede ordenar por este tipo de elemento";
                }
                if (recordsTotal != 0)
                {
                    lst = query.Skip(skip).Take(pageSize).ToList();
                }
                foreach (TableAdminMateriasVM buscador in lst)
                {
                    buscador.Acciones = "<button class=\"btn btn-primary btn-sm \" onclick=\"fntEditMat(" + buscador.Mat_ID + ")\" title=\"Editar\"><i class=\"fas fa-pencil-alt\" aria-hidden=\"true\"></i></button> ";
                    buscador.Acciones += "<button class=\"btn btn-danger btn-sm \" onclick=\"fntDelMat(" + buscador.Mat_ID + ")\" title=\"Eliminar\"><i class=\"far fa-trash-alt\" aria-hidden=\"true\"></i></button> ";
                }

            }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });
        }
        public bool ElimMa(int id)
        {
            using (sgaEntities db = new sgaEntities())
            {
                try
                {
                    var query = (from p in db.materia
                                 where p.Mat_ID == id
                                 select p).Single();
                    db.materia.Remove(query);
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
        public ActionResult EliminarMat(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                if (id < 0)
                {
                    return Json(new { Success = false, msg = "Por favor revise el ID a eliminar" });
                }
                if (ElimMa(id))
                {
                    return Json(new { Success = true, msg = "La asignatura se ha eliminado con éxito" });
                }
                else
                {
                    return Json(new { Success = false, msg = "No se pudo eliminar" });
                }
            }
            return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
        }
        [HttpPost]
        public ActionResult InsertMat(string strNombre, string descripcion)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
                  
                    try
                    {
                        materia estu = new materia();
                        estu.Mat_Nom = strNombre;
                        estu.Mat_Desc = descripcion;
                        db.materia.Add(estu);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        e.InnerException.ToString();
                        return Json(new { Success = false, msg = "No se pudo ingresar en la BD motivo: " + e.InnerException.Message });
                    }
                }
                return Json(new { Success = true, msg = "Asignatura almacenada con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult SelMat(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {

                    var oValidAl = (from d in db.materia
                                    where d.Mat_ID == id
                                    select d).FirstOrDefault();
                    if (oValidAl == null)
                    {
                        return Json(new { Success = false, msg = "Error al precargar datos." });
                    }

                    return Json(new
                    {
                        Success = true,
                        idpersona = oValidAl.Mat_ID,
                        nombre = oValidAl.Mat_Nom,
                        descripcion = oValidAl.Mat_Desc
                    });
                }
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult EditMat(int id,string strNombre, string descripcion)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {

                  

                    try
                    {
                        var std = db.materia.FirstOrDefault(edit => edit.Mat_ID == id);
                      
                        std.Mat_Nom = strNombre;
                        std.Mat_Desc = descripcion;
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo actualizar en la BD motivo: " + e.Message });
                    }
                }
                return Json(new { Success = true, msg = "Asignatura actualizada con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        /*--------------Administracion de cursos-----------------*/
       
        public ActionResult Clases()
        {
            bool edit=false;
            ViewBag.Entra = "N";
            List<CarouselAdminClasesVM> lista = new List<CarouselAdminClasesVM>();
            using (sgaEntities db = new sgaEntities())
            {
                IQueryable<CarouselAdminClasesVM> query = (from curs in db.curso
                                                           join clas in db.clase on curs.Curs_ID equals clas.Curs_ID
                                                           join per in db.periodo on clas.Per_ID equals per.Per_ID
                                                           where fecha >= per.Per_Ini && fecha <= per.Per_Fin
                                                           select new CarouselAdminClasesVM
                                                           {
                                                               Curs_Nom = curs.Curs_Nom,
                                                               Clas_Capa = clas.Clas_Capa,
                                                               Per_Ini = per.Per_Ini,
                                                               Per_Fin = per.Per_Fin,
                                                               Clas_ID = clas.Clas_ID
                                                           });
                
                lista = query.ToList();
                var consulta = (from per in db.periodo
                                where fecha >= per.Per_Ini && fecha <= per.Per_Fin
                                select per).SingleOrDefault();
                if (consulta != null)
                {
                    edit = true;
                }
            }

            if (edit)
            {
                ViewBag.Entra = "S";
            }
            ViewBag.Cursos = lista;

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
        public ActionResult VerClase(int id)
        {
            ViewBag.ID = id;
            bool estado = false;
            try
            {
                using (sgaEntities bdx = new sgaEntities())
                {

                    var query = (from curs in bdx.curso
                                 where curs.Curs_ID == id
                                 select curs).FirstOrDefault();
                    
                    if (query != null&&query.Curs_Nom!="")
                    {
                        estado = true;
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return Clases();
            }
            if (estado==true)
              {
                return View();
                }
            else
            {
                return RedirectToAction("Clases/");
            }     
        }
        [HttpPost]
        public ActionResult InsertClas(string strNombre, int capacidad,long period)
        {
           
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {

                    try
                    {
                        var peri = (from perix in db.periodo
                                        where perix.Per_ID == (period)
                                        select perix).FirstOrDefault();
                        var cursi = (from cu in db.curso
                                     where cu.Curs_Nom.Equals(strNombre)
                                     select cu).FirstOrDefault();
                        if (peri!=null&&(DateTime.Now>=(peri.Per_Ini)&&DateTime.Now<=(peri.Per_Fin))&&cursi==null)
                        {
                        curso curs = new curso();
                        curs.Curs_Nom = strNombre;
                        db.curso.Add(curs);
                        db.SaveChanges();
                        }
                        else
                        {
                            return Json(new { Success = false, msg = "El periodo no existe o probablemente no esta dentro del limite o puede que este curso ya exista." });
                        }
                    }
                    catch (Exception e)
                    {
                        e.InnerException.ToString();
                        return Json(new { Success = false, msg = "No se pudo ingresar en la BD motivo: " + e.InnerException.Message });
                    }
                }
                using(sgaEntities bd = new sgaEntities())
                {
                    try
                    {
                        var proc = bd.InsertarClase(capacidad, period);
                    }catch(Exception e)
                    {
                        e.InnerException.ToString();
                        return Json(new { Success = false, msg = "No se pudo ingresar en la BD motivo: " + e.InnerException.Message });
                    }
                }
                return Json(new { Success = true, msg = "Curso almacenado con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult ElimClas(long id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                if (id < 0)
                {
                    return Json(new { Success = false, msg = "Por favor revise el ID a eliminar" });
                }
                if (ElimCurs(id))
                {
                    return Json(new { Success = true, msg = "El curso se ha eliminado con éxito" });
                }
                else
                {
                    return Json(new { Success = false, msg = "No se pudo eliminar" });
                }
            }
            return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
        }
        public bool ElimCurs(long id)
        {
            using (sgaEntities db = new sgaEntities())
            {
                try
                {
                    var query = (from p in db.clase
                                 where p.Curs_ID == id
                                 select p).Single();
                    db.clase.Remove(query);
                    db.SaveChanges();
                    var transact = (from c in db.curso
                                    where c.Curs_ID == id
                                    select c).Single();
                    db.curso.Remove(transact);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
        public ActionResult EstudiantesClase(int id)
        {
            ViewBag.ID = id;
            bool estado = false;
            try
            {
                using (sgaEntities bdx = new sgaEntities())
                {
                    var consul = (from alc in bdx.alumno_clase
                                  where alc.Clas_ID == id
                                  select alc).Count();
                    var capa = (from xd in bdx.clase
                                where xd.Curs_ID == id
                                select xd).FirstOrDefault();
                    var query = (from curs in bdx.curso
                                 where curs.Curs_ID == id
                                 select curs).FirstOrDefault();
                    if (capa.Clas_Capa == consul)
                    {
                        ViewBag.Entra = "N";
                    }
                    if (query != null && query.Curs_Nom != "")
                    {
                        estado = true;
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return Clases();
            }
            if (estado == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Clases/");
            }
        }
        [HttpPost]
        public ActionResult JsonEstudiantesClase(int id)
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
                IQueryable<TableAdminEstudiantesVM> query = (from Est in db.alumno
                                                             join al_cl in db.alumno_clase on Est.Alum_ID equals al_cl.Alum_ID
                                                             join clas in db.clase on al_cl.Clas_ID equals clas.Clas_ID
                                                             join curs in db.curso on clas.Curs_ID equals curs.Curs_ID
                                                             join per in db.periodo on clas.Per_ID equals per.Per_ID
                                                             where curs.Curs_ID==id
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
                if (!sortColumn.Equals("Alum_Status2") && !sortColumn.Equals("Acciones"))
                {
                    recordsTotal = query.Count();
                }
                else
                {
                    ViewBag.Error = "No se puede ordenar por este tipo de elemento";
                }
                if (recordsTotal != 0)
                {
                    lst = query.Skip(skip).Take(pageSize).ToList();
                }
                foreach (TableAdminEstudiantesVM buscador in lst)
                {
                    if (buscador.Alum_Status == 1)
                    {
                        buscador.Alum_Status2 = "<span class=\"badge badge-success\">Activo</span>";
                    }
                    else
                    {
                        buscador.Alum_Status2 = "<span class=\"badge badge-danger\">Inactivo</span>";
                    }
                  
                    buscador.Acciones += "<button class=\"btn btn-danger btn-sm btnElimEst\" onclick=\"fntQuitarEst(" + buscador.Alum_ID + ")\" title=\"Desvincular\"><i class=\"fas fa-unlink\" aria-hidden=\"true\"></i></button> ";
                }

            }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });   
        }
        [HttpPost]
        public ActionResult InsertEstClas(string strIdentificacion,int claseID)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
                    var oValid = (from d in db.alumno
                                  where d.Alum_Doc == strIdentificacion
                                  select d).FirstOrDefault();

                    var oValidP = (from p in db.periodo
                                   where fecha >= p.Per_Ini && fecha <= p.Per_Fin
                                   select p).FirstOrDefault();
                    var consulta = (from est in db.alumno_clase
                                    where est.Alum_ID == oValid.Alum_ID
                                    select est).SingleOrDefault();
                    if (consulta != null)
                    {
                        return Json(new { Success = false, msg = "No se puede matricular dos veces al mismo estudiante." });
                    }
                    if (oValid == null)
                    {
                        return Json(new { Success = false, msg = "El documento de dicho estudiante no existe, verifique por favor." });
                    }else if (oValidP == null){
                        return Json(new { Success = false, msg = "No se encuentra en las fechas especificas para ese periodo" });
                    }
                    try
                    {
                        alumno_clase alc = new alumno_clase();
                        alc.Alum_ID = oValid.Alum_ID;
                        alc.Clas_ID = claseID;
                        alc.Per_ID = oValidP.Per_ID;
                        db.alumno_clase.Add(alc);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo ingresar en la BD motivo: " + e.Message });
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
        public ActionResult EliminarEstClas(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                if (id < 0)
                {
                    return Json(new { Success = false, msg = "Por favor revise el ID a eliminar" });
                }
                if (ElimEClas(id))
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
        public bool ElimEClas(int id)
        {
            using (sgaEntities db = new sgaEntities())
            {
                try
                {
                    var query = (from p in db.alumno_clase
                                 where p.Alum_ID == id
                                 select p).Single();
                    db.alumno_clase.Remove(query);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
        public ActionResult DocentesClase(int id)
        {
            ViewBag.ID = id;
            bool estado = false;
            try
            {
                using (sgaEntities bdx = new sgaEntities())
                {

                    var query = (from curs in bdx.curso
                                 where curs.Curs_ID == id
                                 select curs).FirstOrDefault();
                    if (query != null && query.Curs_Nom != "")
                    {
                        estado = true;
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return Clases();
            }
            if (estado == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Clases/");
            }
        }
        [HttpPost]
        public ActionResult JsonDocentesClase(int id)
        {
            List<TableAdminDocentesVM> lst = new List<TableAdminDocentesVM>();
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
                IQueryable<TableAdminDocentesVM> query = (from Est in db.docente
                                                          join doc_cl in db.docente_clase on Est.Doc_ID equals doc_cl.Doc_ID
                                                          join clas in db.clase on doc_cl.Clas_ID equals clas.Clas_ID
                                                          join curs in db.curso on clas.Curs_ID equals curs.Curs_ID
                                                          join per in db.periodo on clas.Per_ID equals per.Per_ID
                                                          where curs.Curs_ID == id
                                                          select new TableAdminDocentesVM
                                                          {
                                                              Doc_ID = Est.Doc_ID,
                                                              Doc_Doc = Est.Doc_Doc,
                                                              Doc_Nom = Est.Doc_Nom,
                                                              Doc_Apel = Est.Doc_Apel,
                                                              Doc_Tel = Est.Doc_Tel,
                                                              Doc_Email = Est.Doc_Email,
                                                              Doc_Status = Est.Doc_Status
                                                          });
                if (searchValue != "")
                    query = query.Where(Est => Est.Doc_Doc.Contains(searchValue));
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    query = query.OrderBy(sortColumn + " " + sortColumnDir);
                }
                if (!sortColumn.Equals("Doc_Status2") && !sortColumn.Equals("Acciones"))
                {
                    recordsTotal = query.Count();
                }
                else
                {
                    ViewBag.Error = "No se puede ordenar por este tipo de elemento";
                }
                if (recordsTotal != 0)
                {
                    lst = query.Skip(skip).Take(pageSize).ToList();
                }
                foreach (TableAdminDocentesVM buscador in lst)
                {
                    if (buscador.Doc_Status == 1)
                    {
                        buscador.Doc_Status2 = "<span class=\"badge badge-success\">Activo</span>";
                    }
                    else
                    {
                        buscador.Doc_Status2 = "<span class=\"badge badge-danger\">Inactivo</span>";
                    }
                  
                    buscador.Acciones += "<button class=\"btn btn-danger btn-sm \" onclick=\"fntUnlinkDoc(" + buscador.Doc_ID + ")\" title=\"Desvincular\"><i class=\"fas fa-unlink\" aria-hidden=\"true\"></i></button> "; 
                }
            }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });
        }
        [HttpPost]
        public ActionResult EliminarDocClas(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                if (id < 0)
                {
                    return Json(new { Success = false, msg = "Por favor revise el ID a eliminar" });
                }
                if (ElimDoclas(id))
                {
                    return Json(new { Success = true, msg = "El docente se ha eliminado con éxito" });
                }
                else
                {
                    return Json(new { Success = false, msg = "No se pudo eliminar" });
                }
            }
            return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
        }
        [HttpPost]
        public ActionResult InsertDocClas(string strIdentificacion, int claseID)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
                    var oValid = (from d in db.docente
                                  where d.Doc_Doc == strIdentificacion
                                  select d).FirstOrDefault();

                    var oValidP = (from p in db.periodo
                                   where fecha >= p.Per_Ini && fecha <= p.Per_Fin
                                   select p).FirstOrDefault();
                    var consulta = (from est in db.docente_clase
                                    where est.Doc_ID == oValid.Doc_ID
                                    select est).SingleOrDefault();
                    if (consulta != null)
                    {
                        return Json(new { Success = false, msg = "No se puede matricular dos veces al mismo docente." });
                    }
                    if (oValid == null)
                    {
                        return Json(new { Success = false, msg = "El documento de dicho docente no existe, verifique por favor." });
                    }
                    else if (oValidP == null)
                    {
                        return Json(new { Success = false, msg = "No se encuentra en las fechas especificas para ese periodo" });
                    }
                    try
                    {
                        docente_clase alc = new docente_clase();
                        alc.Doc_ID = oValid.Doc_ID;
                        alc.Clas_ID = claseID;
                        db.docente_clase.Add(alc);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo ingresar en la BD motivo: " + e.Message });
                    }
                }
                return Json(new { Success = true, msg = "Docente almacenado con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        public bool ElimDoclas(int id)
        {
            using (sgaEntities db = new sgaEntities())
            {
                try
                {
                    var query = (from p in db.docente_clase
                                 where p.Doc_ID == id
                                 select p).Single();
                    db.docente_clase.Remove(query);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
        public ActionResult MateriasClase(int id)
        {
            ViewBag.ID = id;
            bool estado = false;
            try
            {
                using (sgaEntities bdx = new sgaEntities())
                {

                    var query = (from curs in bdx.curso
                                 where curs.Curs_ID == id
                                 select curs).FirstOrDefault();
                    if (query != null && query.Curs_Nom != "")
                    {
                        estado = true;
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return Clases();
            }
            if (estado == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Clases/");
            }
        }
        [HttpPost]
        public ActionResult JsonMateriasClase(int id)
        {
            List<TableAdminMateriasVM> lst = new List<TableAdminMateriasVM>();
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
                IQueryable<TableAdminMateriasVM> query = (from Est in db.materia
                                                          join mat_cl in db.materia_clase on Est.Mat_ID equals mat_cl.Mat_ID
                                                          join clas in db.clase on mat_cl.Clas_ID equals clas.Clas_ID
                                                          join curs in db.curso on clas.Curs_ID equals curs.Curs_ID
                                                          join per in db.periodo on clas.Per_ID equals per.Per_ID
                                                          where curs.Curs_ID == id
                                                          select new TableAdminMateriasVM
                                                          {
                                                              Mat_ID = Est.Mat_ID,
                                                              Mat_Nom = Est.Mat_Nom,
                                                              Mat_Desc = Est.Mat_Desc
                                                          });
                if (searchValue != "")
                    query = query.Where(Est => Est.Mat_Nom.Contains(searchValue));
                //Sorting    
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    query = query.OrderBy(sortColumn + " " + sortColumnDir);
                }
                if (!sortColumn.Equals("Acciones"))
                {
                    recordsTotal = query.Count();
                }
                else
                {
                    ViewBag.Error = "No se puede ordenar por este tipo de elemento";
                }
                if (recordsTotal != 0)
                {
                    lst = query.Skip(skip).Take(pageSize).ToList();
                }
                foreach (TableAdminMateriasVM buscador in lst)
                {
                    buscador.Acciones = "<button class=\"btn btn-success btn-sm \" onclick=\"fntEditDoc(" + buscador.Mat_ID + ")\" title=\"Asignar docente\"><i class=\"fas fa-user-edit\" aria-hidden=\"true\"></i></button> ";
                    buscador.Acciones += "<button class=\"btn btn-primary btn-sm \" onclick=\"fntDocMat(" + buscador.Mat_ID +","+ id + ")\" title=\"Ver docente a cargo\"><i class=\"fas fa-eye\" aria-hidden=\"true\"></i></button> ";
                    buscador.Acciones += "<button class=\"btn btn-danger btn-sm \" onclick=\"fntUnlinkMat(" + buscador.Mat_ID + ")\" title=\"Desvincular\"><i class=\"fas fa-unlink\" aria-hidden=\"true\"></i></button> ";
                }

            }
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });
        }
        [HttpPost]
        public ActionResult SelMatX(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {

                    var oValidAl = (from d in db.materia_clase
                                    where d.Mat_ID == id
                                    select d).FirstOrDefault();
                    var oValidx = (from d in db.docente
                                   join mat in db.materia_clase on d.Doc_ID equals mat.Doc_ID
                                   select d).FirstOrDefault();
                    if (oValidAl == null)
                    {
                        return Json(new { Success = false, msg = "Error al precargar datos." });
                    }

                    return Json(new
                    {
                        Success = true,
                        idpersona = oValidAl.Mat_ID,
                        docente = oValidx.Doc_Doc
                    });
                }
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult EditDocMat(long strIdentificacion,long id,long curso)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
                    try
                    {
                        var ValidDoc = (from d in db.docente
                                        where d.Doc_Doc==strIdentificacion.ToString()
                                        select d).FirstOrDefault();
                        var EstaClase = (from d in db.docente_clase
                                         join doc in db.docente on d.Doc_ID equals doc.Doc_ID
                                         where d.Clas_ID == curso && doc.Doc_Doc == strIdentificacion.ToString()
                                         select d).FirstOrDefault();
                        if (EstaClase == null) { return Json(new { Success = false, msg = "Ese docente no esta asignado al curso" }); }
                        if(ValidDoc==null){return Json(new { Success = false, msg = "Ese docente no existe" }); }
                        var Repetido = (from d in db.materia_clase
                                        where d.Mat_ID != id && d.Clas_ID == curso && d.Doc_ID == ValidDoc.Doc_ID
                                        select d).FirstOrDefault();
                        if(Repetido!=null) { return Json(new { Success = false, msg = "Ese docente ya esta asignado a esta materia" }); }
                        var std = (from d in db.materia_clase
                                   where d.Clas_ID == curso && d.Mat_ID == id
                                   select d).FirstOrDefault();
                        if (std == null) { return Json(new { Success = false, msg = "Error durante la consulta" }); }
                        std.Doc_ID = ValidDoc.Doc_ID;
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo actualizar en la BD motivo: " + e.Message });
                    }
                }
                return Json(new { Success = true, msg = "Asignatura actualizada con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult EliminarMatClas(int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                if (id < 0)
                {
                    return Json(new { Success = false, msg = "Por favor revise el ID a eliminar" });
                }
                if (ElimMaclas(id))
                {
                    return Json(new { Success = true, msg = "El docente se ha eliminado con éxito" });
                }
                else
                {
                    return Json(new { Success = false, msg = "No se pudo eliminar" });
                }
            }
            return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
        }
        public bool ElimMaclas(int id)
        {
            using (sgaEntities db = new sgaEntities())
            {
                try
                {
                    var query = (from p in db.materia_clase
                                 where p.Mat_ID == id
                                 select p).Single();
                    db.materia_clase.Remove(query);
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
        public ActionResult ConsulDoceMat(int id,int curso)
        {
            using(sgaEntities db = new sgaEntities())
            {
                var query = (from d in db.docente
                             join doc_cl in db.docente_clase on d.Doc_ID equals doc_cl.Doc_ID
                             join mat_cl in db.materia_clase on doc_cl.Doc_ID equals mat_cl.Doc_ID
                             where mat_cl.Clas_ID == curso && mat_cl.Mat_ID == id
                             select d).FirstOrDefault();
                if (query == null)
                {
                    return Json(new { Success = false, msg = "Docente sin asignar" });
                }
                return Json(new { Success = true, msg = "Nombres y apellidos: "+query.Doc_Nom+" "+query.Doc_Apel+" Doc. de Identidad: "+query.Doc_Doc });
            }
           
        }
        [HttpPost]
        public ActionResult InsertMatClas(int docente,int materia,int curso)
        {

            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
                    var oValid = (from d in db.materia
                                  where d.Mat_ID==materia
                                  select d).FirstOrDefault();

                    var oValidP = (from p in db.periodo
                                   where fecha >= p.Per_Ini && fecha <= p.Per_Fin
                                   select p).FirstOrDefault();
                  
                    var xd = (from doc in db.docente
                              where doc.Doc_Doc == docente.ToString()
                              select doc).FirstOrDefault();
                    if (xd == null) return Json(new { Success = false, msg = "El docente no existe." });
                    var consulta = (from mat in db.materia_clase
                                    where mat.Mat_ID == materia || mat.Doc_ID == xd.Doc_ID && mat.Clas_ID == curso
                                    select mat).SingleOrDefault();
                    if (consulta != null)
                    {
                        return Json(new { Success = false, msg = "No se puede matricular dos veces la misma asignatura o ya ese docente esta asignado." });
                    }
                    if (oValid == null)
                    {
                        return Json(new { Success = false, msg = "La asignatura no existe." });
                    }
                    else if (oValidP == null)
                    {
                        return Json(new { Success = false, msg = "No se encuentra en las fechas especificas para ese periodo" });
                    }
                    try
                    {
                        materia_clase alc = new materia_clase();
                        alc.Clas_ID = curso;
                        alc.Doc_ID = xd.Doc_ID;
                        alc.Mat_ID = oValid.Mat_ID;
                        db.materia_clase.Add(alc);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo ingresar en la BD motivo: " + e.Message });
                    }
                }
                return Json(new { Success = true, msg = "Asignatura almacenada con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult EditCapa(int capa,int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
                    try
                    {
                        var Valid = (from d in db.clase
                                        where d.Curs_ID == id
                                        select d).FirstOrDefault();
                       
                        if (Valid == null) { return Json(new { Success = false, msg = "Dicho curso no existe" }); }
                        
                        var std = (from d in db.clase
                                   where d.Clas_ID == id 
                                   select d).FirstOrDefault();
                        if (std == null) { return Json(new { Success = false, msg = "Error durante la consulta" }); }
                        std.Clas_Capa = capa;
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo actualizar en la BD motivo: " + e.Message });
                    }
                }
                return Json(new { Success = true, msg = "Capacidad actualizada con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult EditNombre(string name,int id)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
                    try
                    {
                        var Valid = (from d in db.clase
                                     where d.Curs_ID == id
                                     select d).FirstOrDefault();

                        if (Valid == null) { return Json(new { Success = false, msg = "Dicho curso no existe" }); }

                        var std = (from d in db.curso
                                   where d.Curs_ID == id
                                   select d).FirstOrDefault();
                        if (std == null) { return Json(new { Success = false, msg = "Error durante la consulta" }); }
                        std.Curs_Nom = name;
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo actualizar en la BD motivo: " + e.Message });
                    }
                }
                return Json(new { Success = true, msg = "Capacidad actualizada con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        public ActionResult NotasAlumno(int curso,int id)
        {
           
            ViewBag.ID = id;
            ViewBag.Curso = curso;
            bool estado = false;
            try
            {
                using (sgaEntities bdx = new sgaEntities())
                {

                    var query = (from curs in bdx.curso
                                 where curs.Curs_ID == curso
                                 select curs).FirstOrDefault();
                    var query2 = (from al in bdx.alumno
                                  where al.Alum_Doc == id.ToString()
                                  select al).FirstOrDefault();
                    var query3 = (from clas in bdx.clase
                                  join alc in bdx.alumno_clase on clas.Clas_ID equals alc.Clas_ID
                                  where alc.Alum_ID == id && alc.Clas_ID == curso
                                  select clas).FirstOrDefault();
                    if (query != null && query.Curs_Nom != ""&&query3!=null&&query2!=null&&query2.Alum_Nom!="")
                    {
                        estado = true;
                    }
                   
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return Clases();
            }
            if (estado == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Clases/");
            }
           
        }
        [HttpPost]
        public ActionResult JsonNotasEstudiante(int id,int curso)
        {
            List<ModificaNotasAdmin> lst = new List<ModificaNotasAdmin>();
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
            try
            {
                using (sgaEntities db = new sgaEntities())
                {
                    IQueryable<ModificaNotasAdmin> query = (from cal in db.calificaciones
                                                            join alc in db.alumno_clase on cal.Alcl_ID equals alc.Alcl_ID
                                                            join clas in db.clase on alc.Clas_ID equals clas.Clas_ID
                                                            join per in db.periodo on clas.Per_ID equals per.Per_ID
                                                            join mat in db.materia on cal.Mat_ID equals mat.Mat_ID
                                                            where fecha >= per.Per_Ini && fecha <= per.Per_Fin && clas.Curs_ID == curso
                                                            select new ModificaNotasAdmin
                                                            {
                                                                Mat_ID = mat.Mat_ID,
                                                                Mat_Nom = mat.Mat_Nom,
                                                                Cal_N1 = cal.Cal_N1.Value,
                                                                Cal_N2 = cal.Cal_N2.Value,
                                                                Cal_N3 = cal.Cal_N3.Value,
                                                                Cal_NF = cal.Cal_NF.Value
                                                            });
                    if (searchValue != "")
                        query = query.Where(Est => Est.Mat_Nom.Contains(searchValue));
                    //Sorting    
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        query = query.OrderBy(sortColumn + " " + sortColumnDir);
                    }
                    if (!sortColumn.Equals("Acciones"))
                    {
                        recordsTotal = query.Count();
                    }
                    else
                    {
                        ViewBag.Error = "No se puede ordenar por este tipo de elemento";
                    }
                    if (recordsTotal != 0)
                    {
                        lst = query.Skip(skip).Take(pageSize).ToList();
                    }
                    foreach (ModificaNotasAdmin buscador in lst)
                    {
                        buscador.Acciones = "<button class=\"btn btn-info btn-sm \" onclick=\"fntEditNot(" + buscador.Mat_ID + ")\" title=\"Editar notas\"><i class=\"fas fa-tools\" aria-hidden=\"true\"></i></button> ";
                    }

                }
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });
            }
            catch (Exception e)
            {
                return Json(new { error=e });
            }
          
        }
        [HttpPost]
        public ActionResult SelNot(int id,int curso)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {

                    var oValidAl = (from d in db.calificaciones
                                    join alc in db.alumno_clase on d.Alcl_ID equals alc.Alcl_ID
                                    join clas in db.clase on alc.Clas_ID equals clas.Clas_ID
                                    where d.Mat_ID == id && clas.Clas_ID==curso
                                    select d).FirstOrDefault();
                    if (oValidAl == null)
                    {
                        return Json(new { Success = false, msg = "Error al precargar datos." });
                    }

                    return Json(new
                    {
                        Success = true,
                        idcal=oValidAl.Cal_ID,
                        none = oValidAl.Cal_N1,
                        ntwo = oValidAl.Cal_N2,
                        ntree = oValidAl.Cal_N3
                    });
                }
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
        [HttpPost]
        public ActionResult EditNot(int id,double not1,double not2,double not3)
        {
            if (Session["User"] != null && Session["Rol"].Equals("Administrador"))
            {
                using (sgaEntities db = new sgaEntities())
                {
                    if(Double.IsNaN(not1)|| Double.IsNaN(not2)||Double.IsNaN(not3)) return Json(new { Success = false, msg = "No se pudo actualizar en la BD motivo: Valores de tipo NaN" });
                    try
                    {
                        var std = db.calificaciones.FirstOrDefault(edit => edit.Cal_ID == id);
                        std.Cal_N1 = not1;
                        std.Cal_N2 = not2;
                        std.Cal_N3 = not3;
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return Json(new { Success = false, msg = "No se pudo actualizar en la BD motivo: " + e.Message });
                    }
                }
                return Json(new { Success = true, msg = "Notas actualizadas con éxito. " });
            }
            else
            {
                return Json(new { Success = false, msg = "No posee los permisos suficientes para dicha acción" });
            }
        }
    }
}