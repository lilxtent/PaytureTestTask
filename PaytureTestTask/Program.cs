using PaytureTestTask;
using PaytureTestTask.ClientModels;

var client = new PaytureClient("sandbox3");

const string key = "Merchant";
var orderId = Guid.NewGuid().ToString();
const int amount = 1000;

var payRequest = new PayRequest
{
    Key = key,
    OrderId = orderId,
    Amount = amount,
    PayInfo = new PayInfo
    {
        PAN = "5218851946955484",
        EMonth = 12,
        EYear = 25,
        OrderId = orderId,
        Amount = amount,
    }
};

var payResponse = await client.Pay(payRequest);

Console.WriteLine($"Pay response:\n" +
                  $"    HttpCode - {payResponse.StatusCode}\n" +
                  $"    Success - {payResponse.Data?.Success}\n" +
                  $"    ErrCode - {payResponse.Data?.ErrCode}");

var getStateResponse = await client.GetState(key, orderId);

Console.WriteLine($"GetState response:\n" +
                  $"    HttpCode - {getStateResponse.StatusCode}\n" +
                  $"    Success - {getStateResponse.Data?.Success}\n" +
                  $"    ErrCode - {getStateResponse.Data?.ErrCode}");