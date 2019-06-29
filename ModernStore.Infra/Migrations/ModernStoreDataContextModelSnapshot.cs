﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModernStore.Infra.DataContext;

namespace ModernStore.Infra.Migrations
{
    [DbContext(typeof(ModernStoreDataContext))]
    partial class ModernStoreDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ModernStore.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BirthDate");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("CustomerId");

                    b.Property<decimal>("DeliveryFee")
                        .HasColumnType("money");

                    b.Property<decimal>("Discount")
                        .HasColumnType("money");

                    b.Property<string>("Number")
                        .IsRequired()
                        .IsFixedLength(true)
                        .HasMaxLength(8);

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("OrderId");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<Guid?>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("varchar(1024)")
                        .HasMaxLength(1024);

                    b.Property<decimal>("Price");

                    b.Property<int>("QuantityOnHand");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Password")
                        .IsRequired()
                        .IsFixedLength(true)
                        .HasMaxLength(32);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.Customer", b =>
                {
                    b.HasOne("ModernStore.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.OwnsOne("ModernStore.Domain.ValueObjects.Document", "Document", b1 =>
                        {
                            b1.Property<Guid>("CustomerId");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnName("DocumentNumber")
                                .HasColumnType("varchar(11)")
                                .IsFixedLength(true)
                                .HasMaxLength(11);

                            b1.ToTable("Customer");

                            b1.HasOne("ModernStore.Domain.Entities.Customer")
                                .WithOne("Document")
                                .HasForeignKey("ModernStore.Domain.ValueObjects.Document", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("ModernStore.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("CustomerId");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnName("Email")
                                .HasColumnType("varchar(150)")
                                .HasMaxLength(150);

                            b1.ToTable("Customer");

                            b1.HasOne("ModernStore.Domain.Entities.Customer")
                                .WithOne("Email")
                                .HasForeignKey("ModernStore.Domain.ValueObjects.Email", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("ModernStore.Domain.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("CustomerId");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnName("FirstName")
                                .HasColumnType("varchar(50)")
                                .HasMaxLength(50);

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnName("LastName")
                                .HasColumnType("varchar(80)")
                                .HasMaxLength(80);

                            b1.ToTable("Customer");

                            b1.HasOne("ModernStore.Domain.Entities.Customer")
                                .WithOne("Name")
                                .HasForeignKey("ModernStore.Domain.ValueObjects.Name", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.Order", b =>
                {
                    b.HasOne("ModernStore.Domain.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("ModernStore.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("ModernStore.Domain.Entities.Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId");

                    b.HasOne("ModernStore.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
