using NetworkUtilities.Api.Services.Interfaces;

namespace NetworkUtilities.Api.Services.Implementation
{
    public class SystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

        public DateTimeOffset LocalNow => DateTimeOffset.Now;

        public string LocalTimeZoneId => TimeZoneInfo.Local.Id;
    }
}
