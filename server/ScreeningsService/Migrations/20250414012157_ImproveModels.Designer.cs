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
    [Migration("20250414012157_ImproveModels")]
    partial class ImproveModels
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

                    b.Property<string>("FeaturesRaw")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ScreeningScheduleId")
                        .HasColumnType("uuid");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.HasIndex("MovieId");

                    b.HasIndex("RoomId");

                    b.HasIndex("ScreeningScheduleId");

                    b.HasIndex("Status");

                    b.ToTable("Screenings");
                });

            modelBuilder.Entity("ScreeningsService.Models.ScreeningSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CinemaId")
                        .HasColumnType("uuid");

                    b.Property<string>("DaysOfWeekRaw")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("FeaturesRaw")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.HasIndex("MovieId");

                    b.HasIndex("RoomId");

                    b.HasIndex("Status");

                    b.ToTable("ScreeningSchedules");
                });

            modelBuilder.Entity("ScreeningsService.Models.ScreeningScheduleTime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<TimeOnly>("End")
                        .HasColumnType("time without time zone");

                    b.Property<Guid>("ScheduleId")
                        .HasColumnType("uuid");

                    b.Property<TimeOnly>("Start")
                        .HasColumnType("time without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("ScreeningScheduleTime");
                });

            modelBuilder.Entity("ScreeningsService.Models.Screening", b =>
                {
                    b.HasOne("ScreeningsService.Models.ScreeningSchedule", "ScreeningSchedule")
                        .WithMany("Screenings")
                        .HasForeignKey("ScreeningScheduleId");

                    b.Navigation("ScreeningSchedule");
                });

            modelBuilder.Entity("ScreeningsService.Models.ScreeningScheduleTime", b =>
                {
                    b.HasOne("ScreeningsService.Models.ScreeningSchedule", "Schedule")
                        .WithMany("Times")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("ScreeningsService.Models.ScreeningSchedule", b =>
                {
                    b.Navigation("Screenings");

                    b.Navigation("Times");
                });
#pragma warning restore 612, 618
        }
    }
}
