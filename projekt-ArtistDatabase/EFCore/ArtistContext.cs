using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace projekt_ArtistDatabase.EFCore
{
    public class ArtistContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ArtistDatabase.mdf");
            optionsBuilder.UseSqlServer($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True;Connect Timeout=30");
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Artist table
            modelBuilder.Entity<Artist>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Artist>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");

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
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");

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
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Genre>()
                .Property(g => g.Name)
                .IsRequired();

            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Artists)
                .WithMany(a => a.Genres);
        }
    }
}
