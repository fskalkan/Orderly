using Microsoft.EntityFrameworkCore;
using Orderly.Application.Interfaces.Repositories;
using Orderly.Application.Interfaces;
using Orderly.Infrastructure;
using Orderly.Infrastructure.Data;
using Orderly.Infrastructure.Repositories;
using Scalar.AspNetCore;
using Orderly.API.Middlewares;
using FluentValidation;
using MediatR;
using Orderly.Application.Common.Behaviors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Orderly.Application.AssemblyReference).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(Orderly.Application.AssemblyReference).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
