using backend.Configurations;
using backend.Data;
using backend.Interfaces;
using backend.Repositories;
using backend.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//parte nosql Mongodb
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<MongoDbService>();

//repositorios dependency inversion solid
builder.Services.AddScoped<IPacientesRepository, PacientesRepository>();
builder.Services.AddScoped<IConsultasRepository, ConsultasRepository>();

//servicos
builder.Services.AddScoped<PacientesService>();
builder.Services.AddScoped<ConsultasService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//swagger feito com informações completas
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "hospital API",
        Version = "v1",
        Description = "API REST para gerenciameno de pacientes e consultas hospitalares.",
        Contact = new OpenApiContact
        {
            Name = "Hospital System",
            Email = "contatp@hospital.com"
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);
});

//cors pro frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hospital API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("FrontendPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
