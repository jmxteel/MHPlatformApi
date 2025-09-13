using MHplatform.Infrastructure.Data;
using MHplatform.Infrastructure.Repository;
using MHPlatform.Api.Controller.SwaggerSecurity;
using MHPlatform.Application.Interface;
using MHPlatform.Application.Mappings;
using MHPlatform.Application.Service;
using MHPlatform.Domain.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .WithExposedHeaders("X-Pagination");
                      });
});

// Add services to the container
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Add Bearer Authentication in Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer <your-token>' in the field below."
    });

    // Apply Security Requirement to all endpoints that needs Authorization/ with [Authorize] annotation
    options.OperationFilter<AuthorizeCheckOperationFilter>();
});


// ✅ Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(
        dbContextOptions => dbContextOptions.UseSqlServer(
            builder.Configuration["ConnectionStrings:Main"], o => o.UseCompatibilityLevel(120)));

builder.Services.AddDbContext<DataContext>(
        dbContextOptions => dbContextOptions.UseSqlServer(
            builder.Configuration["ConnectionStrings:Auth"], o => o.UseCompatibilityLevel(120)));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderFormService, OrderFormService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", jwtBearerOptions =>
{
    jwtBearerOptions.TokenValidationParameters =
    new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:key"]!)),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtToken:issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtToken:audience"],

        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(Convert.ToInt32(builder.Configuration["JwtToken:minutestoexpiration"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    //NOTE claim key and values are case sensitive
    options.AddPolicy("CanAccessProducts", p =>
    p.RequireClaim("CanAccessProducts", "true"));
});

var app = builder.Build();

// ✅ Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger"; // This makes Swagger available at /swagger
    });
}

// Use routing/middleware

app.UseDefaultFiles(); // Enables default file like index.html
app.UseStaticFiles();  // Serves files from wwwroot
app.MapGet("/", context =>
{
    context.Response.Redirect("/custom/index.html", permanent: false);
    return Task.CompletedTask;
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
