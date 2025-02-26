using FluentAssertions;
using PaytureTestTask;
using PaytureTestTask.ClientModels;

namespace Tests;

public class Tests
{
    private IPaytureClient _client;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _client = new PaytureClient("sandbox3");
    }

    [Test]
    public async Task Pay_Should_Success_On_Valid_Data()
    {
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

        var payResponse = await _client.Pay(payRequest);
        
        payResponse.IsSuccessful.Should().BeTrue();
        var data = payResponse.Data!;
        
        data.Success.Should().Be("True");
        data.ErrCode.Should().BeNull();
    }

    [Test]
    public async Task Pay_Should_Fail_On_Invalid_Data()
    {
        const string key = "invalid";
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

        var payResponse = await _client.Pay(payRequest);
        
        payResponse.IsSuccessful.Should().BeTrue();
        var data = payResponse.Data!;
        
        data.Success.Should().Be("False");
        data.ErrCode.Should().NotBeNull();
    }
    
    [Test]
    public async Task Pay_Should_Fail_On_Amount_Exceeded()
    {
        const string key = "Merchant";
        var orderId = Guid.NewGuid().ToString();
        const int amount = 1_000_000;

        var payRequest = new PayRequest
        {
            Key = key,
            OrderId = orderId,
            Amount = amount,
            PayInfo = new PayInfo
            {
                PAN = "3300000000000001",
                EMonth = 12,
                EYear = 25,
                OrderId = orderId,
                Amount = amount,
            }
        };

        var payResponse = await _client.Pay(payRequest);
        
        payResponse.IsSuccessful.Should().BeTrue();
        var data = payResponse.Data!;
        
        data.Success.Should().Be("False");
        data.ErrCode.Should().Be("AMOUNT_EXCEED");
    }
    
    [Test]
    public async Task GetState_Should_Success_On_Valid_Data()
    {
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

        var payResponse = await _client.Pay(payRequest);
        
        payResponse.IsSuccessful.Should().BeTrue();

        var getStateResponse = await _client.GetState(key, orderId);
        
        getStateResponse.IsSuccessful.Should().BeTrue();
        
        var data = getStateResponse.Data!;
        
        data.Success.Should().Be("True");
        data.ErrCode.Should().BeNull();
    }
    
    [Test]
    public async Task GetState_Should_Fail_On_Invalid_Data()
    {
        const string key = "Merchant";
        var orderId = Guid.NewGuid().ToString();
        
        var getStateResponse = await _client.GetState(key, orderId);
        
        getStateResponse.IsSuccessful.Should().BeTrue();
        
        var data = getStateResponse.Data!;
        
        data.Success.Should().Be("False");
        data.ErrCode.Should().NotBeNull();
    }
}