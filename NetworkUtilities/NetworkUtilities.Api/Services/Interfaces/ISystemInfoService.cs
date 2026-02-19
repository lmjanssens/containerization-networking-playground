namespace NetworkUtilities.Api.Services.Interfaces
{
    public interface ISystemInfoService
    {
        object GetSystemInfo();
        IDictionary<string, string?> GetEnvironmentVariables(string? prefix = null);
    }
}