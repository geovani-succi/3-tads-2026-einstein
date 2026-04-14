var builder = WebApplication.CreateBuilder(args);

// 1. Adiciona suporte aos Controllers
builder.Services.AddControllers();

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