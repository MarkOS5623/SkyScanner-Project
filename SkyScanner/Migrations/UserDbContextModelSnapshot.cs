﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkyScanner.Data;

#nullable disable

namespace SkyScanner.Migrations
{
    [DbContext(typeof(UserDbContext))]
    partial class UserDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SkyScanner.Models.Booking", b =>
                {
                    b.Property<string>("BookingID")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("BookedSeats")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("FlightDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FlightId")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoWay")
                        .HasColumnType("bit");

                    b.Property<string>("User_ID")
                        .IsRequired()
                        .HasColumnType("nvarchar(4)");

                    b.HasKey("BookingID");

                    b.HasIndex("User_ID");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("SkyScanner.Models.CreditCard", b =>
                {
                    b.Property<string>("CardNumber")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("CVV")
                        .HasColumnType("int");

                    b.Property<int>("ExpMonth")
                        .HasColumnType("int");

                    b.Property<int>("ExpYear")
                        .HasColumnType("int");

                    b.Property<string>("Israeli_ID")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<bool>("Save")
                        .HasColumnType("bit");

                    b.Property<string>("User_ID")
                        .IsRequired()
                        .HasColumnType("nvarchar(4)");

                    b.HasKey("CardNumber");

                    b.HasIndex("User_ID");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("SkyScanner.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("KeepLoggedIn")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SkyScanner.Models.Booking", b =>
                {
                    b.HasOne("SkyScanner.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SkyScanner.Models.CreditCard", b =>
                {
                    b.HasOne("SkyScanner.Models.User", "User")
                        .WithMany("CreditCards")
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SkyScanner.Models.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("CreditCards");
                });
#pragma warning restore 612, 618
        }
    }
}
