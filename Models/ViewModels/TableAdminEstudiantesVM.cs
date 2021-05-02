using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.ViewModels
{
    public class TableAdminEstudiantesVM
    {
        public long Alum_ID { get; set; }
        public string Alum_Doc { get; set; }
        public string Alum_Nom { get; set; }
        public string Alum_Apel { get; set; }
        public string Alum_Tel { get; set; }
        public string Alum_Email { get; set; }
        public int Alum_Status { get; set; }
        public string Alum_Status2 { get; set; }
        public string Acciones { get; set; }
    }
}