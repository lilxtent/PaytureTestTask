using PaytureTestTask.ClientModels;
using RestSharp;

namespace PaytureTestTask;

public interface IPaytureClient
{
    Task<RestResponse<PayResponse>> Pay(PayRequest request, TimeSpan? timeout = null);
    Task<RestResponse<GetStateResponse>> GetState(string key, string orderId, TimeSpan? timeout = null);
}