﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebTeploobmenApp_Suv.Data;

#nullable disable

namespace WebTeploobmenApp_Suv.Migrations
{
    [DbContext(typeof(TeploobmenContext))]
    [Migration("20241216124628_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("WebTeploobmenApp_Suv.Data.Variant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Avg_heat_capacity")
                        .HasColumnType("REAL");

                    b.Property<double>("Consumption_of_pellets")
                        .HasColumnType("REAL");

                    b.Property<double>("Diameter")
                        .HasColumnType("REAL");

                    b.Property<double>("Diameter_of_pellets")
                        .HasColumnType("REAL");

                    b.Property<double>("Heat_capacity_of_pellets")
                        .HasColumnType("REAL");

                    b.Property<double>("Height")
                        .HasColumnType("REAL");

                    b.Property<double>("Speed_air")
                        .HasColumnType("REAL");

                    b.Property<double>("Temp_air")
                        .HasColumnType("REAL");

                    b.Property<double>("Temp_pellets")
                        .HasColumnType("REAL");

                    b.Property<double>("Volumetric_heat_transfer_coefficient")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Variants");
                });
#pragma warning restore 612, 618
        }
    }
}
