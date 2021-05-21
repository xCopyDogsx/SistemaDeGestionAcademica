using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.ViewModels
{
    public class TableAdminPeriodosVM
    {
        public long Per_ID { get; set; }
        public string Per_Nom { get; set; }
        public System.DateTime Per_Ini { get; set; }
        public System.DateTime Per_Fin { get; set; }
        public string Inicio { get; set; }
        public string Final { get; set; }
        public string Acciones { get; set; }
    }
}