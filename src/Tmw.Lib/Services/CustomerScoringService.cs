using Tmw.Lib.Data;
using Tmw.Lib.Model;

namespace Tmw.Lib.Services;

public class CustomerScoringService : ICustomerScoringService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerScoringService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Customer[] GetCustomersByScore(Coordinates baseCoordinates)
    {
        var customers = _customerRepository.GetAllCustomers();
        var customerScoring = Initialise(customers, baseCoordinates);
        customerScoring = Normalise(customerScoring);
        var scoredCustomers = SetCustomersTotalScore(customerScoring);

        scoredCustomers = AddCustomersWithLittleActivityToScoredCustomers(scoredCustomers);

        return scoredCustomers;
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

    static internal Customer[] RandomCustomersWithLittleActivity(Customer[] customers)
    {
        var takeCount = customers.Count() / 10;

        // take bottom 10% of customers by activity and then take 3 random customers from that set
        var randomSet = customers
            .Select(c => new { c, Activity = c.AcceptedOffers + c.CancelledOffers })
            .OrderBy(n => n.Activity)
            .Take(takeCount)
            .OrderBy(c => Guid.NewGuid()) //random order
            .Select(c => c.c)
            .Take(3);

        return randomSet.ToArray();
    }

    static internal Customer[] AddCustomersWithLittleActivityToScoredCustomers(Customer[] scoredCustomers)
    {
        var customersWithLittleActivity = RandomCustomersWithLittleActivity(scoredCustomers);

        var highScoredCustomers = scoredCustomers
            .Except(customersWithLittleActivity)
            .OrderByDescending(c => c.Score)
            .Take(7).ToArray();

        return highScoredCustomers.Concat(customersWithLittleActivity).ToArray();
    }
}
