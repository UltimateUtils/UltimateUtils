using System.Net;

namespace UltimateUtils.Extensions;

public static class HttpStatusCodeExtensions
{
    private const string BaseUrl = "https://tools.ietf.org/html/rfc9110";

    private static readonly Dictionary<int, string> ReferenceDictionary =
        new()
        {
            [300] = $"{BaseUrl}#section-15.4.1", // Ambiguous // MultipleChoices
            [301] = $"{BaseUrl}#section-15.4.2", // Moved // MovedPermanently
            [302] = $"{BaseUrl}#section-15.4.3", // Found // Redirect
            [303] = $"{BaseUrl}#section-15.4.4", // RedirectMethod // SeeOther
            [304] = $"{BaseUrl}#section-15.4.5", // NotModified
            [305] = $"{BaseUrl}#section-15.4.6", // UseProxy
            [306] = $"{BaseUrl}#section-15.4.7", // Unused
            [307] = $"{BaseUrl}#section-15.4.8", // RedirectKeepVerb // TemporaryRedirect
            [308] = $"{BaseUrl}#section-15.4.9", // PermanentRedirect

            [400] = $"{BaseUrl}#section-15.5.1",  // BadRequest
            [401] = $"{BaseUrl}#section-15.5.2",  // Unauthorized
            [402] = $"{BaseUrl}#section-15.5.3",  // PaymentRequired
            [403] = $"{BaseUrl}#section-15.5.4",  // Forbidden
            [404] = $"{BaseUrl}#section-15.5.5",  // NotFound
            [405] = $"{BaseUrl}#section-15.5.6",  // MethodNotAllowed
            [406] = $"{BaseUrl}#section-15.5.7",  // NotAcceptable
            [407] = $"{BaseUrl}#section-15.5.8",  // ProxyAuthenticationRequired
            [408] = $"{BaseUrl}#section-15.5.9",  // RequestTimeout
            [409] = $"{BaseUrl}#section-15.5.10", // Conflict
            [410] = $"{BaseUrl}#section-15.5.11", // Gone
            [411] = $"{BaseUrl}#section-15.5.12", // LengthRequired
            [412] = $"{BaseUrl}#section-15.5.13", // PreconditionFailed
            [413] = $"{BaseUrl}#section-15.5.14", // RequestEntityTooLarge
            [414] = $"{BaseUrl}#section-15.5.15", // RequestUriTooLong
            [415] = $"{BaseUrl}#section-15.5.16", // UnsupportedMediaType
            [416] = $"{BaseUrl}#section-15.5.17", // RequestedRangeNotSatisfiable
            [417] = $"{BaseUrl}#section-15.5.18", // ExpectationFailed
            [418] = $"{BaseUrl}#section-15.5.19", // April Fools' Day
            [421] = $"{BaseUrl}#section-15.5.20", // MisdirectedRequest
            [422] = $"{BaseUrl}#section-15.5.21", // UnprocessableContent // UnprocessableEntity
            [426] = $"{BaseUrl}#section-15.5.22", // UpgradeRequired

            [500] = $"{BaseUrl}#section-15.6.1", // InternalServerError
            [501] = $"{BaseUrl}#section-15.6.2", // NotImplemented
            [502] = $"{BaseUrl}#section-15.6.3", // BadGateway
            [503] = $"{BaseUrl}#section-15.6.4", // ServiceUnavailable
            [504] = $"{BaseUrl}#section-15.6.5", // GatewayTimeout
            [505] = $"{BaseUrl}#section-15.6.6", // HttpVersionNotSupported
        };

    public static string GetReferenceDocumentUrl(this HttpStatusCode statusCode)
    {
        return ReferenceDictionary[(int)statusCode];
    }
}
