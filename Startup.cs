using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotenv.net.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using tp1_restaurant.Areas.Identity.Data;
using tp1_restaurant.Data;
using tp1_restaurant.Services;

namespace tp1_restaurant
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection servicesCollection)
        {
            servicesCollection.AddDbContext<ZhaoContext>(options =>
            options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            servicesCollection.AddControllersWithViews();
            servicesCollection.AddSingleton<EnvReader>(new EnvReader());
            servicesCollection.AddTransient<ReservationData>();
            servicesCollection.AddTransient<EvaluationData>();
            servicesCollection.AddTransient<PromotionData>();
            servicesCollection.AddTransient<EmailService>();
            servicesCollection.AddDistributedMemoryCache();
            servicesCollection.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
            });

            servicesCollection.Configure<IdentityOptions>(options =>
            {
                // Configuration des parametres du mot de passe.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1; 
                // Configuration du verrouillage de compte.
                options. Lockout . DefaultLockoutTimeSpan = TimeSpan . FromMinutes (5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true; 
                // Configuration des parametres utilisateur.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false; 

            });

            servicesCollection.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            servicesCollection.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("Administrateur"));
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

            });

            servicesCollection.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            ZhaoContext context = services.GetRequiredService<ZhaoContext>();
            DbInitializer.Initialize(context);

            CreateRoles(services).Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ZhaoContext>())
                {
                    context.Database.Migrate();
                }
                using (var context = serviceScope.ServiceProvider.GetService<IdentityDataContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Administrateur", "Utilisateur" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser user = await UserManager.FindByEmailAsync("courrielTI@cstjean.qc.ca");

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "courrielTI@cstjean.qc.ca",
                    Email = "courrielTI@cstjean.qc.ca",
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };
                await UserManager.CreateAsync(user, "2c3P@ssw0rd");
            }
            await UserManager.AddToRoleAsync(user, "Administrateur");
        }
    }
}
