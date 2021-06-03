using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models.ViewModels
{
    public class CarouselAdminClasesVM
    {
      public string Curs_Nom { get; set; }
      public int Clas_Capa { get; set; }
      public System.DateTime Per_Ini { get; set; }
      public System.DateTime Per_Fin { get; set; }
      public long Clas_ID { get; set; }
    }
}