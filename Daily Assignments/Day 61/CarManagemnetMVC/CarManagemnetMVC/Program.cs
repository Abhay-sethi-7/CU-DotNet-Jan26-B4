using CarManagemnetMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarManagemnetMVC
{
    public class Program
    {
        public static async Task Main(string[] args) 
        {
            var builder = WebApplication.CreateBuilder(args);

            // DB for Car
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CarManagemnetMVCContext")));

            // Identity DB
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>() 
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            //  Ensure databases are migrated and create roles + users inside ONE scope
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    // Apply pending migrations for both contexts
                    var appDb = services.GetRequiredService<ApplicationDbContext>();
                    var carDb = services.GetRequiredService<ApplicationDbContext>();
                    await appDb.Database.MigrateAsync();
                    await carDb.Database.MigrateAsync();

                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                    string[] roles = { "Admin", "Customer", "User" };

                    // Create roles
                    foreach (var role in roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role))
                        {
                            var rResult = await roleManager.CreateAsync(new IdentityRole(role));
                            if (!rResult.Succeeded)
                            {
                                logger.LogWarning("Failed to create role {Role}: {Errors}", role, string.Join(", ", rResult.Errors.Select(e => e.Description)));
                            }
                        }
                    }

                    // Create users
                    async Task CreateUser(string email, string role)
                    {
                        var user = await userManager.FindByEmailAsync(email);

                        if (user == null)
                        {
                            user = new IdentityUser
                            {
                                UserName = email,
                                Email = email,
                                EmailConfirmed = true
                            };

                            var createResult = await userManager.CreateAsync(user, "Password@123");
                            if (!createResult.Succeeded)
                            {
                                logger.LogWarning("Failed to create user {Email}: {Errors}", email, string.Join(", ", createResult.Errors.Select(e => e.Description)));
                                return;
                            }

                            var addRoleResult = await userManager.AddToRoleAsync(user, role);
                            if (!addRoleResult.Succeeded)
                            {
                                logger.LogWarning("Failed to add user {Email} to role {Role}: {Errors}", email, role, string.Join(", ", addRoleResult.Errors.Select(e => e.Description)));
                            }
                        }
                    }

                    await CreateUser("admin@test.com", "Admin");
                    await CreateUser("customer@test.com", "Customer");
                    await CreateUser("user@test.com", "User");
                }
                catch (Exception ex)
                {
                    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                    var log = loggerFactory.CreateLogger<Program>();
                    log.LogError(ex, "An error occurred while migrating or seeding the database.");
                    throw;
                }
            }

            // Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // ✅ MUST ADD
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            await app.RunAsync();
        }
    }
}