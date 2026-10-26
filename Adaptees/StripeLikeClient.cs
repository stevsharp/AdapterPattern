namespace AdapterPattern.Adaptees;

/// <summary>
/// Adaptee #2: a modern third-party SDK with a fluent, request/response style
/// API. Also incompatible with IPaymentProcessor, in a different way.
/// </summary>
public class StripeLikeClient
{
    public ChargeResponse CreateCharge(ChargeRequest request)
    {
        Console.WriteLine($"    [StripeLikeClient] CreateCharge(amount={request.Amount}, currency={request.Currency}, email={request.ReceiptEmail})");

        if (string.IsNullOrWhiteSpace(request.ReceiptEmail) || !request.ReceiptEmail.Contains('@'))
        {
            return new ChargeResponse { Status = "failed", Error = "invalid_email" };
        }

        return new ChargeResponse
        {
            Status = "succeeded",
            ChargeId = "ch_" + Guid.NewGuid().ToString("N")[..12]
        };
    }
}

public class ChargeRequest
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "usd";
    public string ReceiptEmail { get; set; } = "";
}

public class ChargeResponse
{
    public string Status { get; set; } = "";
    public string? ChargeId { get; set; }
    public string? Error { get; set; }
}
