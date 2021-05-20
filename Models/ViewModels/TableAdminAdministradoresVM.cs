using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.ViewModels
{
    public class TableAdminAdministradoresVM
    {
        public long Adm_ID { get; set; }
        public string Adm_Doc { get; set; }
        public string Adm_Nom { get; set; }
        public string Adm_Apel { get; set; }
        public string Adm_Tel { get; set; }
        public string Adm_Email { get; set; }
        public string Adm_Passw { get; set; }
        public int Adm_Status { get; set; }
        public string Adm_Status2 { get; set; }
        public string Acciones { get; set; }
    }
}