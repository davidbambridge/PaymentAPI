using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Services.Common.DTO;
using Services.Common.Validators;
using WebApi.Data;
using WebApi.Extensions;
using WebApi.Helpers;
using WebApi.Interfaces;

namespace WebApi
{
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
            services.AddControllers().AddNewtonsoftJson().AddFluentValidation();
            
            services.AddCustomHttpClientServices(Configuration);

            services.AddTransient<IValidator<PaymentItem>, PaymentItemValidator>();
            services.AddTransient<IValidator<Card>, CardValidator>();
            services.AddTransient<IValidator<Address>, AddressValidator>();
            services.AddTransient<IValidator<Customer>, CustomerValidator>();

            services.AddScoped<IPaymentHelper, PaymentHelper>();
            services.AddScoped<IDataAccess, DataAccess>();
            services.AddScoped<IMerchantHelper, MerchantHelper>();
            services.AddScoped<IPaymentControllerDbContext, PaymentControllerDbContextProvider>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment Processing API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Processing API");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
