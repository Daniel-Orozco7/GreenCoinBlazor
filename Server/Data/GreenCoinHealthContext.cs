using System;
using System.Collections.Generic;
using GreenCoinHealth.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GreenCoinHealth.Server.Data
{
    public partial class GreenCoinHealthContext : DbContext
    {
        public GreenCoinHealthContext()
        {
        }

        public GreenCoinHealthContext(DbContextOptions<GreenCoinHealthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Alimentos> Alimentos { get; set; } = null!;
        public DbSet<Dieta> Dietas { get; set; }
        public DbSet<Rutina> Rutinas { get; set; }
        public DbSet<Ejercicio> Ejercicios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DANIEL;Database=GreenCoinHealthDB;Trusted_Connection=True;MultipleActiveResultSets=True;Trust Server Certificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(e => e.IdGender);

                entity.ToTable("Gender");

                entity.Property(e => e.IdGender).HasColumnName("Id_Gender");

                entity.Property(e => e.NameGender).HasColumnName("name_Gender");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role");

                entity.Property(e => e.IdRole).HasColumnName("Id_Role");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Dni);

                entity.ToTable("User");

                entity.HasIndex(e => e.IdGender, "IX_User_Id_Gender");

                entity.HasIndex(e => e.IdRole, "IX_User_Id_Role");

                entity.Property(e => e.Dni).HasMaxLength(50);

                entity.Property(e => e.IdGender).HasColumnName("Id_Gender");

                entity.Property(e => e.IdRole).HasColumnName("Id_Role");

                entity.Property(e => e.LastName).HasMaxLength(80);

                entity.Property(e => e.Name).HasMaxLength(80);

                entity.Property(e => e.Phone).HasMaxLength(80);

                entity.Property(e => e.Username)
                    .HasMaxLength(80)
                    .HasColumnName("username");

                entity.HasOne(d => d.IdGenderNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdGender);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole);
            });

            OnModelCreatingPartial(modelBuilder);

         
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
