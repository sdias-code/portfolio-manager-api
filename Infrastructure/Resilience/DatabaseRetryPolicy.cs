using Polly;
using Polly.Retry;

namespace PortfolioManager.Api.Infrastructure.Resilience;

public static class DatabaseRetryPolicy
{
    public static AsyncRetryPolicy GetRetryPolicy()
    {
        return Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                3,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine(
                        $"Retry {retryCount} after {timeSpan.TotalSeconds}s due to: {exception.Message}");
                });
    }
}