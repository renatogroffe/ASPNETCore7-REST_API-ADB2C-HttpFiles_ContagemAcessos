using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using APIs.Security.JWT;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options);
},
options => { builder.Configuration.Bind("AzureAdB2C", options); });

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();

// Configurando o uso do Swagger para prever tokens JWT
builder.Services.AddSwaggerGenJwt("v1",
    new OpenApiInfo
    {
        Title = "APIContagem",
        Description = "Exemplo de implementação de uso de JWT em uma API de contagem de acessos",
        Version = "v1"
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();