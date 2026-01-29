namespace NetworkUtilities.Api.Services.Interfaces
{
    public interface ISystemClock
    {
        DateTimeOffset UtcNow { get; }
        DateTimeOffset LocalNow { get; }
        string LocalTimeZoneId { get; }

    }
}
