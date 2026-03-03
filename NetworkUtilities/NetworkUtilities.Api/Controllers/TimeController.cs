using Microsoft.AspNetCore.Mvc;
using NetworkUtilities.Api.Services.Interfaces;

namespace NetworkUtilities.Api.Controllers
{
    /// <summary>
    /// Displays the current system time information.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly ISystemClock _clock;
        private readonly ILogger<TimeController> _logger;

        public TimeController(ISystemClock clock, ILogger<TimeController> logger)
        {
            _clock = clock;
            _logger = logger;
        }

        [HttpGet]
        [Route("/time")]
        public IActionResult GetTime()
        {
            var response = new
            {
                UtcNow = _clock.UtcNow,
                LocalNow = _clock.LocalNow,
                LocalTimeZoneId = _clock.LocalTimeZoneId,
                Weekend = _clock.Weekend,
                DaysTillWeekend = _clock.DaysTillWeekend,
            };

            _logger.LogInformation($"UTC Now: {_clock.UtcNow}, Local Now: {_clock.LocalNow}, Local Time Zone: {_clock.LocalTimeZoneId}");

            return Ok(response);
        }
    }
}