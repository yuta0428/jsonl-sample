using ConsoleAppFramework;
using Cysharp.IO;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

await ConsoleApp.RunAsync(args, Commands.Hello);

static class Commands
{
    /// <summary>
    /// Display Hello.
    /// </summary>
    public static async Task Hello()
    {
        var log = new HashSet<string>();
        var filePath = "./sample.jsonl";

        try
        {
            using var reader = new Utf8StreamReader(filePath);
            while (await reader.LoadIntoBufferAsync())
            {
                while (reader.TryReadLine(out var line))
                {
                    // line is ReadOnlyMemory<byte>, deserialize UTF8 directly.
                    var d = JsonSerializer.Deserialize<CopilotUsageMetrics>(line.Span);
                    if (d != null)
                    {
                        log.Add(d.Day);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"The file could not be read: {e.Message}");
        }

        try
        {
            // using var client = new HttpClient();
            // var requestUri = $"https://api.github.com/orgs/{org}/team/{teamSlug}/copilot/usage";
            // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
            // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            // client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
            // client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("HttpClient", "1.0"));  // GitHub API には User-Agent が必要
            // try
            // {
            //     var response = await client.GetAsync(requestUri);
            //     response.EnsureSuccessStatusCode();
            //     var responseBodyBytes = await response.Content.ReadAsByteArrayAsync();
            // }
            // catch (HttpRequestException e)
            // {
            //     Console.WriteLine($"Request error: {e.Message}");
            // }
            using var writer = new StreamWriter(filePath, append: true);
            ReadOnlySpan<byte> byteArray = """[{"day":"2023-10-15","total_suggestions_count":1000,"total_acceptances_count":800,"total_lines_suggested":1800,"total_lines_accepted":1200,"total_active_users":10,"total_chat_acceptances":32,"total_chat_turns":200,"total_active_chat_users":4,"breakdown":[{"language":"python","editor":"vscode","suggestions_count":300,"acceptances_count":250,"lines_suggested":900,"lines_accepted":700,"active_users":5},{"language":"python","editor":"jetbrains","suggestions_count":300,"acceptances_count":200,"lines_suggested":400,"lines_accepted":300,"active_users":2},{"language":"ruby","editor":"vscode","suggestions_count":400,"acceptances_count":350,"lines_suggested":500,"lines_accepted":200,"active_users":3}]},{"day":"2023-10-16","total_suggestions_count":800,"total_acceptances_count":600,"total_lines_suggested":1100,"total_lines_accepted":700,"total_active_users":12,"total_chat_acceptances":57,"total_chat_turns":426,"total_active_chat_users":8,"breakdown":[{"language":"python","editor":"vscode","suggestions_count":300,"acceptances_count":200,"lines_suggested":600,"lines_accepted":300,"active_users":2},{"language":"python","editor":"jetbrains","suggestions_count":300,"acceptances_count":150,"lines_suggested":300,"lines_accepted":250,"active_users":6},{"language":"ruby","editor":"vscode","suggestions_count":200,"acceptances_count":150,"lines_suggested":200,"lines_accepted":150,"active_users":3}]}]  """u8;
            var data = JsonSerializer.Deserialize<CopilotUsageMetrics[]>(byteArray);
            foreach (var d in data ?? [])
            {
                if (log.Contains(d.Day))
                    continue;

                var json = JsonSerializer.Serialize(d);
                await writer.WriteLineAsync(json);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"The file could not be read: {e.Message}");
        }
    }
}