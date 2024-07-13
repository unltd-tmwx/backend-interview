using Tmw.Lib.Data;

namespace Tmw.Lib.Tests;

public class CustomerRepositoryFixtures
{
    [Fact]
    public void GetAllCustomers_ReturnsCustomers()
    {
        var repository = new CustomerSampleRepository();

        // act
        var customers = repository.GetAllCustomers();

        Assert.NotNull(customers);
        Assert.NotEmpty(customers);
    }

    [Fact]
    public void GetAllCustomers_ReturnsCustomersWithCorrectData()
    {
        var repository = new CustomerSampleRepository();

        // act
        var customers = repository.GetAllCustomers();

        Assert.NotNull(customers);
        Assert.NotEmpty(customers);
        var customer = customers[0];
        Assert.Equal("Samir Pacocha", customer.Name);
        Assert.Equal(46, customer.Age);
        Assert.Equal(49, customer.AcceptedOffers);
        Assert.Equal(92, customer.CancelledOffers);
        Assert.Equal(2598, customer.AverageReplyTime);
        Assert.Equal(46.7110, customer.Location.Latitude);
        Assert.Equal(-63.1150, customer.Location.Longitude);
    }
}
