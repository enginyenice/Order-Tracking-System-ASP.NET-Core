﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SiparisTakip.Models;

namespace SiparisTakip.Migrations
{
    [DbContext(typeof(SiparisTakipDB))]
    [Migration("20200707190816_floatToDouble2")]
    partial class floatToDouble2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SiparisTakip.Models.Tables.Department", b =>
                {
                    b.Property<int>("depId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("depName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("depId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("SiparisTakip.Models.Tables.Request", b =>
                {
                    b.Property<int>("requestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("requestCreateAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("requestDeleteDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("requestDeliveryDate")
                        .HasColumnType("Date");

                    b.Property<string>("requestDepartment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("requestDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("requestEstimatedPrice")
                        .HasColumnType("float");

                    b.Property<string>("requestExpensePlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("requestImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("requestNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("requestProductFeatures")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("requestProject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("requestQuantity")
                        .HasColumnType("int");

                    b.Property<string>("requestSpecies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("requestStatus")
                        .HasColumnType("int");

                    b.Property<string>("requestSteff")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("requestSupplyCompany1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("requestSupplyCompany2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("requestSupplyCompany3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("requestId");

                    b.HasIndex("userId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("SiparisTakip.Models.Tables.Setting", b =>
                {
                    b.Property<int>("settingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("settingEPosta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("settingPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("settingSmtpHost")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("settingSmtpPort")
                        .HasColumnType("int");

                    b.Property<bool>("settingSmtpSSL")
                        .HasColumnType("bit");

                    b.HasKey("settingId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("SiparisTakip.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("userMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userPermission")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userSurname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SiparisTakip.Models.Tables.Request", b =>
                {
                    b.HasOne("SiparisTakip.Models.User", "user")
                        .WithMany("requests")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
