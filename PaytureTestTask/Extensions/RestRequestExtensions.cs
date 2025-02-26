using RestSharp;

namespace PaytureTestTask.Extensions;

public static class RestRequestExtensions
{
    public static RestRequest AddQueryParameterIfValueNotNull(
        this RestRequest request,
        string name,
        string value,
        bool encode = true)
    {
        return value is null
            ? request
            : request.AddQueryParameter(name, value, encode);
    }
}