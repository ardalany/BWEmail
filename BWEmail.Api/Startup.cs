using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BWEmail.Api.Services.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.Certificate;

namespace BWEmail.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Set up the correct email client based on the email provider.
            if(Configuration["EmailProvider"] == "Spendgrid") {
                services.AddHttpClient<IEmailClient, SpendgridClient>(client => {
                    client.BaseAddress = new Uri(Configuration["Spendgrid:BaseUrl"]);
                    client.DefaultRequestHeaders.Add("X-Api-Key", Configuration["Spendgrid:ApiKey"]);
                });
            } else {
                services.AddHttpClient<IEmailClient, SnailgunClient>(client => {
                    client.BaseAddress = new Uri(Configuration["Snailgun:BaseUrl"]);
                    client.DefaultRequestHeaders.Add("X-Api-Key", Configuration["Snailgun:ApiKey"]);
                });
            }

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BWEmail", Version = "v1" });
            });

            services.AddAuthentication(
                CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BWEmail v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
