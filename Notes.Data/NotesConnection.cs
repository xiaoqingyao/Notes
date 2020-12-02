using LinqToDB;
using LinqToDB.AspNet.Logging;
using LinqToDB.Configuration;
using LinqToDB.Data;
using Notes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace Notes.Data
{
    public class NotesConnection : DataConnection
    {
        public NotesConnection(LinqToDbConnectionOptions<NotesConnection> options) : base(options)
        {
        }

        public ITable<NotesPageEntity> NotesPage => GetTable<NotesPageEntity>();

        public ITable<NotesSectionEntity> NotesSections => GetTable<NotesSectionEntity>();

        public ITable<NotesContentEntity> PageContent => GetTable<NotesContentEntity>();

        public ITable<NotesEntity> Notes => GetTable<NotesEntity>();

        public ITable<CatalogEntity> Catalogs => GetTable<CatalogEntity>();

        public ITable<NotesForCourseEntity> NotesForCourse => GetTable<NotesForCourseEntity>();


        protected static LinqToDbConnectionOptions<NotesConnection> ChangeType<T>(LinqToDbConnectionOptions<T> TDataContext) 
            where T  : DataConnection
        {
            var builder = new LinqToDbConnectionOptionsBuilder();
            builder.UseSqlServer(TDataContext.ConnectionString);

#if DEBUG

            builder.WriteTraceWith((a, b, c) =>
            {

                Console.WriteLine(a);
                Console.WriteLine(b);

            });
#endif


            return builder.Build<NotesConnection>();
        }

    }
}
