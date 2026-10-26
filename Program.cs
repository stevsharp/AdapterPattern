using AdapterPattern;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("=== Adapter Pattern Demo: Payment Gateways (with IoC) ===");
Console.WriteLine();

// Build the container. All wiring lives in AddPaymentProcessing().
using ServiceProvider provider = new ServiceCollection()
    .AddPaymentProcessing()
    .BuildServiceProvider();

// Resolve the client; the container injects every registered IPaymentProcessor adapter.
var checkout = provider.GetRequiredService<CheckoutService>();
checkout.RunDemo();

Console.WriteLine("CheckoutService and Program know nothing about gateways: the IoC container");
Console.WriteLine("injected adapters that translate each gateway's API into IPaymentProcessor.");
