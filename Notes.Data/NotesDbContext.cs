using Microsoft.EntityFrameworkCore;
using Notes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Toolbelt.ComponentModel.DataAnnotations;

namespace Notes.Data
{
    public class NotesDbContext : DbContext
    {


        public const string NotesConn = "NotesConn";

        public const string NotesWriteConn = "NotesWriteConn";




        public NotesDbContext(DbContextOptions<NotesDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.BuildIndexesFromAnnotations();


        }


        public DbSet<NotesEntity> Notes { get; set; }

        public DbSet<NotesSectionEntity> Sections { get; set; }


        public DbSet<NotesPageEntity> Pages { get; set; }


        public DbSet<NotesContentEntity> Contentes { get; set; }

        public DbSet<CatalogEntity> Catalogs { get; set; }

        public DbSet<NotesForCourseEntity> NotesForCourse { get; set; }



    }
}
