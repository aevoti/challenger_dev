using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.Development.json")
    .Build();

builder.Services.AddDbContext<ForumAEVO.Models.Context>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMvc()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    });

// Configurando o Swagger para receber autenticação nas rotas
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Forum Aevo",
        Version = "v1",
        Description = "Bem-vindo à documentação da API Forum Aevo. Aqui você encontrará " +
                  "informações sobre os endpoints e como usar esta API. **Algumas rotas estão " +
                  "protegidas por um Token que você pode solicitar nas rotas GET de usuário " +
                  "digitando seu email e o Id funciona como um token, insira-o no campo de authorize.**",
    });

    // Definir o parâmetro de cabeçalho "Token" para autenticação
    c.AddSecurityDefinition("Token", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira seu token de autenticação no cabeçalho 'Token' e o Id (uuid do usuário)",
        Name = "Token",
        Type = SecuritySchemeType.ApiKey
    });

    // Adicionando exigência de segurança para o token
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Token"
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configuração de CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha Aplicação v1");
    c.RoutePrefix = "";//definindo o swagger como página principal
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
