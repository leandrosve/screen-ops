﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ScreeningsService.Data;

#nullable disable

namespace ScreeningsService.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20250413041517_ImproveScreeningDateAndTime")]
    partial class ImproveScreeningDateAndTime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ScreeningsService.Models.Screening", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CinemaId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time without time zone");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.HasIndex("MovieId");

                    b.HasIndex("RoomId");

                    b.HasIndex("Status");

                    b.ToTable("Screenings");
                });

            modelBuilder.Entity("ScreeningsService.Models.ScreeningFeature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Feature")
                        .HasColumnType("integer");

                    b.Property<Guid>("ScreeningId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ScreeningId");

                    b.ToTable("ScreeningFeature");
                });

            modelBuilder.Entity("ScreeningsService.Models.ScreeningFeature", b =>
                {
                    b.HasOne("ScreeningsService.Models.Screening", "Screening")
                        .WithMany("Features")
                        .HasForeignKey("ScreeningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Screening");
                });

            modelBuilder.Entity("ScreeningsService.Models.Screening", b =>
                {
                    b.Navigation("Features");
                });
#pragma warning restore 612, 618
        }
    }
}
