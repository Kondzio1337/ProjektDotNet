﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projekt.Data;

#nullable disable

namespace Projekt.Migrations
{
    [DbContext(typeof(ProjektContext))]
    partial class ProjektContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Projekt.Models.Connector", b =>
                {
                    b.Property<int>("ConnectorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConnectorId"));

                    b.Property<int>("KategoriaId")
                        .HasColumnType("int");

                    b.Property<int>("ProduktId")
                        .HasColumnType("int");

                    b.HasKey("ConnectorId");

                    b.HasIndex("KategoriaId");

                    b.HasIndex("ProduktId");

                    b.ToTable("connectors");
                });

            modelBuilder.Entity("Projekt.Models.Kategoria", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("kategorie");
                });

            modelBuilder.Entity("Projekt.Models.Produkt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.ToTable("Produkt");
                });

            modelBuilder.Entity("Projekt.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdRoli")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Projekt.Models.Connector", b =>
                {
                    b.HasOne("Projekt.Models.Kategoria", "Kategoria")
                        .WithMany("ProduktyKategorie")
                        .HasForeignKey("KategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Projekt.Models.Produkt", "Produkt")
                        .WithMany("ProduktyKategorie")
                        .HasForeignKey("ProduktId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategoria");

                    b.Navigation("Produkt");
                });

            modelBuilder.Entity("Projekt.Models.Kategoria", b =>
                {
                    b.Navigation("ProduktyKategorie");
                });

            modelBuilder.Entity("Projekt.Models.Produkt", b =>
                {
                    b.Navigation("ProduktyKategorie");
                });
#pragma warning restore 612, 618
        }
    }
}
