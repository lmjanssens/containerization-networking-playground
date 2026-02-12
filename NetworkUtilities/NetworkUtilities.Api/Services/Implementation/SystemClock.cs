using NetworkUtilities.Api.Services.Interfaces;

namespace NetworkUtilities.Api.Services.Implementation
{
    public class SystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

        public DateTimeOffset LocalNow => DateTimeOffset.Now;

        public string LocalTimeZoneId => TimeZoneInfo.Local.Id;

        public string Weekend
        {
            get
            {
                DayOfWeek dayOfWeek = LocalNow.DayOfWeek;

                return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday ? "It's weekend!" : "No weekend yet :/";
            }
        }

        public int DaysTillWeekend
        {
            get
            {
                DayOfWeek dayOfWeek = LocalNow.DayOfWeek;

                if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
                    return 0;

                return ((int)DayOfWeek.Saturday - (int)dayOfWeek + 7) % 7;
            }
        }
    }
}
