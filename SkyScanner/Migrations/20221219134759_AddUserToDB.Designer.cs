﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkyScanner.Data;

#nullable disable

namespace SkyScanner.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20221219134759_AddUserToDB")]
    partial class AddUserToDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SkyScanner.Models.Flight", b =>
                {
                    b.Property<string>("FlightId")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Airline")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(9)");

                    b.HasKey("FlightId");

                    b.HasIndex("UserId");

                    b.ToTable("Flight");
                });

            modelBuilder.Entity("SkyScanner.Models.Seat", b =>
                {
                    b.Property<string>("Seat_Num")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<bool>("Booked")
                        .HasColumnType("bit");

                    b.Property<string>("Class_Type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("FlightId")
                        .HasColumnType("nvarchar(4)");

                    b.HasKey("Seat_Num");

                    b.HasIndex("FlightId");

                    b.ToTable("Seat");
                });

            modelBuilder.Entity("SkyScanner.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SkyScanner.Models.Flight", b =>
                {
                    b.HasOne("SkyScanner.Models.User", null)
                        .WithMany("Flights")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SkyScanner.Models.Seat", b =>
                {
                    b.HasOne("SkyScanner.Models.Flight", null)
                        .WithMany("Seats")
                        .HasForeignKey("FlightId");
                });

            modelBuilder.Entity("SkyScanner.Models.Flight", b =>
                {
                    b.Navigation("Seats");
                });

            modelBuilder.Entity("SkyScanner.Models.User", b =>
                {
                    b.Navigation("Flights");
                });
#pragma warning restore 612, 618
        }
    }
}
