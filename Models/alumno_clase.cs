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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public alumno_clase()
        {
            this.calificaciones = new HashSet<calificaciones>();
        }
    
        public long Alcl_ID { get; set; }
        public long Alum_ID { get; set; }
        public long Per_ID { get; set; }
        public long Clas_ID { get; set; }
    
        public virtual alumno alumno { get; set; }
        public virtual periodo periodo { get; set; }
        public virtual clase clase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<calificaciones> calificaciones { get; set; }
    }
}
