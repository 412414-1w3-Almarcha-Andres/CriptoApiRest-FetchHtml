using Microsoft.EntityFrameworkCore;
using ParcialWebApi.Models;
using ParcialWebApi.Repositories;
using ParcialWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CriptoContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// agregar inyeccion de dependencias MUY IMPORTANTE ! 
builder.Services.AddScoped<ICriptoRepository, CriptoRepository>();
builder.Services.AddScoped<ICriptoServices, CriptoServices>();





//***PROBANDO CORS PARA TRAER MEDIANTE JAVASCRIPT DESDE OTRO ORIGEN HTTP EN ESTE CASO DESDE EL FRONTEND COMO HTTP://LOCALHOST:3000
// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//**
// Usar CORS
// Usar CORS
app.UseCors("AllowLocalhost");




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
