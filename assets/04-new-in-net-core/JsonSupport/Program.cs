using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System;

namespace JsonSupport
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddHttpClient("github", client =>
                    {
                        client.BaseAddress = new Uri("https://api.github.com");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                        client.DefaultRequestHeaders.Add("User-Agent", "JSON Demo App");
                    });

                    services.AddHostedService<GitHubService>();
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    
                })
                .Build();

            await host.RunAsync();
        }
    }
}
