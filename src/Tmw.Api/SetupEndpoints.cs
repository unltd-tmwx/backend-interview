using Tmw.Lib.Model;
using Tmw.Lib.Services;

namespace Tmw.Api;

public static class SetupEndpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGet("/api/customers/scored/{baseLatitude}/{baseLongitude}", (
            double baseLatitude,
            double baseLongitude,
            ICustomerScoringService service) =>
        {
            var scoredCustomers = service.GetCustomersByScore(new Coordinates(baseLatitude, baseLongitude));
            return Results.Ok(scoredCustomers);
        })
        .WithName("ScoredCustomers")
        .WithOpenApi();

        return app;
    }
}
