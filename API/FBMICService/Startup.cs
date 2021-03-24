using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using FBMICService.Data;
using FBMICService.DataAccess.Data;
using FBMICService.DataAccess.Repository;
using FBMICService.DataAccess.Repository.IRepository;
using FBMICService.Helpers;
using FBMICService.Interfaces;
using FBMICService.Logging;
using FBMICService.Models;
using FBMICService.Services;
using FBMICService.Utility;
//using FBMICService.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FBMICService
{
    public class Startup
    {
        //private readonly ILogger<Startup> _logger;
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //public Startup(IConfiguration configuration, ILogger logger)
        //{
        //    Configuration = configuration;
        //    Logger = logger;
        //}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddControllers();
            services.AddApplicationInsightsTelemetry();

            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
            .AddAzureADBearer(options => Configuration.Bind("AzureActiveDirectory", options));


            services.AddCors();
            services.Configure<EmailOptions>(Configuration);
            services.AddSingleton<IEmailSender, EmailSender>();
            //services.AddSingleton<ILog, LogNLog>();
            //.AddSingleton(typeof(ILogger), logger);

            //services.AddScoped<IBranchRepository, BranchRepository>();
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.ConfigureExceptionHandler(logger);

            app.UseRouting();

            app.UseCors(m => m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
