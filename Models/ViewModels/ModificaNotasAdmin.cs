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
        public ModificaNotasAdmin()
        {
            if (Cal_N1 == null) Cal_N1 = 0;
            if (Cal_N2 == null) Cal_N2 = 0;
            if (Cal_N3 == null) Cal_N3 = 0;
            Cal_NF = (Cal_N1 + Cal_N2 + Cal_N3) / 3;
        }
    }
}