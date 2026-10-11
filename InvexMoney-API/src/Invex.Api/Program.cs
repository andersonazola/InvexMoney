using Invex.Repository;
using Invex.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Invex.Application;
using Invex.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add servico de banco de dados
builder.Services.AddDbContext<InvexContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("InvexDb")));

// add servicos ao contêiner
builder.Services.AddScoped<ISenhaHash, BCryptSenhaHash>();
builder.Services.AddScoped<IUsuarioApplication, UsuarioApplication>();

// Add interfaces do banco
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddControllers();


var app = builder.Build();



// Configure o pipeline de solicitação HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

