using ManterCursosApi.Data;
using ManterCursosApi.Repository;
using ManterCursosApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//Injeçào de Dependencia dos repositorios
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();

builder.Services.AddDbContext<DataContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

//builder.Services.AddDbContext<DataContext>(op => op.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x =>
    x.AllowAnyHeader().
      AllowAnyOrigin().
      AllowAnyMethod()
    );
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
