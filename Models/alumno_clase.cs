//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoFinal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class alumno_clase
    {
        public long Alcl_ID { get; set; }
        public long Alum_ID { get; set; }
        public long Per_ID { get; set; }
        public long Clas_ID { get; set; }
        public long Cal_ID { get; set; }
    
        public virtual alumno alumno { get; set; }
        public virtual calificaciones calificaciones { get; set; }
        public virtual periodo periodo { get; set; }
        public virtual clase clase { get; set; }
    }
}