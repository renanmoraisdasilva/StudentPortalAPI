global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using PortalNotas.Data;
global using PortalNotas.Models;
global using PortalNotas.Models.DTOs.Aluno;
global using PortalNotas.Services.AlunoService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortalNotasContext") ?? throw new InvalidOperationException("Connection string 'PortalNotasContext' not found.")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IAlunoService, AlunoService>();

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
