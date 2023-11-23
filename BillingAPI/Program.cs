using BillingAPI.Models;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//string Connect = builder.Configuration.GetConnectionString("DefaultConnection");
string Connect = builder.Configuration.GetConnectionString("ExternalConnection");
builder.Services.AddDbContext<Context>(options =>
{
    // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExternalConnection"));
});


builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("V");
});


builder.Services.AddCors(options =>
{
    var FrontEndUrl = builder.Configuration.GetValue<string>("frontend_url");
    options.AddPolicy("policies", app =>
    {
        app.WithOrigins(FrontEndUrl).AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed((host) => true);
    });
     
});

var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseCors("policies");
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
