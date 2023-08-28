﻿// <auto-generated />
using System;
using ECommerce.Modules.Returns.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ECommerce.Modules.Returns.Infrastructure.EF.Migrations
{
    [DbContext(typeof(ReturnsDbContext))]
    [Migration("20230828082409_Returns_Module_Init")]
    partial class Returns_Module_Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("returns")
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ECommerce.Modules.Returns.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderPlaced")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders", "returns");
                });

            modelBuilder.Entity("ECommerce.Modules.Returns.Domain.Entities.OrderProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsReturn")
                        .HasColumnType("bit");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Sku")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderProducts", "returns");
                });

            modelBuilder.Entity("ECommerce.Modules.Returns.Domain.Entities.Return", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ReturnStatus")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("OrderProductId")
                        .IsUnique()
                        .HasFilter("[OrderProductId] IS NOT NULL");

                    b.ToTable("Returns", "returns");
                });

            modelBuilder.Entity("ECommerce.Modules.Returns.Domain.Entities.OrderProduct", b =>
                {
                    b.HasOne("ECommerce.Modules.Returns.Domain.Entities.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("ECommerce.Modules.Returns.Domain.Entities.Return", b =>
                {
                    b.HasOne("ECommerce.Modules.Returns.Domain.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("ECommerce.Modules.Returns.Domain.Entities.OrderProduct", "OrderProduct")
                        .WithOne()
                        .HasForeignKey("ECommerce.Modules.Returns.Domain.Entities.Return", "OrderProductId");

                    b.Navigation("Order");

                    b.Navigation("OrderProduct");
                });

            modelBuilder.Entity("ECommerce.Modules.Returns.Domain.Entities.Order", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
