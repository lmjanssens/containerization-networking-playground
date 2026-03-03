using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetworkUtilities.Api.Services.Interfaces;

namespace NetworkUtilities.Api.Controllers
{
    [ApiController]
    [Route("system/[action]")]
    public class SystemController : Controller
    {
        private readonly ISystemInfoService _systemInfoService;
        private readonly EndpointDataSource _endpointDataSource;

        public SystemController(
            ISystemInfoService systemInfoService,
            EndpointDataSource endpointDataSource)
        {
            _systemInfoService = systemInfoService;
            _endpointDataSource = endpointDataSource;
        }

        [HttpGet]
        [Route("/healthz")]
        public IActionResult Health()
        {
            // endpoint used by docker health checks
            return Ok(new { Status = "Healthy" });
        }

        [HttpGet]
        public IActionResult Info()
        {
            object info = _systemInfoService.GetSystemInfo();
            return Ok(info);
        }

        [HttpGet]
        public IActionResult Env([FromQuery] string? prefix = null)
        {
            IDictionary<string, string?> env = _systemInfoService.GetEnvironmentVariables(prefix);
            return Ok(env);
        }

        [HttpGet]
        public IActionResult Routes()
        {
            var routes = _endpointDataSource.Endpoints
                .OfType<RouteEndpoint>()
                .Select(e =>
                {
                    IReadOnlyList<string>? httpMethods = e.Metadata
                        .OfType<HttpMethodMetadata>()
                        .FirstOrDefault()
                        ?.HttpMethods;

                    return new
                    {
                        Route = e.RoutePattern.RawText,
                        HttpMethods = httpMethods is not null
                            ? string.Join(",", httpMethods)
                            : "N/A",
                        DisplayName = e.DisplayName
                    };
                })
                .OrderBy(r => r.Route)
                .ThenBy(r => r.HttpMethods)
                .ToList();

            return Ok(routes);
        }
    }
}
