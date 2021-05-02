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
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FitnessWebApp.Services;
using System.Linq;
using Microsoft.AspNetCore.Http;

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
            
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddDbContext<AppDbContext>(options => options.UseMySql("server=20.52.143.162;user=vasakot;password=Svetlana2001_;database=fitnesswebapp;port=3306;Connect Timeout=20;"));
            //services.AddDbContext<AppDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Default")));
            //����������� identity �������
            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.SignIn.RequireConfirmedEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireDigit = true;
                opts.Tokens.PasswordResetTokenProvider = ResetPasswordTokenProvider.ProviderKey;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders().AddTokenProvider<ResetPasswordTokenProvider>(ResetPasswordTokenProvider.ProviderKey);
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "fitnessWebApp";
                options.Cookie.HttpOnly = false;
                options.LoginPath = "/api/login";
                options.AccessDeniedPath = "/";
                options.LogoutPath = "/api/Logout";
                options.SlidingExpiration = true;
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                options.ExpireTimeSpan= TimeSpan.FromHours(24);
                
                
                

                // options.Cookie.Expiration= TimeSpan.FromMinutes(20);


            });
           /* services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = false;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                            // ��������, ����� �� �������������� �������� ��� ��������� ������
                            ValidateIssuer = true,
                            // ������, �������������� ��������
                            ValidIssuer = AuthOptions.ISSUER,

                            // ����� �� �������������� ����������� ������
                            ValidateAudience = true,
                            // ��������� ����������� ������
                            ValidAudience = AuthOptions.AUDIENCE,
                            // ����� �� �������������� ����� �������������
                            ValidateLifetime = true,

                            // ��������� ����� ������������
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // ��������� ����� ������������
                            ValidateIssuerSigningKey = true,
                       };
                   });*/
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
             app.UseSession();
         

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials()); // allow credentials
             /* app.Run(async (context) =>
            {
                if (context.Session.Keys.Contains("name"))
                    //await context.Response.WriteAsync($"Hello {context.Session.GetString("value")}!");
                await context.Response.StartAsync();
                else
                {
                    context.Session.SetString("name", "Tom");
                    await context.Response.;
                }
            });*/
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
