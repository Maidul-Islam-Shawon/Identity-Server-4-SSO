using API.Services.Implementations;
using API.Services.Interfaces;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", options =>
    {
        options.Authority = "https://localhost:7281";
        options.ApiName = "CoffeeApi";

    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeShopperConnection"));
});

builder.Services.AddTransient<ICoffeeShopService, CoffeeShopService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();



var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//app.MapGet("/", () => "Hello World!");

app.Run();
