using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.ViewModels
{
    public class TableAdminDocentesVM
    {
        public long Doc_ID { get; set; }
        public string Doc_Doc { get; set; }
        public string Doc_Nom { get; set; }
        public string Doc_Apel { get; set; }
        public string Doc_Tel { get; set; }
        public string Doc_Email { get; set; }
        public string Doc_Passw { get; set; }
        public int Doc_Status { get; set; }
        public string Doc_Status2 { get; set; }
        public string Acciones { get; set; }
    }
}