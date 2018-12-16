using SharedLib;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Merchant> Merchants { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public ApplicationDbContext()
        {
            PopulateDataBase();
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        private void PopulateDataBase()
        {
            // TODO
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.ToTable("Company");

                entity.Property(c => c.Id)
                                   .HasColumnName("ID")
                                   .HasMaxLength(255)
                                   .IsUnicode(false)
                                   .ValueGeneratedNever();

                entity.Property(c => c.MerchantID)
                                   .HasColumnName("MerchandID")
                                   .HasMaxLength(255)
                                   .IsUnicode(false)
                                   .ValueGeneratedNever();

                entity.Property(c => c.City)
                 .HasColumnName("City")
                 .HasMaxLength(255)
                 .IsUnicode(false)
                 .ValueGeneratedNever();

                entity.Property(c => c.Number)
                    .HasColumnName("Number")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(c => c.PostalCode)
                     .HasColumnName("Postalcode")
                     .HasMaxLength(255)
                      .IsUnicode(false)
                     .ValueGeneratedNever();

                entity.Property(c => c.Street)
                   .HasColumnName("Street")
                   .HasMaxLength(255)
                   .IsUnicode(false)
                   .ValueGeneratedNever();

                entity.Property(c => c.Type)
                    .HasColumnName("Type")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(c => c.Website)
                    .HasColumnName("WEBSITE")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.HasMany(c => c.Events)
                    .WithOne()
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(c => c.Promotions)
                     .WithOne()
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(c => c.Subscriptions)
                  .WithOne()
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Event");

                entity.Property(e => e.Id)
                  .HasColumnName("ID")
                  .HasMaxLength(255)
                  .IsUnicode(false);

                entity.Property(e => e.Name)
                  .HasColumnName("Name")
                  .HasMaxLength(255)
                  .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("Description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                              .HasColumnName("StartDate")
                              .HasMaxLength(255)
                              .IsUnicode(false);

                entity.Property(e => e.EndDate)
                    .HasColumnName("EndDate")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Merchant>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Merchant");

                entity.Property(e => e.Id)
                  .HasColumnName("ID")
                  .HasMaxLength(255)
                  .IsUnicode(false);

                entity.Property(e => e.FirstName)
                  .HasColumnName("FirstName")
                  .HasMaxLength(255)
                  .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("Email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("City")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .HasColumnName("Number")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                  .HasColumnName("PostalCode")
                  .HasMaxLength(255)
                  .IsUnicode(false);

                entity.Property(e => e.Street)
              .HasColumnName("Street")
              .HasMaxLength(255)
              .IsUnicode(false);

                entity.HasMany(c => c.Companies)
             .WithOne()
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Promotion");

                entity.Property(e => e.Id)
                  .HasColumnName("ID")
                  .HasMaxLength(255)
                  .IsUnicode(false);

                entity.Property(e => e.Name)
                  .HasColumnName("Name")
                  .HasMaxLength(255)
                  .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("Description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasColumnName("StartDate")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)
                  .HasColumnName("EndDate")
                  .HasMaxLength(255)
                  .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("User");

                entity.Property(e => e.Id)
                  .HasColumnName("ID")
                  .HasMaxLength(255)
                  .IsUnicode(false);

                entity.Property(e => e.FirstName)
                  .HasColumnName("FirstName")
                  .HasMaxLength(255)
                  .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
              .HasColumnName("Email")
              .HasMaxLength(255)
              .IsUnicode(false);

                entity.Property(e => e.City)
             .HasColumnName("City")
             .HasMaxLength(255)
             .IsUnicode(false);

                entity.Property(e => e.Number)
                    .HasColumnName("Number")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                  .HasColumnName("PostalCode")
                  .HasMaxLength(255)
                  .IsUnicode(false);

                entity.Property(e => e.Street)
                .HasColumnName("Street")
                .HasMaxLength(255)
                .IsUnicode(false);

                entity.HasMany(c => c.Subscriptions)
             .WithOne()
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Subscription");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}