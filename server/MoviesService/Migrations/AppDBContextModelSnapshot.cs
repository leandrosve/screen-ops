﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ScreenOps.MoviesService.Data;

#nullable disable

namespace MoviesService.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MoviesService.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Acción"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Aventura"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Animación"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Ciencia ficción"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Fantasía"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Terror"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Suspense"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Misterio"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Drama"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Comedia"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Romance"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Musical"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Crimen"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Policíaco"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Western"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Bélico"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Histórico"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Biográfico"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Deportes"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Superhéroes"
                        },
                        new
                        {
                            Id = 21,
                            Name = "Cine negro"
                        },
                        new
                        {
                            Id = 22,
                            Name = "Paranormal"
                        },
                        new
                        {
                            Id = 23,
                            Name = "Metraje encontrado"
                        },
                        new
                        {
                            Id = 24,
                            Name = "Arte y ensayo"
                        },
                        new
                        {
                            Id = 25,
                            Name = "Documental"
                        });
                });

            modelBuilder.Entity("MoviesService.Models.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("LocalizedTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MainActors")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OriginalLanguageCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OriginalReleaseYear")
                        .HasColumnType("integer");

                    b.Property<string>("OriginalTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MoviesService.Models.MovieGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieGenre");
                });

            modelBuilder.Entity("MoviesService.Models.MovieMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieMedia");
                });

            modelBuilder.Entity("MoviesService.Models.MovieGenre", b =>
                {
                    b.HasOne("MoviesService.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesService.Models.Movie", "Movie")
                        .WithMany("Genres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MoviesService.Models.MovieMedia", b =>
                {
                    b.HasOne("MoviesService.Models.Movie", "Movie")
                        .WithMany("Media")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MoviesService.Models.Movie", b =>
                {
                    b.Navigation("Genres");

                    b.Navigation("Media");
                });
#pragma warning restore 612, 618
        }
    }
}
