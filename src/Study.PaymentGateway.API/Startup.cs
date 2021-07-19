namespace Study.PaymentGateway.API
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Study.PaymentGateway.API.Configurations;
    using Study.PaymentGateway.API.Filter;
    using Study.PaymentGateway.App.Services;
    using Study.PaymentGateway.App.Services.Interfaces;
    using Study.PaymentGateway.Domain.AcquiringBanksGateway;
    using Study.PaymentGateway.Domain.AcquiringBanksGateway.Factory;
    using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services;
    using Study.PaymentGateway.Domain.AcquiringBanksGateway.Services.GatewayConfig;
    using Study.PaymentGateway.Domain.Repository;
    using Study.PaymentGateway.Domain.Services;
    using Study.PaymentGateway.Domain.Services.Interfaces;
    using Study.PaymentGateway.Gateways.Configuration;
    using Study.PaymentGateway.Gateways.Executor;
    using Study.PaymentGateway.Gateways.Factory;
    using Study.PaymentGateway.Gateways.Gateways;
    using Study.PaymentGateway.Gateways.Services;
    using Study.PaymentGateway.Repository.MongoDB.Configuration;
    using Study.PaymentGateway.Repository.MongoDB.Configuration.Interfaces;
    using Study.PaymentGateway.Repository.MongoDB.Configuration.Settings;
    using Study.PaymentGateway.Repository.MongoDB.Configuration.Settings.Interfaces;
    using Study.PaymentGateway.Repository.MongoDB.Repository;

    [ExcludeFromCodeCoverageAttribute]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Study.PaymentGateway.API",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "My Company Description",
                            Email = "contact@contac.com",
                        },
                        Description = "API Gateway for OurComp associated Merchants"
                    });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Scoped
            services.AddScoped<IPaymentAppService, PaymentAppService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            //services.AddScoped<IBankGateways, BankGateways>();
            services.AddScoped<IVisaGateway, VisaGateway>();
            services.AddScoped<IGatewayServices, GatewayService>();

            // Singletons
            var mapper = new AutoMapperConfiguration();
            services.AddSingleton(mapper.Mapper);
            services.AddSingleton<IAPIExecutionService, APIExecutionService>();
            services.AddSingleton<IGatewayConfiguration, GatewayConfiguration>();
            services.AddSingleton<IBankAPI, BankAPI>();
            services.AddSingleton<IActions, Actions>();
            services.AddSingleton<IActionUris, ActionUris>();
            services.AddSingleton<IBankCredentials, BankCredentials>();
            services.AddSingleton<IBankFactory, BankFactory>();
            services.AddSingleton<IMongoDBConfiguration, MongoDBConfiguration>();

            // configurations that depends on appsettings.
            services.Configure<MongoDBSettings>(Configuration.GetSection(nameof(MongoDBSettings)));
            services.AddSingleton<IMongoDBSettings>(sp => sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            GatewayConfigurationFactory.SetGatewayConfiguration(services, Configuration);

            //services.Configure<List<IBankAPI>>(Configuration.GetSection("BankAPIs"));

            //var subSettings = Configuration.GetSection("BankAPIs").Get<List<IBankAPI>>();

            //var configurationSection = Configuration.GetSection("BankAPIs");
            // services.Configure<List<IBankAPI>>(configurationSection);
            //services.Configure<GatewayConfiguration>(Configuration.GetSection(nameof(GatewayConfiguration)));
            //services.AddSingleton<IGatewayConfiguration>(sp => sp.GetRequiredService<IOptions<GatewayConfiguration>>().Value);

            services.AddMvc(
                opt =>
                {
                    opt.Filters.Add(new ExceptionHandlerFilter());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Study.PaymentGateway.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}