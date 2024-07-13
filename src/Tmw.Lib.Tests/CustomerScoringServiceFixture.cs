using Tmw.Lib.Data;
using Tmw.Lib.Model;
using Tmw.Lib.Services;

namespace Tmw.Lib.Tests;

public class CustomerScoringServiceFixture
{
    [Fact]
    public void CanInitialiseCustomerScoringDto()
    {
        var customers = new CustomerSampleRepository().GetAllCustomers();

        // act
        var scoringDtos = CustomerScoringService.Initialise(customers, new Coordinates(0.0, 0.0));

        Assert.Equal(customers.Length, scoringDtos.Length);
        Assert.All(scoringDtos, s => Assert.Equal(0, s.AgeScore));
        Assert.All(scoringDtos, s => Assert.True(s.DistanceFromBase > 0));
    }

    [Fact]
    public void CanGetAgeCategoryRangeMinMax()
    {
        var customers = new CustomerSampleRepository().GetAllCustomers();
        var scoringDtos = CustomerScoringService.Initialise(customers, new Coordinates(0.0, 0.0));

        // act
        var (rangeMin, rangeMax) = CustomerScoringService.GetRangeMinMax( scoringDtos.Select(s => (double) s.Customer.Age).ToArray());

        Assert.Equal(21, rangeMin);
        Assert.Equal(90, rangeMax);
    }

    [Fact]
    public void CanNormaliseAgeCategoryRange()
    {
        var customers = new CustomerSampleRepository().GetAllCustomers();
        var scoringDtos = CustomerScoringService.Initialise(customers, new Coordinates(0.0, 0.0));

        var (rangeMin, rangeMax) = CustomerScoringService.GetRangeMinMax( scoringDtos.Select(s => (double) s.Customer.Age).ToArray());
        var customer = scoringDtos.First();

        var normalised = CustomerScoringService.NormaliseToDefinedRange(customer.Customer.Age, rangeMin, rangeMax);

        Assert.True(normalised >= 1 && normalised <= 10);
    }

    [Fact]
    public void CanNormaliseAllAgeCategoryRange()
    {
        var customers = new CustomerSampleRepository().GetAllCustomers();
        var scoringDtos = CustomerScoringService.Initialise(customers, new Coordinates(0.0, 0.0));
        var (rangeMin, rangeMax) = CustomerScoringService.GetRangeMinMax(scoringDtos.Select(s => (double)s.Customer.Age).ToArray());
        var customerAgeRange = scoringDtos.Select(s => (double)s.Customer.Age).ToArray();

        // act
        var normalisedAgeSet = customerAgeRange.Select(age => CustomerScoringService.NormaliseToDefinedRange(age, rangeMin, rangeMax)).ToArray();

        Assert.All(normalisedAgeSet, age => Assert.True(age >= 0.0 && age <= 10.0));

    }
}
