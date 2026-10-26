namespace AdapterPattern.Target;

/// <summary>
/// The Target interface: the contract our application code is written against.
/// Every payment provider must look like this to the client, regardless of
/// what its real API looks like.
/// </summary>
public interface IPaymentProcessor
{
    string ProviderName { get; }
    PaymentResult Pay(decimal amount, string currency, string customerEmail);
}

public record PaymentResult(bool Success, string TransactionId, string Message);
