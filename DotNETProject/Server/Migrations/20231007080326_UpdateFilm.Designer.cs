﻿// <auto-generated />
using System;
using DotNETProject.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotNETProject.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231007080326_UpdateFilm")]
    partial class UpdateFilm
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DotNETProject.Server.Models.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EpisodeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TVSeriesId")
                        .HasColumnType("int");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<long>("View")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TVSeriesId");

                    b.ToTable("episodes");
                });

            modelBuilder.Entity("DotNETProject.Server.Models.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BackgroundUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("IMDBScore")
                        .HasColumnType("float");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReleaseYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("View")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("films");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("DotNETProject.Server.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsPermit")
                        .HasColumnType("bit");

                    b.Property<string>("Otp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("ResetPasswordExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("DotNETProject.Server.Models.Movie", b =>
                {
                    b.HasBaseType("DotNETProject.Server.Models.Film");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("movies");
                });

            modelBuilder.Entity("DotNETProject.Server.Models.TVSeries", b =>
                {
                    b.HasBaseType("DotNETProject.Server.Models.Film");

                    b.ToTable("tvseries");
                });

            modelBuilder.Entity("DotNETProject.Server.Models.Episode", b =>
                {
                    b.HasOne("DotNETProject.Server.Models.TVSeries", null)
                        .WithMany("episodes")
                        .HasForeignKey("TVSeriesId");
                });

            modelBuilder.Entity("DotNETProject.Server.Models.Movie", b =>
                {
                    b.HasOne("DotNETProject.Server.Models.Film", null)
                        .WithOne()
                        .HasForeignKey("DotNETProject.Server.Models.Movie", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DotNETProject.Server.Models.TVSeries", b =>
                {
                    b.HasOne("DotNETProject.Server.Models.Film", null)
                        .WithOne()
                        .HasForeignKey("DotNETProject.Server.Models.TVSeries", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DotNETProject.Server.Models.TVSeries", b =>
                {
                    b.Navigation("episodes");
                });
#pragma warning restore 612, 618
        }
    }
}
