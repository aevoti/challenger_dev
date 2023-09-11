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

// Configurando o Swagger para receber autentica��o nas rotas
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Forum Aevo",
        Version = "v1",
        Description = "Bem-vindo � documenta��o da API Forum Aevo. Aqui voc� encontrar� " +
                  "informa��es sobre os endpoints e como usar esta API. **Algumas rotas est�o " +
                  "protegidas por um Token que voc� pode solicitar nas rotas GET de usu�rio " +
                  "digitando seu email e o Id funciona como um token, insira-o no campo de authorize.**",
    });

    // Definir o par�metro de cabe�alho "Token" para autentica��o
    c.AddSecurityDefinition("Token", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira seu token de autentica��o no cabe�alho 'Token' e o Id (uuid do usu�rio)",
        Name = "Token",
        Type = SecuritySchemeType.ApiKey
    });

    // Adicionando exig�ncia de seguran�a para o token
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

// Configura��o de CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha Aplica��o v1");
    c.RoutePrefix = "";//definindo o swagger como p�gina principal
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
