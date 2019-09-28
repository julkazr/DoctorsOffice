using DoctorsOffice.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DoctorsOffice.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<PersonInfo> PersonInfos { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<File> Files { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>().ToTable("Doctors")
                .HasMany(d => d.Examinations);
            modelBuilder.Entity<Doctor>()
                .HasRequired(d => d.Image)
                .WithMany();
            modelBuilder.Entity<Patient>().ToTable("Patients")
                .HasMany(p => p.Examinations);
            modelBuilder.Entity<Patient>()
                .HasOptional(p => p.PersonalDoctor)
                .WithMany();
            modelBuilder.Entity<Examination>()
                .HasOptional(p => p.Patient)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Examination>()
                .HasOptional(d => d.Doctor)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Examination>()
                .HasMany(e => e.Files);
        }
    }
}