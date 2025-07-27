using MHplatform.Infrastructure.Data;
using MHplatform.Infrastructure.Repository;
using MHPlatform.Application.Interface;
using MHPlatform.Application.Mappings;
using MHPlatform.Application.Service;
using MHPlatform.Domain.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// ✅ Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(
        dbContextOptions => dbContextOptions.UseSqlServer(
            builder.Configuration["ConnectionStrings:DBConnectionString"], o => o.UseCompatibilityLevel(120)));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderFormService, OrderFormService>();


var app = builder.Build();

// ✅ Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use routing/middleware
app.UseAuthorization();
app.MapControllers();

app.Run();
