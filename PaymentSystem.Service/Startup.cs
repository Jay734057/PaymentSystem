using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PaymentSystem.Business;
using PaymentSystem.Core.Interfaces.Business;
using PaymentSystem.Core.Interfaces.Repositories;
using PaymentSystem.Core.Models;
using PaymentSystem.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;

namespace PaymentSystem.Service
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
            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.IgnoreNullValues = true;
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddSwaggerDocument();

            services.AddDbContext<PaymentSystemContext>(opt => opt.UseInMemoryDatabase("Database"));


            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddScoped<IAccountBusiness, AccountBusiness>();
            services.AddScoped<IPaymentBusiness, PaymentBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();

            //seed data for manual test purpose.
            var context = serviceProvider.GetService<PaymentSystemContext>();

            SeedData(context);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void SeedData(PaymentSystemContext context)
        {
            try
            {
                //get data from json file and seed data in DB for test.
                var file = File.ReadAllText("DummyData/DummyData.json");
                var dummyData = JsonConvert.DeserializeObject<IEnumerable<Account>>(file);
                context.AddRange(dummyData);
                context.SaveChanges();
            }
            catch (Exception)
            {
                //not adding any data to DB if exceptions
                return;
            }
        }
    }
}
