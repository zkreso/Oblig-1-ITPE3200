using Microsoft.EntityFrameworkCore;
using Oblig_1_ITPE3200.Models;

namespace Oblig_1_ITPE3200.DAL
{
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public virtual DbSet<Disease> Diseases { get; set; }
        public virtual DbSet<Symptom> Symptoms { get; set; }
        public virtual DbSet<DiseaseSymptom> DiseaseSymptoms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
