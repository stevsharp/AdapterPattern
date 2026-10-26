using AdapterPattern.Adaptees;
using AdapterPattern.Target;

namespace AdapterPattern.Adapters;

/// <summary>
/// Adapter: wraps LegacyBankGateway and translates between its cents/out-param/
/// status-code world and the IPaymentProcessor contract. Uses object adapter
/// style (composition), the usual choice in C#.
/// </summary>
public class LegacyBankAdapter(LegacyBankGateway gateway) : IPaymentProcessor
{
    private readonly LegacyBankGateway _gateway = gateway;

    public string ProviderName => "Legacy Bank";

    public PaymentResult Pay(decimal amount, string currency, string customerEmail)
    {
        long cents = (long)Math.Round(amount * 100, MidpointRounding.AwayFromZero);
        string accountHolder = customerEmail.Split('@')[0];

        int code = _gateway.SubmitTransfer(cents, currency.ToUpperInvariant(), accountHolder, out long reference);

        return code == 0
            ? new PaymentResult(true, $"BANK-{reference}", "Transfer accepted")
            : new PaymentResult(false, string.Empty, $"Bank rejected transfer (code {code})");
    }
}
