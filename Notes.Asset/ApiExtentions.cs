﻿using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Asset
{
    public static class ApiExtentions
    {
        public static ContainerBuilder AddApiAsset(this ContainerBuilder builder, Func<ApiOptions> optFunc)
        {

            var opt = optFunc();

            builder.Register((cnt) => Options.Create(opt)).SingleInstance();

            builder.RegisterType<UserSvc>().As<IUserSvc>().SingleInstance();

            return builder;
        }
    }
}
