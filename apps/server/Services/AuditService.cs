using profisysApp.Config;
using System.Security.Claims;

namespace profisysApp.Services
{
  public class AuditService
  {
    private readonly AppSettings _appSettings;

    public AuditService(AppSettings appSettings)
    {
      _appSettings = appSettings;
    }

    public async Task LogAsync(ClaimsPrincipal user, string action, string details = "")
    {
      var username = user?.Claims.FirstOrDefault(c => c.Type == "username")?.Value ?? "Unknown";
      await LogAsync(username, action, details);
    }

    public async Task LogAsync(string username, string action, string details = "")
    {
      var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {username} | {action} | {details}";
      await File.AppendAllTextAsync(_appSettings.AUDIT_LOG_PATH, logEntry + Environment.NewLine);
    }
  }
}
