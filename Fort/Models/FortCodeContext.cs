using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Fort.Models
{
    public partial class FortCodeContext : DbContext
    {
        public FortCodeContext()
        {
        }

        public FortCodeContext(DbContextOptions<FortCodeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Countryid).HasColumnName("countryid");

                entity.HasOne(d => d.CityUser)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CityUserId)
                    .HasConstraintName("FK_Cities_Users");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Countryid).HasColumnName("countryid");

                entity.Property(e => e.Countryname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("countryname");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
