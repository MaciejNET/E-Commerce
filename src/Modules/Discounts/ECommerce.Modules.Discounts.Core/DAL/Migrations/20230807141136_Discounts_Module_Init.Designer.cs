﻿// <auto-generated />
using System;
using ECommerce.Modules.Discounts.Core.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ECommerce.Modules.Discounts.Core.DAL.Migrations
{
    [DbContext(typeof(DiscountsDbContext))]
    [Migration("20230807141136_Discounts_Module_Init")]
    partial class Discounts_Module_Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("discounts")
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ECommerce.Modules.Discounts.Core.Entities.DiscountCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Percentage")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("DiscountCodes", "discounts");
                });

            modelBuilder.Entity("ECommerce.Modules.Discounts.Core.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Products", "discounts");
                });

            modelBuilder.Entity("ECommerce.Modules.Discounts.Core.Entities.ProductDiscount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("NewPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductDiscounts", "discounts");
                });

            modelBuilder.Entity("ProductsDiscountCodes", b =>
                {
                    b.Property<Guid>("DiscountCodesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DiscountCodesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("ProductsDiscountCodes", "discounts");
                });

            modelBuilder.Entity("ECommerce.Modules.Discounts.Core.Entities.ProductDiscount", b =>
                {
                    b.HasOne("ECommerce.Modules.Discounts.Core.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProductsDiscountCodes", b =>
                {
                    b.HasOne("ECommerce.Modules.Discounts.Core.Entities.DiscountCode", null)
                        .WithMany()
                        .HasForeignKey("DiscountCodesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ECommerce.Modules.Discounts.Core.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
