using API.Models;
using API.Services.Implementations;
using API.Services.Interfaces;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var identityConfiguration = 
    builder.Configuration.GetSection("IdentityServerConfiguration").Get<IdentityServerConfiguration>();

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

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = identityConfiguration.ApiDisplayName, Version = "v1" });
});

builder.Services.AddControllers();



var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

//app.MapGet("/", () => "Hello World!");

app.Run();
