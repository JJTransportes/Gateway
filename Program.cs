using Gateway.Config;
using Gateway.Config.Api;
using Gateway.Config.Auth;

var builder = WebApplication.CreateBuilder(args);

IConfigurationSection authSection = builder.Configuration.GetSection(AuthConfig.SectionName);
IConfigurationSection apiSection = builder.Configuration.GetSection(ApiConfig.SectionName);
IConfigurationSection appSection = builder.Configuration.GetSection(AppConfig.SectionName);
builder.Services.Configure<AuthConfig>(authSection);
builder.Services.Configure<ApiConfig>(apiSection);
builder.Services.Configure<AppConfig>(appSection);

var authConfig = authSection.Get<AuthConfig>();
var apiConfig = apiSection.Get<ApiConfig>();
var appConfig = appSection.Get<AppConfig>();

if (authConfig is null) throw new Exception("AuthConfig not provided.");
if (apiConfig is null) throw new Exception("ApiConfig not provided.");
if (appConfig is null) throw new Exception("AppConfig not provided.");

builder.WebHost.UseUrls($"http://*:{appConfig.Port}");

builder.Services.AddSingleton<IProxyDefinitions<AuthProxyDefinitions>, AuthProxyDefinitions>();
builder.Services.AddSingleton<IProxyDefinitions<ApiProxyDefinitions>, ApiProxyDefinitions>();
builder.Services.AddSingleton<IProxyDefinitions<GatewayProxyDefinitions>, GatewayProxyDefinitions>();

builder.Services.AddOpenApi();
builder.Services.UseReverseProxy();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.MapReverseProxy();

app.MapGet("health", () => new
{
  service = appConfig.Service,
  status = appConfig.Status,
  port = appConfig.Port,
  time = TimeOnly.FromDateTime(DateTime.UtcNow)
});

await app.RunAsync();
