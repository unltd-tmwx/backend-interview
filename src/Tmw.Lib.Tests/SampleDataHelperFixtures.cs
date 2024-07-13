using System.Text.Json;
using Tmw.Lib.Data.sample_data;

namespace Tmw.Lib.Tests;

public class SampleDataHelperFixtures
{
    [Fact]
    public void CanReadEmbeddedSampleDataFile()
    {
        var json = SampleDataHelper.ReadEmbeddedCustomerData();
        Assert.NotNull(json);
    }

    [Fact]
    public void CanDeserializeSampleDataItem()
    {
        var sampleJson = """{"id":"541d25c9-9500-4265-8967-240f44ecf723","name":"Samir Pacocha","age":46,"acceptedOffers":49,"canceledOffers":92,"averageReplyTime":2598,"location":{"latitude":"46.7110","longitude":"-63.1150"}}"""; ;

        // act
        var customerDto = JsonSerializer.Deserialize<CustomerDto>(sampleJson);

        Assert.NotNull(customerDto);
        Assert.Equal("Samir Pacocha", customerDto.Name);
        Assert.Equal(46, customerDto.Age);
        Assert.Equal(49, customerDto.AcceptedOffers);
        Assert.Equal(92, customerDto.CancelledOffers);
        Assert.Equal(2598, customerDto.AverageReplyTime);
        Assert.Equal("46.7110", customerDto.Location.Latitude);
        Assert.Equal("-63.1150", customerDto.Location.Longitude);
    }

    [Fact]
    public void CanDeserializeSampleDataJson()
    {
        var json = SampleDataHelper.ReadEmbeddedCustomerData();

        // act
        var customerDtos = SampleDataHelper.DeserializeCustomerData(json);

        Assert.NotNull(customerDtos);
        Assert.True(customerDtos.Length > 0);
        var customerDto = customerDtos[0];
        Assert.Equal("Samir Pacocha", customerDto.Name);
        Assert.Equal(46, customerDto.Age);
        Assert.Equal(49, customerDto.AcceptedOffers);
        Assert.Equal(92, customerDto.CancelledOffers);
        Assert.Equal(2598, customerDto.AverageReplyTime);
        Assert.Equal("46.7110", customerDto.Location.Latitude);
        Assert.Equal("-63.1150", customerDto.Location.Longitude);
    }

    [Fact]
    public void CanMapToCustomer()
    {
        var json = SampleDataHelper.ReadEmbeddedCustomerData();
        var customerDtos = SampleDataHelper.DeserializeCustomerData(json);
        var customerDto = customerDtos[0];

        // act 
        var customer = customerDto.Map();

        Assert.NotNull(customer);
        Assert.Equal(customerDto.Id, customer.Id);
        Assert.Equal(customerDto.Name, customer.Name);
        Assert.Equal(customerDto.Age, customer.Age);
        Assert.Equal(customerDto.AcceptedOffers, customer.AcceptedOffers);
        Assert.Equal(customerDto.CancelledOffers, customer.CancelledOffers);
        Assert.Equal(customerDto.AverageReplyTime, customer.AverageReplyTime);
        Assert.Equal(46.7110, customer.Location.Latitude);
        Assert.Equal(-63.1150, customer.Location.Longitude);
    }
}