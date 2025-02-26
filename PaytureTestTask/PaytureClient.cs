using PaytureTestTask.ClientModels;
using PaytureTestTask.Extensions;
using RestSharp;

namespace PaytureTestTask;

public class PaytureClient
{
    private IRestClient _client;
    
    public PaytureClient(string enviroment, TimeSpan? defaultTimeout = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(enviroment);

        var clientOptions = new RestClientOptions($"https://{enviroment}.payture.com/api")
        {
            Timeout = defaultTimeout ?? TimeSpan.FromSeconds(30),
        };

        _client = new RestClient(clientOptions);
    }

    //Тут можно накидать оберток, чтобы передавать timeout как TimeSpan, или количество секунд int
    public async Task<RestResponse<PayResponse>> Pay(PayRequest request, TimeSpan? timeout = null)
    {
        var restRequest = new RestRequest("Pay");

        //Overrides timeout from RestClientOptions
        if (timeout.HasValue)
        {
            restRequest.Timeout = timeout.Value;
        }
        
        restRequest
            .AddQueryParameter("Key", request.Key, false)
            .AddQueryParameter("OrderId", request.OrderId, false)
            .AddQueryParameter("Amount", request.Amount, false)
            .AddQueryParameter("PayInfo", BuildPayInfoString(request.PayInfo))
            .AddQueryParameterIfValueNotNull("PaytureId", request.PaytureId, false)
            .AddQueryParameterIfValueNotNull("CustomerKey", request.CustomerKey, false)
            .AddQueryParameterIfValueNotNull("CustomFields", request.CustomFields)
            .AddQueryParameterIfValueNotNull("Cheque", request.Cheque, false);

        return await _client.ExecutePostAsync<PayResponse>(restRequest);
    }

    public async Task<RestResponse<GetStateResponse>> GetState(string key, string orderId, TimeSpan? timeout = null)
    {
        var restRequest = new RestRequest("GetState");
        
        if (timeout.HasValue)
        {
            restRequest.Timeout = timeout.Value;
        }

        restRequest
            .AddQueryParameter("Key", key, false)
            .AddQueryParameter("OrderId", orderId, false);

        return await _client.ExecutePostAsync<GetStateResponse>(restRequest);
    }
    
    private string BuildPayInfoString(PayInfo payInfo)
    {
        return $"PAN={payInfo.PAN}" +
               $";EMonth={payInfo.EMonth}" +
               $";EYear={payInfo.EYear}" +
               $";OrderId={payInfo.OrderId}" +
               $";Amount={payInfo.Amount}" +
               $";SecureCode={payInfo.SecureCode}" +
               $";CardHolder={payInfo.CardHolder}";
    }
}