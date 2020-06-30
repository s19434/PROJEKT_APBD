using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace PROJEKT_APBD.Models
{
    public class CampaignAdvertsDbContext : DbContext
    {
        public CampaignAdvertsDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected CampaignAdvertsDbContext()
        {
        }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Campaign>(opt =>
            {
                opt.HasKey(p => p.IdCampaign);
                opt.Property(p => p.IdCampaign)
                .ValueGeneratedOnAdd();

                opt.Property(p => p.StartDate).IsRequired()
                .HasColumnType("Date");

                opt.Property(p => p.EndDate).IsRequired()
                .HasColumnType("Date");

                opt.Property(p => p.PricePerSquareMeter).IsRequired()
                .HasColumnType("decimal(6, 2)");

                opt.HasOne(p => p.Client)
                   .WithMany(p => p.Campaigns)
                   .HasForeignKey(p => p.IdClient)
                   .OnDelete(DeleteBehavior.ClientSetNull);

                opt.HasOne(p => p.FromBuilding)
                   .WithMany(p => p.FromCampaigns)
                   .HasForeignKey(p => p.FromIdBuilding)
                   .OnDelete(DeleteBehavior.ClientSetNull);

                opt.HasOne(p => p.ToBuilding)
                   .WithMany(p => p.ToCampaigns)
                   .HasForeignKey(p => p.ToIdBuilding)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            });

            modelBuilder.Entity<Client>(opt =>
            {
                opt.HasKey(p => p.IdClient);
                opt.Property(p => p.IdClient)
                .ValueGeneratedOnAdd();

                opt.Property(p => p.FirstName).IsRequired()
                .HasMaxLength(100);

                opt.Property(p => p.LastName).IsRequired()
                .HasMaxLength(100);

                opt.Property(p => p.Email).IsRequired()
                .HasMaxLength(100);

                opt.Property(p => p.Phone).IsRequired()
                .HasMaxLength(100);

                opt.Property(p => p.Login).IsRequired()
                .HasMaxLength(100);
            });

            modelBuilder.Entity<Banner>(opt =>
            {
                opt.HasKey(p => p.IdAdvertisement);
                opt.Property(p => p.IdAdvertisement)
                   .ValueGeneratedOnAdd();

                opt.Property(p => p.Price).IsRequired()
                   .HasColumnType("decimal(6, 2)");

                opt.Property(p => p.Area).IsRequired()
                   .HasColumnType("decimal(6, 2)");

                opt.HasOne(p => p.Campaign)
                   .WithMany(p => p.Banners)
                   .HasForeignKey(p => p.IdCampaign)
                   .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Building>(opt =>
            {
                opt.HasKey(p => p.IdBuilding);
                opt.Property(p => p.IdBuilding)
                   .ValueGeneratedOnAdd();

                opt.Property(p => p.Street).IsRequired()
                   .HasMaxLength(100);

                opt.Property(p => p.City).IsRequired()
                   .HasMaxLength(100);

                opt.Property(p => p.Height).IsRequired()
                   .HasColumnType("decimal(6, 2)");
            });
        }

    }
}
