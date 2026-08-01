namespace Gateway.Config;

public static class ProxyConfig
{
  public static IServiceCollection UseReverseProxy(this IServiceCollection services)
  {
    IProxyDefinitions<GatewayProxyDefinitions> gatewayProxyDefinitions = services.BuildServiceProvider()
      .GetRequiredService<IProxyDefinitions<GatewayProxyDefinitions>>();

    var Routes = gatewayProxyDefinitions.GetRoutes();
    var Clusters = gatewayProxyDefinitions.GetClusters();

    services.AddReverseProxy()
      .LoadFromMemory(Routes, Clusters);

    return services;
  }
}