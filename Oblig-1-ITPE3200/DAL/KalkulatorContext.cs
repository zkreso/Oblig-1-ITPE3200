using Microsoft.EntityFrameworkCore;
using Oblig_1_ITPE3200.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.DAL
{
    public class KalkulatorContext : DbContext
    {
        public KalkulatorContext(DbContextOptions<KalkulatorContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Symptom> Symptomer { get; set; }
        public DbSet<Diagnose> Diagnoser { get; set; }
        public DbSet<SymptomDiagnose> SymptomDiagnoser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        //Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SymptomDiagnose>()
                .HasKey(sd => new { sd.SymptomId, sd.DiagnoseId });

            modelBuilder.Entity<SymptomDiagnose>()
                .HasOne(sd => sd.Symptom)
                .WithMany(s => s.SymptomDiagnoser)
                .HasForeignKey(sd => sd.SymptomId);

            modelBuilder.Entity<SymptomDiagnose>()
                .HasOne(sd => sd.Diagnose)
                .WithMany(d => d.SymptomDiagnoser)
                .HasForeignKey(sd => sd.DiagnoseId);
        }
    }
}
