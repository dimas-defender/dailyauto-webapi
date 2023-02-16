using DailyAuto.Controllers;
using DailyAuto.Database;
using DailyAuto.Repositories.Implementation;
using DailyAuto.Repositories.Interfaces;
using DailyAuto.Services.Implementation;
using DailyAuto.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ICarsRepository, CarsRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ICarsService, CarsService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    { 
        Title = "DailyAuto API",
        Version = "v1",
        Description = "Web API for car rent app"
    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
