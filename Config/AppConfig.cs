namespace Gateway.Config;

public class AppConfig
{
  public static string SectionName = "AppConfig";

  public int Port { get; init; }
  public string Service { get; init; } = string.Empty;
  public string Status { get; init; } = string.Empty;
}