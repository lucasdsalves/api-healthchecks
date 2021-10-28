using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace Template.Configuration.HealthChecks
{
    public class CustomHealthChecks : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new HealthCheckResult(
                        status: HealthStatus.Unhealthy,
                        description: "There's something not ok."
                        ));
        }
    }
}
