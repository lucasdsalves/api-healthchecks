This project shows the use of HealthChecks, which is a middleware that shows us application status and health.

# Used packages
```
dotnet add package AspNetCore.HealthChecks.SqlServer
dotnet add package AspNetCore.HealthChecks.UI
dotnet add package AspNetCore.HealthChecks.UI.Client
dotnet add package AspNetCore.HealthChecks.UI.InMemory.Storage
```

# Registering and configuring the service

<b><i>startup.cs</i></b> - ConfigureServices 
```csharp
            services.AddHealthChecks()
                .AddSqlServer(connectionString: Configuration.GetConnectionString("SQLContext"),
                name: "Sql Server Instance")
                .AddCheck<CustomHealthChecks>("Custom Health Checks"); // That's where we add our custom HealthCheck


            //HealthChecks UI
            services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(5);
                options.MaximumHistoryEntriesPerEndpoint(10);
                options.AddHealthCheckEndpoint("API with Health Checks", "/health");
            })
            .AddInMemoryStorage(); 

```

<b><i>startup.cs </b></i>- Configure
```csharp
            app.UseHealthChecksUI(options => {
                options.UIPath = "/dashboard";
                options.AddCustomStylesheet("Configuration/HealthChecks/HealthChecksUI.css");
            });
```			

# Creating a custom healthchecks

<b><i>CustomHealthChecks.cs</b></i> 
```csharp
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
```

# Adding a new personalized style file to format HealthCheckUI 
<b><i>HealthChecksUI.css </b></i> 
```css
:root {
    --bgMain: #fff; /* background color */
    --fcBase: #000; /* font color */
    --bgMenuActive: #2f2f2f; /* background color of active menu item */
    --bcMenuActive: #fffb00; /* border color of active menu item */
    --primaryColor: #5789e0; /* primary color */
    --secondaryColor: #5789e0; /* secondary color */
    --bgMenuActive: #5789e0; /* background color of menu item when active */
    --bgButton: #5789e0; /* background color of button */
    --logoImageUrl: url('https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg'); /* logo image url */
    --bgAside: var(--primaryColor); /* background color of aside */
    --bgTable: #99b8ee; /* background color of table */
    --bgTableSecondary: #99b8ee; /* secondary background color of table */
```

##
  
The healthcheck will be accessed through '/dashboard' on the URL. E.g. localhost:44304/dashboard <br />
  
For more information, check on at https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks <br />
For a user tutorial in portuguese, check on at https://balta.io/blog/aspnet-health-check