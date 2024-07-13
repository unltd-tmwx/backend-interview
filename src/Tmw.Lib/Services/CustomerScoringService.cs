using Tmw.Lib.Model;

namespace Tmw.Lib.Services;

public class CustomerScoringService : ICustomerScoringService
{
    public Customer[] GetCustomersByScore(Coordinates baseCoordinates)
    {
        throw new NotImplementedException();
    }

    static internal CustomerScoring[] Initialise(Customer[] customers, Coordinates baseCoordinates)
    {
        return customers.Select(c => new CustomerScoring(
            c,
            0,
            c.Location.DistanceTo(baseCoordinates),
            0,
            0,
            0,
            0
        )).ToArray();
    }

    static internal (double min, double max) GetRangeMinMax(double[] range) => (range.Min(), range.Max());

    static internal double NormaliseToDefinedRange(double val, double min, double max, bool invert = false)
    {
        var score = 1 + (val - min) * 9 / (max - min);
        return invert ? 10 - score : score;
    }

    static internal CustomerScoring[] Normalise(CustomerScoring[] customers)
    {
        var (minAge, maxAge) = GetRangeMinMax(customers.Select(c => (double)c.Customer.Age).ToArray());
        var (minDist, maxDist) = GetRangeMinMax(customers.Select(c => c.DistanceFromBase).ToArray());
        var (minAccOff, maxAccOff) = GetRangeMinMax(customers.Select(c => (double)c.Customer.AcceptedOffers).ToArray());
        var (minCanOff, maxCanOff) = GetRangeMinMax(customers.Select(c => (double)c.Customer.CancelledOffers).ToArray());
        var (minAvTime, maxAvTime) = GetRangeMinMax(customers.Select(c => (double)c.Customer.AverageReplyTime).ToArray());

        return customers.Select(c => c with
        {
            AgeScore = NormaliseToDefinedRange(c.Customer.Age, minAge, maxAge),
            DistanceScore = NormaliseToDefinedRange(c.DistanceFromBase, minDist, maxDist, true),
            AcceptedOffersScore = NormaliseToDefinedRange(c.Customer.AcceptedOffers, minAccOff, maxAccOff),
            CancelledOffersScore = NormaliseToDefinedRange(c.Customer.CancelledOffers, minCanOff, maxCanOff, true),
            AverageReplyTimeScore = NormaliseToDefinedRange(c.Customer.AverageReplyTime, minAvTime, maxAvTime, true)
        })
        .Select(c => c)
        .ToArray();
    }

    static internal Customer[] SetCustomersTotalScore(CustomerScoring[] scoredCustomers)
    {
        var (min, max) = GetRangeMinMax(scoredCustomers.Select(c => c.TotalScore).ToArray());
        var computedScores = scoredCustomers.Select(c => c.Customer with
        {
            Score = NormaliseToDefinedRange(c.TotalScore, min, max)
        });

        return computedScores.ToArray();
    }
}
