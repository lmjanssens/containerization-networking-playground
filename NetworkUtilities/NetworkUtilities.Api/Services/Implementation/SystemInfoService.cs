using System.Runtime.InteropServices;
using NetworkUtilities.Api.Services.Interfaces;

namespace NetworkUtilities.Api.Services.Implementation
{
    public class SystemInfoService : ISystemInfoService
    {
        public object GetSystemInfo()
        {
            return new
            {
                MachineName = Environment.MachineName,
                OsDescription = RuntimeInformation.OSDescription,
                OsArchitecture = RuntimeInformation.OSArchitecture.ToString(),
                ProcessArchitecture = RuntimeInformation.ProcessArchitecture.ToString(),
                FrameworkDescription = RuntimeInformation.FrameworkDescription,
                ProcessId = Environment.ProcessId,
                CurrentDirectory = Environment.CurrentDirectory,
                EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown"
            };
        }

        public IDictionary<string, string?> GetEnvironmentVariables(string? prefix = null)
        {
            var all = Environment.GetEnvironmentVariables();
            var dict = new Dictionary<string, string?>();

            foreach (var key in all.Keys.Cast<string>())
            {
                if (!string.IsNullOrEmpty(prefix) &&
                    !key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                dict[key] = all[key]?.ToString();
            }

            return dict;
        }
    }
}