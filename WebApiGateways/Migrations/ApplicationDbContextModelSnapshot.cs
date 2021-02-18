﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiGateways.Contexts;

namespace WebApiGateways.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApiGateways.Entities.Gateway", b =>
                {
                    b.Property<string>("SerialNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IpAddress");

                    b.Property<string>("Name");

                    b.HasKey("SerialNumber");

                    b.ToTable("GatewaysController");
                });

            modelBuilder.Entity("WebApiGateways.Entities.Peripheral", b =>
                {
                    b.Property<long>("UID");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Status");

                    b.Property<string>("Vendor");

                    b.HasKey("UID");

                    b.ToTable("Peripherals");
                });

            modelBuilder.Entity("WebApiGateways.Entities.PeripheralsGateways", b =>
                {
                    b.Property<string>("GatewaySerialNumber");

                    b.Property<long>("PeripheralId");

                    b.HasKey("GatewaySerialNumber", "PeripheralId");

                    b.ToTable("PeripheralGateways");
                });
#pragma warning restore 612, 618
        }
    }
}