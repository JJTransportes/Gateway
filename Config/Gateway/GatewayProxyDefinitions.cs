using Gateway.Config;
using Gateway.Config.Api;
using Gateway.Config.Auth;
using Gateway.Config.PaymentGateway;
using Gateway.Config.Services;
using Yarp.ReverseProxy.Configuration;

class GatewayProxyDefinitions(
IProxyDefinitions<PaymentGatewayProxyDefinitions> paymentGatewayProxyDefinitions,
IProxyDefinitions<ServicesProxyDefinitions> servicesProxyDefinitions,
IProxyDefinitions<AuthProxyDefinitions> authProxyDefinitions,
IProxyDefinitions<ApiProxyDefinitions> apiProxyDefinitions
) : IProxyDefinitions<GatewayProxyDefinitions>
{
  public List<ClusterConfig> GetClusters()
  {
    List<ClusterConfig> paymentGatewayClusters = paymentGatewayProxyDefinitions.GetClusters();
    List<ClusterConfig> servicesClusters = servicesProxyDefinitions.GetClusters();
    List<ClusterConfig> authClusters = authProxyDefinitions.GetClusters();
    List<ClusterConfig> apiClusters = apiProxyDefinitions.GetClusters();

    List<ClusterConfig> Clusters = [
      .. paymentGatewayClusters,
      .. servicesClusters,
      .. authClusters,
      .. apiClusters
      ];

    return Clusters;
  }

  public List<RouteConfig> GetRoutes()
  {
    List<RouteConfig> paymentGatewayRoutes = paymentGatewayProxyDefinitions.GetRoutes();
    List<RouteConfig> servicesRoutes = servicesProxyDefinitions.GetRoutes();
    List<RouteConfig> apiRoutes = apiProxyDefinitions.GetRoutes();
    List<RouteConfig> authRoutes = authProxyDefinitions.GetRoutes();

    List<RouteConfig> Routes = [
      .. paymentGatewayRoutes,
      .. servicesRoutes,
      .. authRoutes,
      .. apiRoutes
      ];

    return Routes;
  }
}