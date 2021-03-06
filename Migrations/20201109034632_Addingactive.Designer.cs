﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tp1_restaurant.Data;

namespace tp1_restaurant.Migrations
{
    [DbContext(typeof(ZhaoContext))]
    [Migration("20201109034632_Addingactive")]
    partial class Addingactive
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("tp1_restaurant.Models.Evaluation", b =>
                {
                    b.Property<int>("EvaluationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Commentaires")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Courriel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Datevisite")
                        .HasColumnType("datetime");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("QualiteRepas")
                        .HasColumnType("int");

                    b.Property<int>("QualiteService")
                        .HasColumnType("int");

                    b.Property<int>("TypeReservation")
                        .HasColumnType("int");

                    b.HasKey("EvaluationId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("tp1_restaurant.Models.Promotion", b =>
                {
                    b.Property<int>("PromotionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateFin")
                        .HasColumnType("datetime");

                    b.Property<string>("DescriptionPromotion")
                        .IsRequired()
                        .HasColumnType("varchar(300)")
                        .HasMaxLength(300);

                    b.Property<int>("TauxApplicable")
                        .HasColumnType("int");

                    b.Property<int>("TypePromotion")
                        .HasColumnType("int");

                    b.HasKey("PromotionId");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("tp1_restaurant.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Courriel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateHeureReservation")
                        .HasColumnType("datetime");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("NombrePersonnes")
                        .HasColumnType("int");

                    b.Property<string>("NumeroTelephone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("TypeReservation")
                        .HasColumnType("int");

                    b.Property<bool>("active")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("ReservationId");

                    b.ToTable("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
