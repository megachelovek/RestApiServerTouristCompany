﻿using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ServerTouristCompanyApi.Configuration;
using ServerTouristCompanyApi.Controllers;
using ServerTouristCompanyApi.Middleware;
using ServerTouristCompanyApi.Services;
using ServerTouristCompanyApi.SwaggerExamples;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace ServerTouristCompanyApi
{
    /// <summary>
    ///     OWIN configuration and setup.
    /// </summary>
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        /// <summary>
        ///     Initializes new instance of <see cref="Startup" />
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var configuration =
                new ConfigurationBuilder()
                    .SetBasePath(_env.ContentRootPath)
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", true, true)
                    .AddEnvironmentVariables()
                    .Build();

            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
            services.AddLogging();

            // Register your types
            services.AddTransient<ITourService, TourService>();
            services.AddTransient<ITransferService, TransferService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<ITicketService, TicketService>();

            // Refer to this article if you require more information on CORS
            // https://docs.microsoft.com/en-us/aspnet/core/security/cors
            void build(CorsPolicyBuilder b)
            {
                b.WithOrigins("*").WithMethods("*").WithHeaders("*").AllowCredentials().Build();
            }

            services.AddCors(options => { options.AddPolicy("AllowAllPolicy", build); });

            ///JWT Bearer
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,

                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true
                    };
                });
            ///JWT Bearer

            services
                .AddMvc(
                    options =>
                    {
                        // Refer to this article for more details on how to properly set the caching for your needs
                        // https://docs.microsoft.com/en-us/aspnet/core/performance/caching/response
                        options.CacheProfiles.Add(
                            "default",
                            new CacheProfile
                            {
                                Duration = 600,
                                Location = ResponseCacheLocation.None
                            });

                        //var jsonInputFormatter = options.InputFormatters.OfType<JsonInputFormatter>().First();
                        //jsonInputFormatter.SupportedMediaTypes.Add("multipart/form-data");
                    })
                .AddJsonOptions(options => { options.SerializerSettings.Formatting = Formatting.Indented; });

            services.AddResponseCaching();

            services.AddSwaggerExamplesFromAssemblyOf<TourRequestExample>();
            services.AddSwaggerExamplesFromAssemblyOf<TicketRequestExample>();
            services.AddSwaggerExamplesFromAssemblyOf<ReservationRequestExample>();
            services.AddSwaggerExamplesFromAssemblyOf<TransferRequestExample>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "My API",
                    Description = "My API",
                    TermsOfService = "None",
                    Contact = new Contact {Name = "Name Surname", Email = "email@gmail.com", Url = ""}
                });

                c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                    $"{PlatformServices.Default.Application.ApplicationName}.xml"));
                c.DescribeAllEnumsAsStrings();

                c.ExampleFilters();
                c.OperationFilter<AddFileParamTypesOperationFilter>();
            });

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(
                    async context =>
                    {
                        var loggerFactory = app.ApplicationServices.GetService<ILogger>();

                        context.Request.EnableRewind();
                        context.Request.Body.Position = 0;

                        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                        {
                            var body = await reader.ReadToEndAsync().ConfigureAwait(false);
                            var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();

                            loggerFactory.LogError(exceptionHandler.Error, "Error: {0}. Request: {1}",
                                exceptionHandler.Error.Message, body);
                        }
                    });
            });

            app.UseMiddleware<RequestValidationMiddleware>();
            app.UseCors("AllowAllPolicy");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}