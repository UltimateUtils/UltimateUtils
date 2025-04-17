using System.Text.Json;

namespace UltimateUtils.Extensions;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions CamelCaseOptions =
        new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

    private static readonly JsonSerializerOptions CamelCaseIndentedOptions =
        new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

    public static string Serialize<TValue>(this TValue value, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(value, options);
    }

    public static string SerializeWithCamelCaseKey<TValue>(this TValue value, bool indented = false)
    {
        return indented
            ? JsonSerializer.Serialize(value, CamelCaseIndentedOptions)
            : JsonSerializer.Serialize(value, CamelCaseOptions);
    }
}
