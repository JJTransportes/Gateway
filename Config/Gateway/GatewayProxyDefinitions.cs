using Gateway.Config;
using Gateway.Config.Api;
using Gateway.Config.Auth;
using Yarp.ReverseProxy.Configuration;

class GatewayProxyDefinitions(
IProxyDefinitions<ApiProxyDefinitions> apiProxyDefinitions,
IProxyDefinitions<AuthProxyDefinitions> authProxyDefinitions
) : IProxyDefinitions<GatewayProxyDefinitions>
{
  public List<ClusterConfig> GetClusters()
  {
    List<ClusterConfig> apiClusters = apiProxyDefinitions.GetClusters();
    List<ClusterConfig> authClusters = authProxyDefinitions.GetClusters();

    List<ClusterConfig> Clusters = [.. authClusters, .. apiClusters];

    return Clusters;
  }

  public List<RouteConfig> GetRoutes()
  {

    List<RouteConfig> apiRoutes = apiProxyDefinitions.GetRoutes();
    List<RouteConfig> authRoutes = authProxyDefinitions.GetRoutes();

    List<RouteConfig> Routes = [.. authRoutes, .. apiRoutes];

    return Routes;
  }
}