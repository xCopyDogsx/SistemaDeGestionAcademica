﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class sgaEntities : DbContext
    {
        public sgaEntities()
            : base("name=sgaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<docente_clase> docente_clase { get; set; }
        public virtual DbSet<materia_clase> materia_clase { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<acudiente> acudiente { get; set; }
        public virtual DbSet<administrador> administrador { get; set; }
        public virtual DbSet<alumno> alumno { get; set; }
        public virtual DbSet<alumno_clase> alumno_clase { get; set; }
        public virtual DbSet<calificaciones> calificaciones { get; set; }
        public virtual DbSet<clase> clase { get; set; }
        public virtual DbSet<curso> curso { get; set; }
        public virtual DbSet<dias> dias { get; set; }
        public virtual DbSet<docente> docente { get; set; }
        public virtual DbSet<horario> horario { get; set; }
        public virtual DbSet<materia> materia { get; set; }
        public virtual DbSet<periodo> periodo { get; set; }
    
        public virtual int InsertarClase(Nullable<int> classCapa, Nullable<long> perid)
        {
            var classCapaParameter = classCapa.HasValue ?
                new ObjectParameter("ClassCapa", classCapa) :
                new ObjectParameter("ClassCapa", typeof(int));
    
            var peridParameter = perid.HasValue ?
                new ObjectParameter("Perid", perid) :
                new ObjectParameter("Perid", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertarClase", classCapaParameter, peridParameter);
        }
    }
}
