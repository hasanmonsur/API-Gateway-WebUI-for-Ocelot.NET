using System.Collections.Generic;

namespace DynamicGateway.Models
{
    // Models for Ocelot configuration
    public class OcelotConfig
    {
        public List<Route> Routes { get; set; } = new List<Route>();
    }

    public class Route
    {
        public string DownstreamPathTemplate { get; set; }
        public string UpstreamPathTemplate { get; set; }
        public List<HostAndPort> DownstreamHostAndPorts { get; set; }
    }

    public class HostAndPort
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class RouteUpdateModel
    {
        public string DownstreamPathTemplate { get; set; }
        public string UpstreamPathTemplate { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
