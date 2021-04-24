namespace Study.PaymentGateway.API
{
    using System.Diagnostics.CodeAnalysis;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Study.PaymentGateway.API.Configurations;
    using Study.PaymentGateway.API.Filter;
    using Study.PaymentGateway.App.Mapper;
    using Study.PaymentGateway.App.Services;
    using Study.PaymentGateway.App.Services.Interfaces;
    using Study.PaymentGateway.Domain.AcquiringBanksGateway;
    using Study.PaymentGateway.Domain.Repository;
    using Study.PaymentGateway.Domain.Services;
    using Study.PaymentGateway.Domain.Services.Interfaces;
    using Study.PaymentGateway.Gateways.Executor;
    using Study.PaymentGateway.Gateways.Executor.Interface;
    using Study.PaymentGateway.Repository.MongoDB.Configuration;
    using Study.PaymentGateway.Repository.MongoDB.Configuration.Interfaces;
    using Study.PaymentGateway.Repository.MongoDB.Configuration.Settings;
    using Study.PaymentGateway.Repository.MongoDB.Configuration.Settings.Interfaces;
    using Study.PaymentGateway.Repository.MongoDB.Mapper;
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Study.PaymentGateway.API", Version = "v1" });
            });

            services.AddScoped<IPaymentAppService, PaymentAppService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IBankGateways, BankGateways>();

            var mapper = new AutoMapperConfiguration();
            services.AddSingleton(mapper.Mapper);
            services.AddSingleton<IAPIExecutionService, APIExecutionService>();

            services.AddSingleton<IMongoDBConfiguration, MongoDBConfiguration>();
            services.Configure<MongoDBSettings>(Configuration.GetSection(nameof(MongoDBSettings)));
            services.AddSingleton<IMongoDBSettings>(sp => sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

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