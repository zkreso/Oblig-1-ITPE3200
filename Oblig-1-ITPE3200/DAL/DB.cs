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

        public virtual DbSet<Sykdom> Sykdomstabel { get; set; }
        public virtual DbSet<Symptom> Symptomstabel { get; set; }
        public virtual DbSet<Relasjon> Relasjonstabel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}

