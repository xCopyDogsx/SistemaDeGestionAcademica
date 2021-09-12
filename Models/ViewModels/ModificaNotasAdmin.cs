using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.ViewModels
{
    public class ModificaNotasAdmin
    {
        public long Mat_ID { get; set; }
        public string Mat_Nom { get; set; }
        public double Cal_N1 { get; set; }
        public double Cal_N2 { get; set; }
        public double Cal_N3 { get; set; }
        public double Cal_NF { get; set; }
        public string Acciones { get; set; }
 
    }
}