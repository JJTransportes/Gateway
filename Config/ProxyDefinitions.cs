using Gateway.Config;
using Yarp.ReverseProxy.Configuration;

public static class ProxyDefinitions
{
  public static IReverseProxyBuilder LoadApiProxyConfiguration(this IReverseProxyBuilder builder, ApiConfig config)
  {

    List<ClusterConfig> Clusters = new List<ClusterConfig>
    {
      new ClusterConfig
      {
        ClusterId = config.ClusterId,
        Destinations = new Dictionary<string, DestinationConfig>
        {
          [config.ClusterKey] = new DestinationConfig
          {
            Address = config.ClusterAddress,
          }
        }
      },
    };

    List<RouteConfig> Routes = new List<RouteConfig>
    {
      new RouteConfig
        {
          AuthorizationPolicy = config.DefaultPolicy,
          RouteId = config.RouteId,
          ClusterId = config.ClusterId,
          Match = new RouteMatch
          {
            Path = config.Path
          },
          Transforms = new[]
          {
            new Dictionary<string, string>
            {
               ["PathRemovePrefix"] = config.Route
            }
          },
        },
    };

    builder.LoadFromMemory(Routes, Clusters);

    return builder;
  }
}