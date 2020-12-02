using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Notes.Data;
using Notes.Asset;
using Notes.Domain;
using Notes.infrastructure;
using Notes.Application.Modules;
using Microsoft.AspNetCore.Http;
using Notes.Infrastructure.Caching;
using DotNetCore.CAP;
using Notes.Domain.BusServices;
using Notes.Application.Modules.Factories;

namespace Notes.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {

            var cb = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath);

            cb.AddJF(env, "appsettings", "CAPSettings", "api");

            cb.AddEnvironmentVariables();


            Configuration = cb.Build(); //configuration;

            //Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public ILifetimeScope AutofacContainer { get; private set; }



        public bool IocHttp { get; set; } = true;


        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:

            if (IocHttp)
            {
                builder.RegisterType<HttpContextAccessor>()
                  .As<IHttpContextAccessor>()
                  .SingleInstance();

            }
            builder.Reg<AppUser, IAppUser>(IocHttp);
            builder.Reg<NotesContext, INotesHttpContext>(IocHttp);
            builder.Reg<QueryBridge, IQueryBridge>(IocHttp);
            builder.Reg<EntryFromD4L, IEntry>(IocHttp);

            builder.AddApiAsset(() => Configuration.GetSection(ApiOptions.SectionName).Get<ApiOptions>());

            builder.AddDomain(this.IocHttp, Configuration);

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();


            services.AddOptions();


            //Db
            services.AddNotesDbSorte(Configuration.GetConnectionString(NotesDbContext.NotesConn)
                                        , Configuration.GetConnectionString(NotesDbContext.NotesWriteConn)
                                        , Configuration.GetValue<bool>(DataEntentions.IsDebug));


            //Reids缓存...
            services.AddRedisCaching(Configuration);


            if (Configuration.GetValue<bool>("IsSubscriber"))
            {
                //Note: The injection of services needs before of `services.AddCap()`
                services.AddTransient<ISubscriberService, SubscriberService>();
            }




            services.AddCap(co =>
            {

                co.UseEntityFramework<NotesWriteDbContext>();

                //co.UseDashboard();

                co.UseRabbitMQ(mo =>
                {


                    var cfg = Configuration.GetSection("ABPSettings").Get<RabbitMQOptions>();
                    mo.HostName = cfg.HostName;
                    mo.UserName = cfg.UserName;
                    mo.Password = cfg.Password;
                    mo.Port = cfg.Port;
                    mo.VirtualHost = cfg.VirtualHost;
                    mo.ExchangeName = "cap.271Notes";
                });



            });


            services.AddControllers()
                    .AddNewtonsoftJson()
                    .AddJsonOptions(opt =>
                    {
                        opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    }); // Jsonnet支持




            //Swagger....
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "271笔记 API", Version = "v1" });
                var basePath = AppDomain.CurrentDomain.BaseDirectory; //AppContext.BaseDirectory;
                foreach (var name in Directory.GetFiles(basePath, "*.xml", SearchOption.AllDirectories))
                {
                    c.IncludeXmlComments(name);
                }
                c.OperationFilter<AuthorOperatorFilter>();
            });

            services.AddSwaggerGenNewtonsoftSupport();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            app.UseCors(policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/api/error");
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                string path = env.IsProduction() ? "/notes" : "";
                c.SwaggerEndpoint($"{path}/swagger/v1/swagger.json", "笔记Api V1");
            });
        }
    }
}
