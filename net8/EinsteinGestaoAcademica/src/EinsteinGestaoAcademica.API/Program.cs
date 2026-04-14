using EinsteinGestaoAcademica.API.Aplicacao.AlterarCurso;
using EinsteinGestaoAcademica.API.Aplicacao.CriarCurso;
using EinsteinGestaoAcademica.API.Aplicacao.RemoverCurso;
using EinsteinGestaoAcademica.API.Data;
using EinsteinGestaoAcademica.API.Data.Repositorios;
using EinsteinGestaoAcademica.API.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Adiciona suporte aos Controllers
builder.Services.AddControllers();

var configuration = builder.Configuration;


// 2. Configura o Swagger/OpenAPI
// Saiba mais em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Einstein Gestão Acadêmica WebAPI - .NET CORE 8",
        Version = "v1",
        Description = "API para gestão acadêmica do sistema Einstein"
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(configuration.GetValue<string>("Settings:CONNECTION_STRING"), o => o.UseRelationalNulls()));

builder.Services.AddTransient<ICriarCursoUseCase, CriarCursoUseCase>();
builder.Services.AddTransient<IAlterarCursoUseCase, AlterarCursoUseCase>();
builder.Services.AddTransient<IRemoverCursoUseCase, RemoverCursoUseCase>();
builder.Services.AddTransient<ICursoRepositorio, CursoRepositorio>();


var app = builder.Build();

// 3. Configura o pipeline de requisição HTTP
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // Define o endpoint do JSON e o nome que aparece no topo da página
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Einstein API v1");

    // Opcional: Define o Swagger como a página inicial (ao acessar raiz /)
    // options.RoutePrefix = string.Empty; 
});

app.UseAuthorization();

app.MapControllers();

app.Run();