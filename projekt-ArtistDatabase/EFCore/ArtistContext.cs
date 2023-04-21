using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_ArtistDatabase.EFCore
{
    public class ArtistContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\Users\vojte\Můj disk\Documents\00 Skola\VUT\4. semestr\BPC-OOP Objektově orientované programování\cvičení\projekt-ArtistDatabase\projekt-ArtistDatabase\ArtistDatabase.mdf;Integrated Security=True;Connect Timeout=30");
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Artist table
            modelBuilder.Entity<Artist>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Artist>()
                .Property(a => a.Name)
                .IsRequired();

            modelBuilder.Entity<Artist>()
                .HasMany(a => a.Genres)
                .WithMany(g => g.Artists);

            modelBuilder.Entity<Artist>()
                .HasMany(a => a.Albums)
                .WithOne(al => al.Artist)
                .HasForeignKey(al => al.ArtistId);

            // Album table
            modelBuilder.Entity<Album>()
                .HasKey(al => al.Id);

            modelBuilder.Entity<Album>()
                .Property(al => al.Name)
                .IsRequired();

            modelBuilder.Entity<Album>()
                .Property(al => al.Year)
                .IsRequired();

            // Genre table
            modelBuilder.Entity<Genre>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<Genre>()
                .Property(g => g.Name)
                .IsRequired();

            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Artists)
                .WithMany(a => a.Genres);
        }
    }
}
