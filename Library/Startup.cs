using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Library.Data;
using Microsoft.EntityFrameworkCore;
using Library.Core;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Library.Data.IRepositories;
using System;
using Library.Core.Services;
using Library.Core.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using Library.Data.Repository;
using Library.Data.Helpers;

namespace Library
{
    public class Startup
    {
        // IContainer instance in the Startup class 
        private static IContainer ApplicationContainer { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Microservices API",
                    Description = "A simple Api project with .NET Core"
                });
                //c.IncludeXmlComments(xmlPath);
            });

            services.AddApplicationInsightsTelemetry(Configuration);
            //Config context
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LibraryConnection")));
            //IoC
            CreateDependencyInjection(services);
            //Initialize Mapping
            MappingConfig.Initialize();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void CreateDependencyInjection(IServiceCollection services)
        {
            // create a Autofac container builder
            ContainerBuilder builder = new ContainerBuilder();

            // read service collection to Autofac
            builder.Populate(services);

            // use and configure Autofac
            builder.RegisterType<Context>().As<IQueryableUnitOfWork>().WithParameter("schema", Configuration.GetConnectionString("SchemaName"));
            builder.RegisterType<LoggerHelper>().As<ILoggerHelper>();
            builder.RegisterType<BookService>().As<IBookService>();
            builder.RegisterType<BookRepository>().As<IBookRepository>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            // build the Autofac container
            ApplicationContainer = builder.Build();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Books}/{action=GettAll}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Books", action = "GetAll" });
            });

            app.UseSwagger();


            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
