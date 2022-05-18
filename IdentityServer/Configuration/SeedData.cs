using IdentityModel;
using IdentityServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IdentityServer.Configuration
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();

            services.AddDbContext<AspNetIdentityDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AspNetIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddOperationalDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
            });

            services.AddConfigurationDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseSqlServer(connectionString,
                    sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
            });


            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

            var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            context.Database.Migrate();
            EnsureSeedData(context);

            var ctx = scope.ServiceProvider.GetService<AspNetIdentityDbContext>();
            ctx.Database.Migrate();
            EnsureUsers(scope);


        }

        public static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients.ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var identityResouse in Config.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(identityResouse.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var apiScopes in Config.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(apiScopes.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var apiResource in Config.ApiResources.ToList())
                {
                    context.ApiResources.Add(apiResource.ToEntity());
                }
                context.SaveChanges();
            }

        }

        private static void EnsureUsers(IServiceScope scope)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var maidul = userManager.FindByNameAsync("maidul").Result;
            if (maidul == null)
            {
                maidul = new IdentityUser
                {
                    UserName = "maidul",
                    Email = "maidul.bd@asa-internation.com",
                    EmailConfirmed = true
                };

                var result = userManager.CreateAsync(maidul, "Pass123$").Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userManager.AddClaimsAsync(
                    maidul,
                    new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, "Maidul Islam"),
                        new Claim(JwtClaimTypes.GivenName, "Maidul"),
                        new Claim(JwtClaimTypes.FamilyName, "Islam"),
                        new Claim(JwtClaimTypes.WebSite,"https://maidul-islam.net"),
                        new Claim("location","Jatrabari")
                    }).Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }
    }
}
