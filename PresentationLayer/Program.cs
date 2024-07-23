using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TiacPraksaP1.Services.Interfaces;
using TiacPraksaP1.Services.Service;
using TiacPraksaP1.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Products.Exceptions;
using Products.Exceptions.CustomExceptions;
using TiacPraksaP1.Models;
using TiacPraksaP1.Data;
using DataAccessLayer.Repository.Repositories;
using DataAccessLayer.Repositories.Interfaces;
using DataAccessLayer.Repository.Interfaces;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using BusinessLogicLayer.Services.Service;
using BusinessLogicLayer.Validators;


var builder = WebApplication.CreateBuilder(args);
string _cors = "cors";

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Products", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v2"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddIdentity<User,IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(options => {
    options.AddPolicy(name: _cors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ProductValidator, ProductValidator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserValidator, UserValidator>();
builder.Services.AddScoped<IUserProductsRepository, UserProductsRepository>();
builder.Services.AddScoped<IUserProductService, UserProductService>();
builder.Services.AddScoped<UserProductValidator, UserProductValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddExceptionHandler<AppExceptionHandler>();
builder.Services.AddExceptionHandler<GeneralExceptionHandler>();
builder.Services.AddProblemDetails();
var app = builder.Build();

app.UseExceptionHandler(_ => { });
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/throw", (_) => throw new Exception());
app.MapGet("/throwNotFound", (_) => throw new NotFoundException());
app.MapGet("/throwForbid", (_) => throw new ForbidException());

app.Run();
