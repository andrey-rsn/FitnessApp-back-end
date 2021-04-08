using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FitnessWebApp.Domain;
using Microsoft.OpenApi.Models;
using FitnessWebApp.Models;

namespace FitnessWebApp
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
            //services.AddDbContext<AppDbContext>(options =>
            // options.UseSqlServer("Server=DESKTOP-FHIPLI2;Database=fitnessapplication;Trusted_Connection=True;MultipleActiveResultSets=true;"));
            services.AddDbContext<AppDbContext>(options => options.UseMySql("server=20.52.143.162;user=vasakot;password=Svetlana2001_;database=fitnessapp;port=3306;Connect Timeout=20;"));
            // services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            //настраиваем identity систему
            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.SignIn.RequireConfirmedEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireDigit = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });
            services.AddControllersWithViews();
           /* services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FitnessWebApp", Version = "v1" });
            });*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitnessWebApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller=TrainingPlan}/{action=GetPlans}");
            });
        }
    }
}
