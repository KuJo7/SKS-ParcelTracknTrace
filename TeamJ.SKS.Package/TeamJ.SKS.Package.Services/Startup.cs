/*
 * Parcel Logistics Service
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.20.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using TeamJ.SKS.Package.Services.Filters;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using TeamJ.SKS.Package.BusinessLogic.DTOs;
using TeamJ.SKS.Package.BusinessLogic.DTOs.Validators;
using Microsoft.EntityFrameworkCore;
using TeamJ.SKS.Package.DataAccess.Interfaces;
using TeamJ.SKS.Package.BusinessLogic.Interfaces;
using TeamJ.SKS.Package.Services.DTOs.MapperProfiles;
using TeamJ.SKS.Package.ServiceAgents.Interfaces;

namespace TeamJ.SKS.Package.Services
{
    /// <summary>
    /// Startup
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnv;

        private IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _hostingEnv = env;
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            //AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfiles());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();

            //Validator
            services.AddMvc(setup => { }).AddFluentValidation();
            services.AddTransient<IValidator<BLParcel>, BLParcelValidator>();
            services.AddTransient<IValidator<BLWarehouse>, BLWarehouseValidator>();
            services.AddTransient<IValidator<BLHopArrival>, BLHopArrivalValidator>();
            services.AddTransient<IValidator<BLRecipient>, BLRecipientValidator>();
            services.AddTransient<IValidator<BLWarehouseNextHops>, BLWarehouseNextHopsValidator>();

            //DBContext
            services.AddDbContext<DataAccess.Sql.Context>( opt =>
                {
                    opt.UseSqlServer(Configuration.GetConnectionString("ParcelTracknTraceDb"),
                                                        p => p.UseNetTopologySuite().EnableRetryOnFailure());
                    opt.EnableSensitiveDataLogging(true);
                    opt.EnableDetailedErrors(true);
                });


            services.AddScoped<IContext, DataAccess.Sql.Context>(provider => provider.GetRequiredService<DataAccess.Sql.Context>());

            //DI
            services.AddTransient<IParcelLogic, BusinessLogic.ParcelLogic>();
            services.AddTransient<IHopLogic, BusinessLogic.HopLogic>();

            services.AddScoped<IHopRepository, DataAccess.Sql.SqlHopRepository>();
            services.AddScoped<IParcelRepository, DataAccess.Sql.SqlParcelRepository>();

            services.AddScoped<IGeoEncodingAgent, ServiceAgents.OpenStreetMapEncodingAgent>();


            // Add framework services.
            services
                .AddMvc(options =>
                {
                    options.InputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>();
                    options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter>();
                })
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
                })
                .AddXmlSerializerFormatters();


            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("1.20.1", new OpenApiInfo
                    {
                        Version = "1.20.1",
                        Title = "Parcel Logistics Service",
                        Description = "Parcel Logistics Service (ASP.NET Core 3.1)",
                        Contact = new OpenApiContact()
                        {
                           Name = "SKS",
                           Url = new Uri("http://www.technikum-wien.at/"),
                           Email = ""
                        },
                    });
                    c.CustomSchemaIds(type => type.FullName);
                    c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");

                    // Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
                    // Use [ValidateModelState] on Actions to actually validate it in C# as well!
                    c.OperationFilter<GeneratePathParamsValidationFilter>();
                });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="context"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, DataAccess.Sql.Context context)
        {
            //context.Database.Migrate();

            app.UseRouting();

            //TODO: Uncomment this if you need wwwroot folder
            // app.UseStaticFiles();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //TODO: Either use the SwaggerGen generated Swagger contract (generated from C# classes)
                c.SwaggerEndpoint("/swagger/1.20.1/swagger.json", "Parcel Logistics Service");

                //TODO: Or alternatively use the original Swagger contract that's included in the static files
                // c.SwaggerEndpoint("/swagger-original.json", "Parcel Logistics Service Original");
            });

            //TODO: Use Https Redirection
            // app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //TODO: Enable production exception handling (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling)
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }
        }
    }
}
