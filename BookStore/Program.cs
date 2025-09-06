using System.Text;  
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
var cfg = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = cfg["Jwt:Key"]
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});


// ------------------ JWT ------------------
builder.Services.AddSingleton<JwtService>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        var key = cfg["Jwt:Key"] ?? throw new Exception("Missing Jwt:Key");
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,               // ตรวจสอบ Issuer
            ValidateAudience = true,             // ตรวจสอบ Audience
            ValidateIssuerSigningKey = true,     // ตรวจสอบ Signature
            ValidIssuer = cfg["Jwt:Issuer"],
            ValidAudience = cfg["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

builder.Services.AddAuthorization();

// ------------------ HttpClient ------------------
// สำหรับเรียก https://api.itbook.store
builder.Services.AddHttpClient<BookStoreClient>();
     
// ------------------ Controllers & Swagger ------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ------------------ Middleware ------------------.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();  
app.UseAuthorization();

app.MapControllers();

app.Run();
