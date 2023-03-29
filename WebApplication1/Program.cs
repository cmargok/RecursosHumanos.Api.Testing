
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using RecursosHumanos.API.DataAccess;
using RecursosHumanos.API.Services;
using Microsoft.AspNetCore.Authentication;
using RecursosHumanos.API.MinimalSecurity;
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

////var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

//var configUri = builder.Configuration["URI"];
//Uri uri = new Uri(configUri);

//var cliente = new SecretClient(uri, new DefaultAzureCredential());
//var secret = cliente.GetSecret("ConnectionString").ToString();



// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddControllersAsServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<RH_Context>(options => options.UseSqlServer(" "));
builder.Services.AddSingleton<IAdoRepository>(connection => new AdoRepository(" "));
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuthentication", null);



//AdoRepository(obje=> obje.)>();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
