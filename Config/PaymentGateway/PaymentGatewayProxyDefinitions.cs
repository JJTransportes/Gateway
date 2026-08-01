using Microsoft.Extensions.Options;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Health;

namespace Gateway.Config.PaymentGateway;

public class PaymentGatewayProxyDefinitions(IOptions<PaymentGatewayConfig> config) : IProxyDefinitions<PaymentGatewayProxyDefinitions>
{
  public List<ClusterConfig> GetClusters() => new List<ClusterConfig>
    {
      new ClusterConfig
      {
        ClusterId = config.Value.ClusterId,
        Destinations = new Dictionary<string, DestinationConfig>
        {
          [config.Value.ClusterKey] = new DestinationConfig
          {
            Address = config.Value.ClusterAddress,
          }
        },
        HealthCheck = new HealthCheckConfig
        {
          Passive = new PassiveHealthCheckConfig
          {
            Enabled = true,
            Policy = HealthCheckConstants.PassivePolicy.TransportFailureRate,
            ReactivationPeriod = TimeSpan.FromSeconds(10)
          },
          Active = new ActiveHealthCheckConfig
          {
            Enabled = false
          }
        }
      },
    };

  public List<RouteConfig> GetRoutes() => new List<RouteConfig>
    {
      new RouteConfig
        {
          AuthorizationPolicy = config.Value.DefaultPolicy,
          RouteId = config.Value.RouteId,
          ClusterId = config.Value.ClusterId,
          Match = new RouteMatch
          {
            Path = config.Value.Path
          },
          Transforms = new[]
          {
            new Dictionary<string, string>
            {
               ["PathRemovePrefix"] = config.Value.Route
            }
          },
        },
    };
}