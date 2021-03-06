﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notes.Data;

namespace Notes.Data.Migrations
{
    [DbContext(typeof(NotesDbContext))]
    [Migration("20200722035652_addCatalog")]
    partial class addCatalog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Notes.Data.Entities.NotesContentEntity", b =>
                {
                    b.Property<int>("IndentityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NotesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("IndentityId");

                    b.HasIndex("Deleted");

                    b.HasIndex("Id");

                    b.HasIndex("NotesId");

                    b.HasIndex("PageId");

                    b.HasIndex("SectionId");

                    b.ToTable("NotesContent");
                });

            modelBuilder.Entity("Notes.Data.Entities.NotesEntity", b =>
                {
                    b.Property<int>("IndentityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CalelogCode")
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Catelog")
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("ClassId")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ClassName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CreatorName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("GradeCode")
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("IndentityId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("Deleted");

                    b.HasIndex("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("Notes.Data.Entities.NotesPageEntity", b =>
                {
                    b.Property<int>("IndentityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NotesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("IndentityId");

                    b.HasIndex("Deleted");

                    b.HasIndex("Id");

                    b.HasIndex("NotesId");

                    b.HasIndex("SectionId");

                    b.ToTable("NotesPage");
                });

            modelBuilder.Entity("Notes.Data.Entities.NotesSectionEntity", b =>
                {
                    b.Property<int>("IndentityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Deleted")
                        .HasColumnType("int");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NotesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("IndentityId");

                    b.HasIndex("Deleted");

                    b.HasIndex("Id");

                    b.HasIndex("NotesId");

                    b.ToTable("NotesSection");
                });
#pragma warning restore 612, 618
        }
    }
}
