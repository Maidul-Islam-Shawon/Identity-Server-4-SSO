using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly.GetName().Name;
var CoffeeShopperConnection = builder.Configuration.GetConnectionString("CoffeeShopperConnection");

builder.Services.AddIdentityServer()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = c => c.UseSqlServer(CoffeeShopperConnection,
            opt => opt.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = c => c.UseSqlServer(CoffeeShopperConnection,
            opt => opt.MigrationsAssembly(assembly));
    })
    .AddDeveloperSigningCredential();


var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseIdentityServer();

app.Run();
