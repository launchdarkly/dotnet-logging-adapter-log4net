This .NET package provides integration from the [`LaunchDarkly.Logging`](https://launchdarkly.github.io/dotnet-logging) API that is used by the LaunchDarkly [.NET SDK](https://github.com/launchdarkly/dotnet-server-sdk), [Xamarin SDK](https://github.com/launchdarkly/xamarin-client-sdk), and other LaunchDarkly libraries, to the [Apache log4net](https://logging.apache.org/log4net/) framework.

This adapter is published as a separate NuGet package to avoid unwanted dependencies on log4net in the LaunchDarkly SDKs and in applications that do not use that framework.

## Usage

The **<xref:LaunchDarkly.Logging.LdLog4net>** adapter is provided by the NuGet package [**`LaunchDarkly.Logging.Log4net`**](https://nuget.org/packages/LaunchDarkly.Logging.Log4net). It provides integration with [log4net](https://logging.apache.org/log4net/) version 2.0.6 and higher.

Log4net has a rich configuration system that allows log behavior to be controlled in many ways. The LaunchDarkly adapter does not define any specific logging behavior itself, so the actual behavior will be determined by how you have configured log4net.

To use the adapter:

1. Add the NuGet package `LaunchDarkly.Logging.Log4net` to your project. Make sure you also have a dependency on a compatible version of [log4net](https://nuget.org/packages/log4net).

2. Use the property [**LdLog4net.Adapter**](xref:LaunchDarkly.Logging.LdLog4net.Adapter) in any LaunchDarkly library configuration that accepts a `LaunchDarkly.Logging.ILogAdapter` object. For instance, if you are configuring the LaunchDarkly .NET SDK:

```csharp
using LaunchDarkly.Logging;
using LaunchDarkly.Sdk.Server;

var config = Configuration.Builder("my-sdk-key")
    .Logging(LdLog4net.Adapter)
    .Build();
var client = new LdClient(config);
```
