# Adapter Pattern Console App (C# / .NET 9)

Demonstrates the **Adapter** structural pattern with a payment-processing scenario.

| Role     | Type                                   | Folder      |
|----------|----------------------------------------|-------------|
| Target   | `IPaymentProcessor`                    | `Target/`   |
| Adaptee  | `LegacyBankGateway`, `StripeLikeClient`| `Adaptees/` |
| Adapter  | `LegacyBankAdapter`, `StripeLikeAdapter`| `Adapters/`|
| Client   | `CheckoutService`                      | root        |
| IoC root | `ServiceCollectionExtensions`          | root        |

Wiring uses Microsoft.Extensions.DependencyInjection. `AddPaymentProcessing()` registers the
adaptees, registers each adapter as `IPaymentProcessor`, and registers the client, which
receives `IEnumerable<IPaymentProcessor>` by constructor injection.
gateway (object adapter via composition) and translates calls, units, and results.

## Run

```
dotnet run
```
