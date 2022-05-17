using API.Services.Implementations;
using API.Services.Interfaces;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeShopperConnection"));
});

builder.Services.AddTransient<ICoffeeShopService, CoffeeShopService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();



var app = builder.Build();

app.MapControllers();

//app.MapGet("/", () => "Hello World!");

app.Run();
