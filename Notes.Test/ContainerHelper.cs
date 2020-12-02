using Autofac;
using Autofac.Extensions.DependencyInjection;
using DotNetCore.CAP.Processor;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Notes.Application.Modules;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.IO;

namespace Notes.Test
{

    public class ContainerHelper
    {



        public static IHttpContextAccessor Teacher(string role = "teacher")
        {
            var hca = new Mock<IHttpContextAccessor>();

            var context = new DefaultHttpContext();
            context.Request.Headers.Add(NotesContext.HeaderToken, new Microsoft.Extensions.Primitives.StringValues("05986DDB458B8F10A31D59B613D"));

            hca.Setup(_ => _.HttpContext).Returns(context);


            return hca.Object;
        }

        public static IContainer Builder()
        {

            var Services = new ServiceCollection();
            Services.AddLogging();
            //Services.AddTransient(s => new LoggerFactory().CreateLogger<Dispatcher>());
            //Services.AddTransient(s => new LoggerFactory().CreateLogger<DotNetCore.CAP.Internal.IMessageSender>());

            var env = new Mock<IWebHostEnvironment>();
            env.Setup(e => e.EnvironmentName).Returns("Development");
            env.Setup(e => e.ContentRootPath).Returns(Directory.GetCurrentDirectory());
    




            IConfiguration cfg = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var startup = new Notes.Application.Startup(cfg, env.Object)
            {
                IocHttp = false
            };

            startup.ConfigureServices(Services);

            var builder = new ContainerBuilder();

            builder.Register(ctx => Teacher());

            startup.ConfigureContainer(builder);

            builder.Populate(Services);

            return builder.Build();


        }

    }

}