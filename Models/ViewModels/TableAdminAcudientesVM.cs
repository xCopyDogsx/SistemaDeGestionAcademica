using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.ViewModels
{
    public class TableAdminAcudientesVM
    {
        public long Acu_ID { get; set; }
        public string Acu_Doc { get; set; }
        public string Acu_Nom { get; set; }
        public string Acu_Apel { get; set; }
        public string Acu_Tel { get; set; }
        public string Acu_Email { get; set; }
        public string Acu_Passw { get; set; }
        public System.DateTime Acu_FecReg { get; set; }
        public int Acu_Status { get; set; }
        public string Acu_Status2 { get; set; }
        public string Acciones { get; set; }
    }
}