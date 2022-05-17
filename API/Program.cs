using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoffeeShopperConnection"));
});


var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.Run();
