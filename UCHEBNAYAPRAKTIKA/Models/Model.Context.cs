﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UCHEBNAYAPRAKTIKA.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProcurementRegEntities : DbContext
    {
        public ProcurementRegEntities()
            : base("name=ProcurementRegEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Adress> Adresses { get; set; }
        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<LogIn> LogIns { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<ResponsiblePerson> ResponsiblePersons { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Street> Streets { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TimeZone> TimeZones { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<Zakupka> Zakupkas { get; set; }
    }
}