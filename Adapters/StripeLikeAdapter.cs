using AdapterPattern.Adaptees;
using AdapterPattern.Target;

namespace AdapterPattern.Adapters;

/// <summary>
/// Adapter: wraps StripeLikeClient and maps its request/response objects onto
/// the IPaymentProcessor contract.
/// </summary>
public class StripeLikeAdapter(StripeLikeClient client) : IPaymentProcessor
{
    private readonly StripeLikeClient _client = client;

    public string ProviderName => "StripeLike";

    public PaymentResult Pay(decimal amount, string currency, string customerEmail)
    {
        var response = _client.CreateCharge(new ChargeRequest
        {
            Amount = amount,
            Currency = currency.ToLowerInvariant(),
            ReceiptEmail = customerEmail
        });

        return response.Status == "succeeded"
            ? new PaymentResult(true, response.ChargeId!, "Charge succeeded")
            : new PaymentResult(false, string.Empty, $"Charge failed: {response.Error}");
    }
}
