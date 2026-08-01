using Yarp.ReverseProxy.Configuration;

namespace Gateway.Config;

public interface IProxyDefinitions<T>
{
  public List<ClusterConfig> GetClusters();
  public List<RouteConfig> GetRoutes();
}