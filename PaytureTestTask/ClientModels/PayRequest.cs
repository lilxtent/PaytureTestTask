namespace PaytureTestTask.ClientModels;

public class PayRequest
{
    public required string Key { get; init; }
    
    public required string OrderId { get; init; }

    public required int Amount { get; init; }

    public required PayInfo PayInfo { get; init; }
    
    public string PaytureId { get; init; }

    public string CustomerKey { get; init; }
    
    public string CustomFields { get; init; }
    
    public string Cheque { get; init; }
}