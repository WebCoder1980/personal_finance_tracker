using System.Text;
using System.Text.Json;

namespace PersonalFinanceTracker.Frontend.Auth;

public static class JwtPayloadDecoder
{
    public static string DecodePayloadJson(string jwt)
    {
        var parts = jwt.Split('.');
        if (parts.Length < 2)
            throw new FormatException("Некорректный JWT: ожидается header.payload[.signature].");

        var jsonBytes = Base64UrlDecode(parts[1]);
        using var document = JsonDocument.Parse(jsonBytes);
        return JsonSerializer.Serialize(document.RootElement, new JsonSerializerOptions { WriteIndented = true });
    }

    private static byte[] Base64UrlDecode(string input)
    {
        var padded = input.Replace('-', '+').Replace('_', '/');
        switch (padded.Length % 4)
        {
            case 2: padded += "=="; break;
            case 3: padded += "="; break;
        }

        return Convert.FromBase64String(padded);
    }
}
