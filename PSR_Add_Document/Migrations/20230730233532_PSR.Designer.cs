﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PSR_Add_Document.Models;

#nullable disable

namespace PSR_Add_Document.Migrations
{
    [DbContext(typeof(CustomerDbContext))]
    [Migration("20230730233532_PSR")]
    partial class PSR
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PSR_Add_Document.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Brn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TinNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PSR_Add_Document.Models.CustomerDocument", b =>
                {
                    b.Property<int>("CustomerDocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerDocumentId"), 1L, 1);

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AssesmentYear")
                        .HasColumnType("int");

                    b.Property<string>("BF1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BF2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BF3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BF4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BF5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BF6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Document")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ProcessDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProcessIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProcessUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TinNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerDocumentId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerDocuments");
                });

            modelBuilder.Entity("PSR_Add_Document.Models.Models.BranchUserLogin", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("Active")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EmpID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LoginID")
                        .HasColumnType("int");

                    b.Property<string>("LoginStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("UserRole")
                        .HasColumnType("int");

                    b.Property<string>("UserStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("UserRole");

                    b.ToTable("branchUserLogins");
                });

            modelBuilder.Entity("PSR_Add_Document.Models.Models.CustomerUserLogin", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("Active")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmpID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IPAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("LoginID")
                        .HasColumnType("int");

                    b.Property<string>("LoginStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.Property<string>("UserStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("CustomerUserLogins");
                });

            modelBuilder.Entity("PSR_Add_Document.Models.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"), 1L, 1);

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PSR_Add_Document.Models.OTPManage", b =>
                {
                    b.Property<int>("OTPId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OTPId"), 1L, 1);

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("IPADDRESS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OTP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("OtpCreateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("OtpLastingTime")
                        .HasColumnType("datetime2");

                    b.HasKey("OTPId");

                    b.ToTable("OTPManage");
                });

            modelBuilder.Entity("PSR_Add_Document.Models.OtpVerificationOptions", b =>
                {
                    b.Property<int>("OtpVerificationOptionsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OtpVerificationOptionsId"), 1L, 1);

                    b.Property<bool>("EnableUrl")
                        .HasColumnType("bit");

                    b.Property<int>("Expire")
                        .HasColumnType("int");

                    b.Property<int>("Iterations")
                        .HasColumnType("int");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OTP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("OtpVerificationOptionsId");

                    b.ToTable("OtpVerificationOptions");
                });

            modelBuilder.Entity("PSR_Add_Document.Models.CustomerDocument", b =>
                {
                    b.HasOne("PSR_Add_Document.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("PSR_Add_Document.Models.Models.BranchUserLogin", b =>
                {
                    b.HasOne("PSR_Add_Document.Models.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("UserRole");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
