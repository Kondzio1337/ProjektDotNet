using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projekt.Models;

namespace Projekt.Data
{
    public class ProjektContext : DbContext
    {
        public ProjektContext (DbContextOptions<ProjektContext> options)
            : base(options)
        {
        }


        public DbSet<Projekt.Models.Produkt> Produkt { get; set; } = default!;
       
        public DbSet<Projekt.Models.Kategoria> kategorie { get; set; }
        public DbSet<Projekt.Models.User> users { get; set; }
        public DbSet<Projekt.Models.Connector> connectors { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kategoria>()
                .HasKey(k => k.CategoryID);
            
            modelBuilder.Entity<Connector>()
        .HasOne(pk => pk.Produkt)
        .WithMany(p => p.ProduktyKategorie)
        .HasForeignKey(pk => pk.ProduktId);

            modelBuilder.Entity<Connector>()
                .HasOne(pk => pk.Kategoria)
                .WithMany(k => k.ProduktyKategorie)
                .HasForeignKey(pk => pk.KategoriaId);

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
    
}
