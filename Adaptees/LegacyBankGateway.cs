namespace AdapterPattern.Adaptees;

/// <summary>
/// Adaptee #1: an old in-house bank gateway. Works in integer cents, uses
/// out-parameters and numeric status codes. We cannot change this class.
/// </summary>
public class LegacyBankGateway
{
    private int _nextReference = 1000;

    // Returns 0 on success, non-zero error code otherwise.
    public int SubmitTransfer(long amountInCents, string isoCurrency, string accountHolder, out long referenceNumber)
    {
        Console.WriteLine($"    [LegacyBankGateway] SubmitTransfer(cents={amountInCents}, iso={isoCurrency}, holder={accountHolder})");

        if (amountInCents <= 0)
        {
            referenceNumber = -1;
            return 42; // invalid amount
        }

        referenceNumber = _nextReference++;
        return 0;
    }
}
