using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ForumContext>(options => { options.UseSqlServer("Data Source=.;Initial Catalog=Forum;Integrated Security=True;TrustServerCertificate=True"); });

builder.Services.AddScoped<ITopicoRepository, TopicoRepository>();
builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>();
//builder.Services.AddScoped<I,TopicoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();
app.UseRouting();

app.Run();

