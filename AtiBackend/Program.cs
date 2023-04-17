using Business.Abstract;
using Business.Concrete;
using Business.Validation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using FluentValidation.AspNetCore;
using Serilog.Events;
using Serilog;
using ExceptionHandling;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IdentityServer4.AccessTokenValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddControllers().AddFluentValidation(
    x => x.RegisterValidatorsFromAssemblyContaining<ProductValidator>());


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    //Token'ý yayýnlayan Auth Server adresi bildiriliyor. Yani yetkiyi daðýtan mekanizmanýn adresi bildirilerek ilgili API ile iliþkilendiriliyor.
                    options.Authority = "https://localhost:7084";
                    //Auth Server uygulamasýndaki '__' isimli resource ile bu API iliþkilendiriliyor.
                    options.Audience = "AtiBackend";
                });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ReadAccess", policy => policy.RequireClaim("scope", "AtiBackend.Read"));
    options.AddPolicy("WriteAccess", policy => policy.RequireClaim("scope", "AtiBackend.Write"));
});


builder.Services.AddLogging();
builder.Logging.AddSerilog();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Log/log.txt", LogEventLevel.Warning)
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


