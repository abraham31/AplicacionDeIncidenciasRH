using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using WEB_API.Helpers;
using Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped<ISolicitudVacacionesRepository, SolicitudVacacionesRepository>();
builder.Services.AddScoped<ISolicitudPermisoRepository, SolicitudPermisosRepository>();
builder.Services.AddScoped<IInformeQuejaRepository, InformeQuejaRepository>();



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
