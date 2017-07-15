using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DotnetCoreTrademeStats.ClassLib.Models;

namespace ClassLib.Migrations
{
    [DbContext(typeof(TrademeStatsContext))]
    [Migration("20170620123120_AddDistrictId")]
    partial class AddDistrictId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("DotnetCoreTrademeStats.ClassLib.Models.Agency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsRealEstateAgency");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Agencies");
                });

            modelBuilder.Entity("DotnetCoreTrademeStats.ClassLib.Models.District", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("DistrictId");

                    b.Property<int?>("LocalityId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("LocalityId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("DotnetCoreTrademeStats.ClassLib.Models.Locality", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("LocalityId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Localities");
                });

            modelBuilder.Entity("DotnetCoreTrademeStats.ClassLib.Models.RentalListing", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ListingId");

                    b.Property<int?>("AgencyId");

                    b.Property<int>("Bathrooms");

                    b.Property<int>("Bedrooms");

                    b.Property<string>("District");

                    b.Property<int>("DistrictId");

                    b.Property<string>("Region");

                    b.Property<int>("RegionId");

                    b.Property<int>("RentPerWeek");

                    b.Property<string>("StartDate");

                    b.Property<DateTime?>("StartDateConverted");

                    b.Property<string>("Suburb");

                    b.Property<int>("SuburbId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("AgencyId");

                    b.ToTable("RentalListings");
                });

            modelBuilder.Entity("DotnetCoreTrademeStats.ClassLib.Models.Suburb", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("SuburbId");

                    b.Property<int?>("DistrictId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Suburbs");
                });

            modelBuilder.Entity("DotnetCoreTrademeStats.ClassLib.Models.District", b =>
                {
                    b.HasOne("DotnetCoreTrademeStats.ClassLib.Models.Locality", "Locality")
                        .WithMany("Districts")
                        .HasForeignKey("LocalityId");
                });

            modelBuilder.Entity("DotnetCoreTrademeStats.ClassLib.Models.RentalListing", b =>
                {
                    b.HasOne("DotnetCoreTrademeStats.ClassLib.Models.Agency", "Agency")
                        .WithMany()
                        .HasForeignKey("AgencyId");
                });

            modelBuilder.Entity("DotnetCoreTrademeStats.ClassLib.Models.Suburb", b =>
                {
                    b.HasOne("DotnetCoreTrademeStats.ClassLib.Models.District", "District")
                        .WithMany("Suburbs")
                        .HasForeignKey("DistrictId");
                });
        }
    }
}
