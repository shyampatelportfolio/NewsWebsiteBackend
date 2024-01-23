using MongoDB.Driver;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

string keyVaultUrl = builder.Configuration["Azure:KeyVault:KeyVaultURL"];

var credential = new ClientSecretCredential(
builder.Configuration["Azure:KeyVault:TenantId"],
builder.Configuration["Azure:KeyVault:ClientId"],
builder.Configuration["Azure:KeyVault:ClientSecret"]);
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), credential);


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.WithOrigins("http://localhost:5173", "http://localhost:5174")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
    });
}

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var client = new SecretClient(new Uri(keyVaultUrl), credential);
    return new MongoClient(client.GetSecret("cosmosDB").Value.Value.ToString());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

