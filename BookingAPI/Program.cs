using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            try { 
                var scope = host.Services.CreateScope();

                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                ctx.Database.EnsureCreated();

                var adminRole = new IdentityRole("Admin");
                if (!ctx.Roles.Any(r => r.Name == adminRole.Name))
                {
                    roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
                }

                if (!ctx.Users.Any(u => u.UserName == "admin"))
                {
                    var adminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "admin@test.com"
                    };
                    var result = userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                    userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
                }


                var customerRole = new IdentityRole("Customer"); 
                if(!ctx.Roles.Any(r => r.Name == customerRole.Name))
                {
                    roleManager.CreateAsync(customerRole).GetAwaiter().GetResult();
                }

                if (!ctx.Users.Any(u => u.UserName == "customer"))
                {
                    var customerUser = new IdentityUser
                    {
                        UserName = "customer",
                        Email = "customer@test.com"
                    };
                    var result = userManager.CreateAsync(customerUser, "password").GetAwaiter().GetResult();
                    userManager.AddToRoleAsync(customerUser, customerRole.Name).GetAwaiter().GetResult();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
