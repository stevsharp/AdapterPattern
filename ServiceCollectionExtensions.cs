using AdapterPattern.Adaptees;
using AdapterPattern.Adapters;
using AdapterPattern.Target;
using Microsoft.Extensions.DependencyInjection;

namespace AdapterPattern;

/// <summary>
/// Composition root. This is the only place that knows which concrete adapter
/// wraps which concrete adaptee. Register a new provider here and nothing else changes.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPaymentProcessing(this IServiceCollection services)
    {
        // Adaptees: the third-party / legacy classes we cannot modify.
        services.AddSingleton<LegacyBankGateway>();
        services.AddSingleton<StripeLikeClient>();

        // Adapters: registered against the Target interface so the client can
        // receive them as IPaymentProcessor (or IEnumerable<IPaymentProcessor>).
        services.AddSingleton<IPaymentProcessor, LegacyBankAdapter>();
        services.AddSingleton<IPaymentProcessor, StripeLikeAdapter>();

        // Client.
        services.AddTransient<CheckoutService>();

        return services;
    }
}
