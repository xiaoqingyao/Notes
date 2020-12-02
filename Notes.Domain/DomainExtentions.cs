using AspectCore.DynamicProxy.Parameters;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Notes.Domain.BusServices;
using Notes.Domain.Core;
using Notes.Domain.Core.Factories;
using Notes.Domain.Core.Users;
using Notes.Events;
using Notes.infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Domain
{
    public static class DomainExtentions
    {

        public static ContainerBuilder AddDomain(this ContainerBuilder builder, bool iocHttp, IConfiguration config)
        {

            builder.Reg<NotesSvc, INotesSvc>(iocHttp);
            builder.Reg<NotesLoader, INotesLoader>(iocHttp);
            builder.Reg<NotesQuerySvc, INotesQuerySvc>(iocHttp);
            builder.Reg<BodyLoader, IBodyLoader>(iocHttp);
            builder.Reg<BodySvc, IBodySvc>(iocHttp);
            builder.Reg<UserRootSvc, IUserRootSvc>(iocHttp);
            builder.Reg<UserLoader, IUserLoader>(iocHttp);



            var opt = config.GetSection(DomainOptions.OptionsSection).Get<DomainOptions>();

            builder.Register(ctx => Options.Create(opt)).SingleInstance();


            builder.AddEventBus(config);

            return builder;
        }

    }
}
