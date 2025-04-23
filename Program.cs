
using BibliotecaCleanArch.Application.Services;
using BibliotecaCleanArch.Core.Interfaces;
using BibliotecaCleanArch.Infrastructure.Data;
using BibliotecaCleanArch.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<LivroService>();

var app = builder.Build();

// Middlewares
app.UseHttpsRedirection();
//app.UseSwagger();
//app.UseSwaggerUI();
app.MapControllers();

app.Run();


