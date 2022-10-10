using System;
using Microsoft.EntityFrameworkCore;

namespace Oblig_1_ITPE3200.Models
{
    public class DB : DbContext
    {
        // Constructor
        public DB (DbContextOptions<DB> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Sykdom> SykdomTabel { get; set; }
        public virtual DbSet<Symptom> SymptomTabel { get; set; }
        public virtual DbSet<Sykdomssymptom> SykdomssymptomTabel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
