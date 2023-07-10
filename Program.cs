global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using PortalNotas.Data;
global using PortalNotas.Models;
global using PortalNotas.Models.DTOs.Aluno;
global using PortalNotas.Models.DTOs.Materia;
global using PortalNotas.Services.AlunoService;
global using PortalNotas.Services.MateriaService;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortalNotasContext") ?? throw new InvalidOperationException("Connection string 'PortalNotasContext' not found.")));

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<IMateriaService, MateriaService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run();
