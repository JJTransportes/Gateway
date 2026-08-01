using Microsoft.Extensions.Options;

namespace Gateway.Config;

public static class ProxyConfig
{
  public static IServiceCollection UseReverseProxy(this IServiceCollection services)
  {
    var apiConfig = services.BuildServiceProvider()
      .GetRequiredService<IOptions<ApiConfig>>().Value;

    services.AddReverseProxy()
      .LoadApiProxyConfiguration(apiConfig);

    return services;
  }
}