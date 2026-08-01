using Gateway.Config;

var builder = WebApplication.CreateBuilder(args);

IConfigurationSection apiSection = builder.Configuration.GetSection(ApiConfig.SectionName);
IConfigurationSection appSection = builder.Configuration.GetSection(AppConfig.SectionName);
builder.Services.Configure<ApiConfig>(apiSection);
builder.Services.Configure<AppConfig>(appSection);

var apiConfig = appSection.Get<ApiConfig>();
var appConfig = appSection.Get<AppConfig>();

if (apiConfig is null) throw new Exception("ApiConfig not provided.");
if (appConfig is null) throw new Exception("AppConfig not provided.");

builder.WebHost.UseUrls($"http://*:{appConfig.Port}");

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
