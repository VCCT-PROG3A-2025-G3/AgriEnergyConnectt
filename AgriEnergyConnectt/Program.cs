using AgriEnergyConnectt.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnectt
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin", "Employee", "Farmer" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }

                } 
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var users = new[]
                {
        new { Email = "admin@aec.com", Password = "Admin1234!", Role = "Admin" },
        new { Email = "farmer1@aec.com", Password = "Farmer1234!", Role = "Farmer" },
        new { Email = "farmer2@aec.com", Password = "Farmer1234!", Role = "Farmer" },
        new { Email = "employee1@aec.com", Password = "Employee1234!", Role = "Employee" },
        new { Email = "employee2@aec.com", Password = "Employee1234!", Role = "Employee" },
    };

                foreach (var userInfo in users)
                {
                    if (await userManager.FindByEmailAsync(userInfo.Email) == null)
                    {
                        var user = new IdentityUser
                        {
                            UserName = userInfo.Email,
                            Email = userInfo.Email,
                            EmailConfirmed = true
                        };

                        var result = await userManager.CreateAsync(user, userInfo.Password);

                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, userInfo.Role);
                        }
                        else
                        {
                            // Log or handle errors here (e.g., weak password, etc.)
                            foreach (var error in result.Errors)
                            {
                                Console.WriteLine($"Error creating user {userInfo.Email}: {error.Description}");
                            }
                        }
                    }
                }
            }


            app.Run();
        }
    }
}
