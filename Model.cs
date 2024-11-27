using System.Text.Json.Serialization;

public record CopilotUsageMetrics(
    [property: JsonPropertyName("day")] string Day,
    [property: JsonPropertyName("total_suggestions_count")] int? TotalSuggestionsCount,
    [property: JsonPropertyName("total_acceptances_count")] int? TotalAcceptancesCount,
    [property: JsonPropertyName("total_lines_suggested")] int? TotalLinesSuggested,
    [property: JsonPropertyName("total_lines_accepted")] int? TotalLinesAccepted,
    [property: JsonPropertyName("total_active_users")] int? TotalActiveUsers,
    [property: JsonPropertyName("total_chat_acceptances")] int? TotalChatAcceptances,
    [property: JsonPropertyName("total_chat_turns")] int? TotalChatTurns,
    [property: JsonPropertyName("total_active_chat_users")] int? TotalActiveChatUsers,
    [property: JsonPropertyName("breakdown")] List<Breakdown> Breakdown
);

public record Breakdown(
    [property: JsonPropertyName("language")] string Language,
    [property: JsonPropertyName("editor")] string Editor,
    [property: JsonPropertyName("suggestions_count")] int? SuggestionsCount,
    [property: JsonPropertyName("acceptances_count")] int? AcceptancesCount,
    [property: JsonPropertyName("lines_suggested")] int? LinesSuggested,
    [property: JsonPropertyName("lines_accepted")] int? LinesAccepted,
    [property: JsonPropertyName("active_users")] int? ActiveUsers
);
