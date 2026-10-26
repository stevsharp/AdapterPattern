using AdapterPattern.Target;

namespace AdapterPattern;

/// <summary>
/// The Client. Depends only on IPaymentProcessor instances supplied by the
/// IoC container. It never constructs a gateway or an adapter itself.
/// </summary>
public class CheckoutService(IEnumerable<IPaymentProcessor> processors)
{
    private readonly IEnumerable<IPaymentProcessor> _processors = processors;

    public void RunDemo()
    {
        foreach (var processor in _processors)
        {
            Console.WriteLine($"--- {processor.ProviderName} ---");
            Checkout(processor, 49.99m, "USD", "alice@example.com"); // happy path
            Checkout(processor, 0m, "EUR", "bob@example.com");       // bank rejects amount
            Checkout(processor, 12.50m, "GBP", "not-an-email");      // stripe rejects email
        }
    }

    private static void Checkout(IPaymentProcessor processor, decimal amount, string currency, string customerEmail)
    {
        Console.WriteLine($"  Checkout via {processor.ProviderName}: {amount:0.00} {currency} for {customerEmail}");
        PaymentResult result = processor.Pay(amount, currency, customerEmail);

        Console.WriteLine(result.Success
            ? $"  OK   -> {result.TransactionId} ({result.Message})"
            : $"  FAIL -> {result.Message}");
        Console.WriteLine();
    }
}
