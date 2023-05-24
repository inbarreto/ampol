using Ampol.Persistence.Infrastructure;
using Application.CalculatePurchase.Command;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PurchaseCommandHandler).GetTypeInfo().Assembly));

builder.Services.AddDbContext<AmpolDbContext>(options =>
options.UseInMemoryDatabase("Ampol")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
AmpolDbContext dbcontext = scope.ServiceProvider.GetRequiredService<AmpolDbContext>();
dbcontext.Database.EnsureCreated();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();