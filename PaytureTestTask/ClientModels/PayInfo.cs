namespace PaytureTestTask.ClientModels;

public class PayInfo
{
    public required string PAN { get; init; }

    public required int EMonth { get; init; }

    public required int EYear { get; init; }

    public required string OrderId { get; init; }

    public required int Amount { get; init; }

    public int SecureCode { get; init; }

    public string CardHolder { get; init; }
}