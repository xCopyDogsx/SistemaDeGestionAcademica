using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.ViewModels
{
    public class TableAdminMateriasVM
    {
        public long Mat_ID { get; set; }
        public string Mat_Nom { get; set; }
        public string Mat_Desc { get; set; }
        public string Acciones { get; set; }
    }
}