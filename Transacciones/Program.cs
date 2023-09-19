using Microsoft.EntityFrameworkCore;
using Transacciones.Dominio.Context;
using Transacciones.Dominio.Repository.Implements;
using Transacciones.Dominio.Repository.Interfaces;
using Transacciones.Negocio.MappingEntities;
using Transacciones.Negocio.Services.Implements;
using Transacciones.Negocio.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adiciona la cadena de conexion de la base de datos al contexto
builder.Services.AddDbContext<TransaccionesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDbTransacciones")),
    ServiceLifetime.Scoped
);

//Inyecta las dependencias de los repositorios para el proyecto Transacciones.Negocio
builder.Services.AddScoped<IclienteRepository,ClienteRepository>();
builder.Services.AddScoped<IcuentaRepository,CuentaRepository>();
builder.Services.AddScoped<IMovimientosRepository,MovimientoRepository>();
builder.Services.AddScoped<ITipoCuentaRepository,TipoCuentaRepository>();
builder.Services.AddScoped<IGeneroRepository,GeneroRepository>();

//Add Mapper
var config = new MapperConfig();
builder.Services.AddSingleton(config.getMappper());

//Inyecta las dependencias de los servicios a ser utilizados por los controladores del API
builder.Services.AddScoped<IGeneroService, GenerosServices>();
builder.Services.AddScoped<ITipoCuentaService,TipoCuentaService>();
builder.Services.AddScoped<IClienteService,ClienteService>();
builder.Services.AddScoped<ICuentaService,CuentaService>();
builder.Services.AddScoped<IMovimientoServices,MovimientoServices>();





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
