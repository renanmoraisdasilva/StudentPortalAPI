global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
using ProfessorPortalAPI.Services.ProfessorService;
using StudentPortalAPI.Data;
using StudentPortalAPI.Services.CourseService;
using StudentPortalAPI.Services.ProfessorService;
using StudentPortalAPI.Services.StudentService;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortalNotasContext")
        ?? throw new InvalidOperationException("Connection string 'PortalNotasContext' not found."));
});

builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add Identity Framework services
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortalNotasContext")
        ?? throw new InvalidOperationException("Connection string 'PortalNotasContext' not found."));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();


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
