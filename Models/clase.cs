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
    
    public partial class clase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public clase()
        {
            this.alumno_clase = new HashSet<alumno_clase>();
            this.horario = new HashSet<horario>();
        }
    
        public long Clas_ID { get; set; }
        public long Mat_ID { get; set; }
        public long Doc_ID { get; set; }
        public int Clas_Capa { get; set; }
        public long Per_ID { get; set; }
        public long Curs_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<alumno_clase> alumno_clase { get; set; }
        public virtual periodo periodo { get; set; }
        public virtual materia materia { get; set; }
        public virtual docente docente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<horario> horario { get; set; }
        public virtual curso curso { get; set; }
    }
}
