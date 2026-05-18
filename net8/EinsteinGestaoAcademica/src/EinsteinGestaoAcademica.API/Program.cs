
using System.Text;
using EinsteinGestaoAcademica.Aplicacao.Services.Alunos.CriarAluno;
using EinsteinGestaoAcademica.Aplicacao.Services.Cursos.AlterarCurso;
using EinsteinGestaoAcademica.Aplicacao.Services.Cursos.CriarCurso;
using EinsteinGestaoAcademica.Aplicacao.Services.Cursos.RemoverCurso;
using EinsteinGestaoAcademica.Aplicacao.Services.Token;
using EinsteinGestaoAcademica.Aplicacao.Services.Usuarios.CriarUsuario;
using EinsteinGestaoAcademica.Aplicacao.Services.Usuarios.RealizarLogin;
using EinsteinGestaoAcademica.Dados.Data;
using EinsteinGestaoAcademica.Dados.Data.Repositorios;
using EinsteinGestaoAcademica.Dominio;
using EinsteinGestaoAcademica.Dominio.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

    // Adiciona suporte ao Bearer Token no Swagger
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Insira o token JWT"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(configuration.GetValue<string>("Settings:CONNECTION_STRING"), o => o.UseRelationalNulls()));

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });


builder.Services.AddTransient<ICriarCursoUseCase, CriarCursoUseCase>();
builder.Services.AddTransient<IAlterarCursoUseCase, AlterarCursoUseCase>();
builder.Services.AddTransient<IRemoverCursoUseCase, RemoverCursoUseCase>();
builder.Services.AddTransient<ICursoRepositorio, CursoRepositorio>();

builder.Services.AddTransient<ICriarAlunoUseCase, CriarAlunoUseCase>();
builder.Services.AddTransient<IAlunoRepository, AlunoRepository>();

builder.Services.AddTransient<ICriarUsuarioUseCase, CriarUsuarioUseCase>();
builder.Services.AddTransient<IRealizarLoginUseCase, RealizarLoginUseCase>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddTransient<ITokenService, TokenService>();


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();