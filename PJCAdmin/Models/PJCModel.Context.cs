﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PJCAdmin.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class pjcEntities : DbContext
    {
        public pjcEntities()
            : base("name=pjcEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Application> Applications { get; set; }
        public DbSet<job> jobs { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<prompt> prompts { get; set; }
        public DbSet<prompttype> prompttypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<task> tasks { get; set; }
        public DbSet<taskcategory> taskcategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<usertask> usertasks { get; set; }
        public DbSet<usertaskprompt> usertaskprompts { get; set; }
        public DbSet<Debug> Debugs { get; set; }
    }
}
