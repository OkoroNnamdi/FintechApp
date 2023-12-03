using FinTech.DB;
using FinTech.DB.DTO;
using FinTechApi;
using FinTechApi.Extension;
using FinTechCore.Implementations;
using FinTechCore.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                .AddScoped<IUrlHelper>(x =>
                    x.GetRequiredService<IUrlHelperFactory>()
                        .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext));
// for entity framework
builder.Services.AddDbContext<AppDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
builder.Services.ConfigureMailService(config);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register Dependency Injection Service Extension
builder.Services.AddDependencyInjection();
// Configure Identity
builder.Services.ConfigureIdentity();

builder.Services.AddAuthentication();
// Add Jwt Authentication and Authorization
services.ConfigureAuthentication(config);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
