﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MedSys
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ModelMedContainer : DbContext
    {
        public ModelMedContainer()
            : base("name=ModelMedContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Person> PersonSet { get; set; }
        public virtual DbSet<MedCard> MedCardSet { get; set; }
        public virtual DbSet<Record> RecordSet { get; set; }
        public virtual DbSet<TimeForVisit> TimeForVisitSet { get; set; }
        public virtual DbSet<Illness> IllnessSet { get; set; }
        public virtual DbSet<Job> JobSet { get; set; }
        public virtual DbSet<Document> DocumentSet { get; set; }
        public virtual DbSet<Cabinet> CabinetSet { get; set; }
    }
}