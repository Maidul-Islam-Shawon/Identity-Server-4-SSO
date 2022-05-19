using IdentityServer.Configuration;
using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var seed = args.Contains("/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly.GetName().Name;
var CoffeeShopperConnection = builder.Configuration.GetConnectionString("CoffeeShopperConnection");


if (seed)
{
    SeedData.EnsureSeedData(CoffeeShopperConnection);
}


builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
{
    options.UseSqlServer(CoffeeShopperConnection,
        opt => opt.MigrationsAssembly(assembly));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AspNetIdentityDbContext>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = c => c.UseSqlServer(CoffeeShopperConnection,
            opt => opt.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = c => c.UseSqlServer(CoffeeShopperConnection,
            opt => opt.MigrationsAssembly(assembly));

        options.EnableTokenCleanup = true;
        options.TokenCleanupInterval = 30;
    })
    .AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();


var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();

app.UseIdentityServer();

//app.UseAuthentication();

app.UseAuthorization();

//app.MapGet("/", () => "Hello World!");

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
