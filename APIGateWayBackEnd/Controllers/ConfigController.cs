using DynamicGateway.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace DynamicGateway.Controllers
{
    [ApiController]
    [Route("api/config")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _configFilePath = "ocelot.json";

        public ConfigController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult GetConfig()
        {
            var config = System.IO.File.ReadAllText(_configFilePath);
            return Ok(config);
        }

        [HttpPost]
        public IActionResult UpdateConfig([FromBody] dynamic config)
        {
            System.IO.File.WriteAllText(_configFilePath, config.ToString());
            return Ok();
        }

        [HttpPost("reload")]
        public IActionResult ReloadConfig()
        {
            // Implement logic to reload Ocelot configuration (if supported).
            return Ok();
        }

        /*
        [HttpPost("update-route")]
        public IActionResult UpdateRoute([FromBody] RouteUpdateModel routeUpdate)
        {
            // Read existing ocelot.json
            var jsonString = System.IO.File.ReadAllText(_configFilePath);
            var ocelotConfig = JsonSerializer.Deserialize<OcelotConfig>(jsonString);

            // Update the configuration (e.g., add or modify a route)
            var route = ocelotConfig.Routes.FirstOrDefault(r => r.DownstreamPathTemplate == routeUpdate.DownstreamPathTemplate);
            if (route == null)
            {
                route = new Route
                {
                    DownstreamPathTemplate = routeUpdate.DownstreamPathTemplate,
                    UpstreamPathTemplate = routeUpdate.UpstreamPathTemplate,
                    DownstreamHostAndPorts = new List<HostAndPort> { new HostAndPort { Host = routeUpdate.Host, Port = routeUpdate.Port } }
                };
                ocelotConfig.Routes.Add(route);
            }
            else
            {
                route.UpstreamPathTemplate = routeUpdate.UpstreamPathTemplate;
                route.DownstreamHostAndPorts[0].Host = routeUpdate.Host;
                route.DownstreamHostAndPorts[0].Port = routeUpdate.Port;
            }

            // Write back to ocelot.json
            var updatedJson = JsonSerializer.Serialize(ocelotConfig, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(_configFilePath, updatedJson);

            return Ok("Configuration updated. Changes will reflect soon.");
        }

        */
    }
}
