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
        Assert.Equal( 1.0, normalisedAgeSet.Min());
        Assert.Equal( 10.0, normalisedAgeSet.Max());
    }

    [Fact]
    public void CanNormaliseAllDistanceCategoryRangeAndInvert()
    {
        var customers = new CustomerSampleRepository().GetAllCustomers();
        var scoringDtos = CustomerScoringService.Initialise(customers, new Coordinates(0.0, 0.0));
        var (rangeMin, rangeMax) = CustomerScoringService.GetRangeMinMax(scoringDtos.Select(s => s.DistanceFromBase).ToArray());
        var customerDistanceRange = scoringDtos.Select(s => s.DistanceFromBase).ToArray();

        // act
        var normalisedDistanceSet = customerDistanceRange.Select(distance => CustomerScoringService.NormaliseToDefinedRange(distance, rangeMin, rangeMax, true)).ToArray();

        Assert.All(normalisedDistanceSet, distance => Assert.True(distance >= 0.0 && distance <= 10.0));
        Assert.Equal(0, normalisedDistanceSet.Min());
        Assert.Equal( 9, normalisedDistanceSet.Max());
    }

    [Fact]
    public void CanNormaliseAllAcceptedOffersCategoryRange()
    {
        var customers = new CustomerSampleRepository().GetAllCustomers();
        var scoringDtos = CustomerScoringService.Initialise(customers, new Coordinates(0.0, 0.0));
        var (rangeMin, rangeMax) = CustomerScoringService.GetRangeMinMax(scoringDtos.Select(s => (double) s.Customer.AcceptedOffers).ToArray());
        var customerAcceptedOffersRange = scoringDtos.Select(s => s.Customer.AcceptedOffers).ToArray();

        // act
        var normalisedAcceptedOffersSet = customerAcceptedOffersRange.Select(acceptedOffers => CustomerScoringService.NormaliseToDefinedRange(acceptedOffers, rangeMin, rangeMax)).ToArray();

        Assert.All(normalisedAcceptedOffersSet, acceptedOffers => Assert.True(acceptedOffers >= 0.0 && acceptedOffers <= 10.0));
        Assert.Equal( 1.0, normalisedAcceptedOffersSet.Min());
        Assert.Equal( 10.0, normalisedAcceptedOffersSet.Max());
    }

    [Fact]
    public void CanNormaliseAllCancelledOffersCategoryRangeAndInvert()
    {
        var customers = new CustomerSampleRepository().GetAllCustomers();
        var scoringDtos = CustomerScoringService.Initialise(customers, new Coordinates(0.0, 0.0));
        var (rangeMin, rangeMax) = CustomerScoringService.GetRangeMinMax(scoringDtos.Select(s => (double) s.Customer.CancelledOffers).ToArray());
        var customerCancelledOffersRange = scoringDtos.Select(s => s.Customer.CancelledOffers).ToArray();

        // act
        var normalisedCancelledOffersSet = customerCancelledOffersRange.Select(cancelledOffers => CustomerScoringService.NormaliseToDefinedRange(cancelledOffers, rangeMin, rangeMax, true)).ToArray();

        Assert.All(normalisedCancelledOffersSet, cancelledOffers => Assert.True(cancelledOffers >= 0.0 && cancelledOffers <= 10.0));
        Assert.Equal( 0, normalisedCancelledOffersSet.Min());
        Assert.Equal( 9, normalisedCancelledOffersSet.Max());
    }

    
}
