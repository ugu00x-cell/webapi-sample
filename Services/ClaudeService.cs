using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WebApplication1.Services;

// Flask の anthropic.Anthropic() クライアントに相当
public class ClaudeService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    public ClaudeService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _http.BaseAddress = new Uri("https://api.anthropic.com/");

        // Flask: os.environ["ANTHROPIC_API_KEY"] に相当
        _apiKey = config["ANTHROPIC_API_KEY"]
            ?? Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY")
            ?? throw new InvalidOperationException("ANTHROPIC_API_KEY が設定されていません");
    }

    public async Task<string> AnalyzeAsync(string text)
    {
        // Flask: client.messages.create(...) に相当
        var requestBody = new
        {
            model = "claude-sonnet-4-6-20250514",
            max_tokens = 1024,
            messages = new[]
            {
                new { role = "user", content = text }
            }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Claude API のヘッダー
        _http.DefaultRequestHeaders.Clear();
        _http.DefaultRequestHeaders.Add("x-api-key", _apiKey);
        _http.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");

        var response = await _http.PostAsync("v1/messages", content);
        var responseJson = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Claude API error: {response.StatusCode} {responseJson}");
        }

        // レスポンスから content[0].text を取り出す
        using var doc = JsonDocument.Parse(responseJson);
        return doc.RootElement
            .GetProperty("content")[0]
            .GetProperty("text")
            .GetString() ?? "";
    }
}
