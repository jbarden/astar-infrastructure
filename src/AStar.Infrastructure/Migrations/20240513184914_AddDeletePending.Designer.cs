﻿// <auto-generated />
using System;
using AStar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AStar.Infrastructure.Migrations
{
    [DbContext(typeof(FilesContext))]
    [Migration("20240513184914_AddDeletePending")]
    partial class AddDeletePending
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("NOCASE")
                .HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("AStar.Web.Domain.FileDetail", b =>
                {
                    b.Property<string>("FileName")
                        .HasColumnType("TEXT")
                        .UseCollation("NOCASE");

                    b.Property<string>("DirectoryName")
                        .HasColumnType("TEXT")
                        .UseCollation("NOCASE");

                    b.Property<bool>("DeletePending")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DetailsLastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<long>("FileSize")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastViewed")
                        .HasColumnType("TEXT");

                    b.Property<bool>("SoftDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Width")
                        .HasColumnType("INTEGER");

                    b.HasKey("FileName", "DirectoryName");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("AStar.Web.Domain.TagToIgnore", b =>
                {
                    b.Property<string>("Value")
                        .HasColumnType("TEXT")
                        .UseCollation("NOCASE");

                    b.HasKey("Value");

                    b.ToTable("TagsToIgnore");
                });

            modelBuilder.Entity("AStar.Web.Domain.TagToIgnoreCompletely", b =>
                {
                    b.Property<string>("Value")
                        .HasColumnType("TEXT")
                        .UseCollation("NOCASE");

                    b.HasKey("Value");

                    b.ToTable("TagsToIgnoreCompletely");
                });
#pragma warning restore 612, 618
        }
    }
}