﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi;

#nullable disable

namespace webapi.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20231210131406_DataSeeding")]
    partial class DataSeeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("webapi.DeliveryStep", b =>
                {
                    b.Property<int>("StepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StepId"), 1L, 1);

                    b.Property<string>("CourierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<string>("StepDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StepId");

                    b.HasIndex("RequestId");

                    b.ToTable("DeliverySteps");

                    b.HasData(
                        new
                        {
                            StepId = 1,
                            CourierName = "Courier 1",
                            Date = new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7800),
                            RequestId = 1,
                            StepDescription = "Received"
                        },
                        new
                        {
                            StepId = 2,
                            CourierName = "Courier 2",
                            Date = new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7802),
                            RequestId = 2,
                            StepDescription = "Delivered"
                        });
                });

            modelBuilder.Entity("webapi.Offer", b =>
                {
                    b.Property<int>("OfferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OfferId"), 1L, 1);

                    b.Property<string>("CannotDeliverReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateDelivered")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateReceived")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OfferDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ValidityPeriod")
                        .HasColumnType("int");

                    b.HasKey("OfferId");

                    b.HasIndex("RequestId");

                    b.ToTable("Offers");

                    b.HasData(
                        new
                        {
                            OfferId = 1,
                            CourierName = "Courier 1",
                            DateDelivered = new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7784),
                            DateReceived = new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7783),
                            OfferDate = new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7781),
                            Price = 50.00m,
                            RequestId = 1,
                            Status = "Accepted",
                            ValidityPeriod = 7
                        },
                        new
                        {
                            OfferId = 2,
                            CannotDeliverReason = "Address not found",
                            CourierName = "Courier 2",
                            DateDelivered = new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7787),
                            DateReceived = new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7786),
                            OfferDate = new DateTime(2023, 12, 10, 13, 14, 6, 171, DateTimeKind.Utc).AddTicks(7785),
                            Price = 30.00m,
                            RequestId = 2,
                            Status = "Pending",
                            ValidityPeriod = 5
                        });
                });

            modelBuilder.Entity("webapi.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"), 1L, 1);

                    b.Property<bool>("DeliveryAtWeekend")
                        .HasColumnType("bit");

                    b.Property<string>("DestinationAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Height")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Length")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Weight")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Width")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Requests");

                    b.HasData(
                        new
                        {
                            RequestId = 1,
                            DeliveryAtWeekend = true,
                            DestinationAddress = "Destination 1",
                            Height = "15",
                            Length = "10",
                            OwnerId = 1,
                            Priority = "High",
                            SourceAddress = "Source 1",
                            Weight = "20",
                            Width = "5"
                        },
                        new
                        {
                            RequestId = 2,
                            DeliveryAtWeekend = false,
                            DestinationAddress = "Destination 2",
                            Height = "12",
                            Length = "8",
                            OwnerId = 2,
                            Priority = "Low",
                            SourceAddress = "Source 2",
                            Weight = "15",
                            Width = "6"
                        });
                });

            modelBuilder.Entity("webapi.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "John Doe"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Jane Smith"
                        });
                });

            modelBuilder.Entity("webapi.DeliveryStep", b =>
                {
                    b.HasOne("webapi.Request", "Request")
                        .WithMany("DeliverySteps")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("webapi.Offer", b =>
                {
                    b.HasOne("webapi.Request", "Request")
                        .WithMany("Offers")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("webapi.Request", b =>
                {
                    b.HasOne("webapi.User", "OwnerUser")
                        .WithMany("Requests")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwnerUser");
                });

            modelBuilder.Entity("webapi.Request", b =>
                {
                    b.Navigation("DeliverySteps");

                    b.Navigation("Offers");
                });

            modelBuilder.Entity("webapi.User", b =>
                {
                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}