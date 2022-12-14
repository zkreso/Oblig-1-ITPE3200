using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging;
using Oblig_1_ITPE3200.Models;
using System.Diagnostics.CodeAnalysis;

namespace Oblig_1_ITPE3200.DAL
{
    [ExcludeFromCodeCoverage]
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<DiseaseSymptom> DiseaseSymptoms { get; set; }

        // Users for login
        public DbSet<User> Users { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies();
        //}
        
        // source: https://www.entityframeworktutorial.net/efcore/configure-many-to-many-relationship-in-ef-core.aspx
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiseaseSymptom>().HasKey(ds => new { ds.DiseaseId, ds.SymptomId });
        }
    }
}
