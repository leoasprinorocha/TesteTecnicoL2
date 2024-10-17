using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using TesteTecnicoL2.Application.Applications;
using TesteTecnicoL2.Application.Applications.Interfaces;
using TesteTecnicoL2.Application.Dtos;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Serviço empacotamento de produtos", Version = "v1" });

    // Configure Bearer Token Authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor insira o token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

#region DI application container
builder.Services.AddScoped<IEntradaPedidoApplication, EntradaPedidoApplication>();
#endregion

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "GerenciaFood", 
            ValidAudience = "https://localhost",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PEREJICOMIPEREJICENE"))
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

HttpContent GetHttpContent<T>(T obj)
{
    // Serialize the object to JSON
    var json = JsonSerializer.Serialize(obj);

    // Create and return HttpContent from the JSON string
    return new StringContent(json, Encoding.UTF8, "application/json");
}


app.MapPost("/autenticar", [AllowAnonymous] async (string email, string senha) =>
{
    using HttpClient client = new HttpClient();
    client.BaseAddress = new Uri("https://localhost:44325/");
    var paramsLogin = new { email = email, password = senha };
    var response = await client.PostAsync("authentication/login", GetHttpContent(paramsLogin));
    response.EnsureSuccessStatusCode();
    var responseString = await response.Content.ReadAsStringAsync();
    return Results.Ok(responseString);
})
.WithName("Autenticar")
.WithOpenApi();


app.MapPost("/processarpedidos", [AllowAnonymous] async (PedidoDto pedido, IEntradaPedidoApplication entradaPedidoApplication) =>
{
    var pedidosProcessados = await entradaPedidoApplication.RealizaProcessamentoDosPedidos(pedido.pedidos);
    return Results.Ok(pedidosProcessados);
})
.WithName("ProcessaPedidos")
.WithOpenApi();


app.Run();

