namespace Gateway.Config;

public class AuthConfig
{
  public static string SectionName = "AuthConfig";
  public string ClusterId { get; init; } = string.Empty;
  public string ClusterKey { get; init; } = string.Empty;
  public string ClusterAddress { get; init; } = string.Empty;
  public string RouteId { get; init; } = string.Empty;
  public string Path { get; init; } = string.Empty;
  public string Route { get; init; } = string.Empty;
  public string AcessPolicy { get; init; } = string.Empty;
  public string DefaultPolicy { get; init; } = string.Empty;
}