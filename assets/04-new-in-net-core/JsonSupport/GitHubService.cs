using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Threading;
using System;
using System.Text.Json;
using System.Text;

namespace JsonSupport
{
    public class GitHubService : IHostedService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GitHubService> _logger;

        public GitHubService(
            IHttpClientFactory httpClientFactory, 
            ILogger<GitHubService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("github");
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var msg = await _httpClient.GetStringAsync("orgs/dotnet/repos");
            PrintJson(msg);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public static void PrintJson(string str)
        {
            ReadOnlySpan<byte> dataUtf8 = Encoding.ASCII.GetBytes(str).AsSpan();
            var json = new Utf8JsonReader(dataUtf8, isFinalBlock: true, state: default);

            while (json.Read())
            {
                JsonTokenType tokenType = json.TokenType;
                ReadOnlySpan<byte> valueSpan = json.ValueSpan;
                switch (tokenType)
                {
                    case JsonTokenType.StartObject:
                    case JsonTokenType.EndObject:
                        break;
                    case JsonTokenType.StartArray:
                    case JsonTokenType.EndArray:
                        break;
                    case JsonTokenType.PropertyName:
                        break;
                    case JsonTokenType.String:
                        Console.WriteLine($"STRING: {json.GetString()}");
                        break;
                    case JsonTokenType.Number:
                        if (!json.TryGetInt32(out int valueInteger))
                        {
                            throw new FormatException();
                        }
                        break;
                    case JsonTokenType.True:
                    case JsonTokenType.False:
                        Console.WriteLine($"BOOL: {json.GetBoolean()}");
                        break;
                    case JsonTokenType.Null:
                        break;
                    default:
                        throw new ArgumentException();
                }
            }

            dataUtf8 = dataUtf8.Slice((int)json.BytesConsumed);
            JsonReaderState state = json.CurrentState;
        }
    }
}
