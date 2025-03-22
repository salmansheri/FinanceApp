using Backend.Data;
using Backend.Repositories;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Backend.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

    });

// Identity 
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
}).AddEntityFrameworkStores<ApplicationDbContext>();

// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme();
//     options.DefaultChallengeScheme();
//     options.DefaultForbidScheme()
// }).AddJwtBearer();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))

);

builder.Services.AddScoped<IStockInterface, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
