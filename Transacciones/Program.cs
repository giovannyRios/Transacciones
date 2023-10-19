using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Unicode;
using Transacciones.Dominio.Context;
using Transacciones.Dominio.IoCRegister;
using Transacciones.Filters;
using Transacciones.Negocio.MappingEntities;
using Transacciones.Negocio.Services.Implements;
using Transacciones.Negocio.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Adiciona la cadena de conexion de la base de datos al contexto
builder.Services.AddDbContext<TransaccionesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDbTransacciones")),
    ServiceLifetime.Scoped
);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(
    options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };

    });


//Configure JWT in Swagger
builder.Services.AddSwaggerGen(c =>
{

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT de acceso",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });

});

//Inversion de control proyecto Transacciones.Dominio, Arquitectura cebolla
IoCRegister.GetConfiguration(builder.Services);
builder.Services.AddMemoryCache();

//Add Mapper
var config = new MapperConfig();
builder.Services.AddSingleton(config.getMappper());

//Inyecta las dependencias de los servicios a ser utilizados por los controladores del API
builder.Services.AddScoped<IGeneroService, GenerosServices>();
builder.Services.AddScoped<ITipoCuentaService, TipoCuentaService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ICuentaService, CuentaService>();
builder.Services.AddScoped<IMovimientoServices, MovimientoServices>();

//Add JWT Service
builder.Services.AddSingleton<IJwtService, JwtService>();

//Add Filters
builder.Services.AddTransient<ValidateJWTFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Habilita la autorización de Swagger
        c.OAuthClientId("swagger-client-id");
        c.OAuthClientSecret("swagger-client-secret");
        c.OAuthAppName("Swagger");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
