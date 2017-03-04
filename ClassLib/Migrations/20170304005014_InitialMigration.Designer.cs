using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DotnetCoreTrademeStats.ClassLib.Models;

namespace ClassLib.Migrations
{
    [DbContext(typeof(TrademeStatsContext))]
    [Migration("20170304005014_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("DotnetCoreTrademeStats.ClassLib.Models.Agency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsRealEstateAgency");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Agencies");
                });

            modelBuilder.Entity("DotnetCoreTrademeStats.ClassLib.Models.RentalListing", b =>
                {
                    b.Property<int>("ListingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AgencyId");

                    b.Property<int>("Bathrooms");

                    b.Property<int>("Bedrooms");

                    b.Property<string>("District");

                    b.Property<string>("Region");

                    b.Property<int>("RegionId");

                    b.Property<int>("RentPerWeek");

                    b.Property<string>("StartDate");

                    b.Property<DateTime?>("StartDateConverted");

                    b.Property<string>("Suburb");

                    b.Property<int>("SuburbId");

                    b.Property<string>("Title");

                    b.HasKey("ListingId");

                    b.HasIndex("AgencyId");

                    b.ToTable("RentalListings");
                });

            modelBuilder.Entity("DotnetCoreTrademeStats.ClassLib.Models.RentalListing", b =>
                {
                    b.HasOne("DotnetCoreTrademeStats.ClassLib.Models.Agency", "Agency")
                        .WithMany()
                        .HasForeignKey("AgencyId");
                });
        }
    }
}
