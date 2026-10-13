using Invex.Repository;
using Invex.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Invex.Application;
using Invex.Application.Interfaces;
using Invex.Application.Seguranca;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;



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

//Add Jwt
var jwtSettings = builder.Configuration.GetSection(JwtSettings.Secao).Get<JwtSettings>() ?? throw new InvalidOperationException("Configuração JwtSettings não encontrada");

if(Encoding.UTF8.GetByteCount(jwtSettings.Chave) < 32)
    throw new InvalidOperationException("JwtSettings: Chave precisa ter pelo menos 32 caracteres");

builder.Services.AddSingleton(jwtSettings);
builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Emissor,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audiencia,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Chave)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();



// Configure o pipeline de solicitação HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

