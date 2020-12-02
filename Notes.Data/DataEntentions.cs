using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notes.Data.EFProvider.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Data
{
    public static class DataEntentions
    {
        public const string IsDebug = "IsDebug";

        public static IServiceCollection AddNotesDbSorte(this IServiceCollection servcies, string connString, string writeConnStr, bool isDebug)
        {

            servcies.AddDbContext<NotesDbContext>(opts =>
            {
                opts.UseSqlServer(connString);
            })
                .AddDbContext<NotesWriteDbContext>(opts =>
                {
                    opts.UseSqlServer(writeConnStr);
                })
                .AddUnitOfWork<NotesDbContext, NotesWriteDbContext>();

            //servcies.AddScoped<INotesWriter, NotesWriter>();
            //            servcies.AddLinqToDbContext<NotesConnection>((p, opt) =>
            //                     {

            //                         opt.UseDefaultLogging(p);

            //#if DEBUG

            //                opt.WriteTraceWith((a, b, c) =>
            //                               {

            //                                   Console.WriteLine(a);
            //                                   Console.WriteLine(b);

            //                               });
            //#endif
            //            });




            //servcies.AddLinqToDbContext<NotesConnection>((p, opt) =>
            //{


            //});


#if DEBUG

            LinqToDB.Data.DataConnection.TurnTraceSwitchOn();

#endif 

            servcies.AddLinqToDbContext<WritterConnection>((p, opt) =>
            {


                opt.UseSqlServer(writeConnStr);
            });


            servcies.AddLinqToDbContext<ReaderConnection>((p, opt) =>
            {

                opt.UseSqlServer(connString);

            });




            return servcies;
        }
    }
}
