using Microsoft.EntityFrameworkCore;
using Notes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Toolbelt.ComponentModel.DataAnnotations;

namespace Notes.Data
{
    public class NotesWriteDbContext : DbContext
    {
        public NotesWriteDbContext(DbContextOptions<NotesWriteDbContext> dbContextOptions) : base(dbContextOptions)
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


            modelBuilder.Entity<NotesSectionEntity>();
            modelBuilder.Entity<NotesEntity>();

        }




        public DbSet<NotesEntity> Notes { get; set; }

        public DbSet<NotesSectionEntity> Sections { get; set; }


        public DbSet<NotesPageEntity> Pages { get; set; }


        public DbSet<NotesContentEntity> Contentes { get; set; }

        public DbSet<NotesForCourseEntity> NotesForCourse { get; set; }

    }
}
