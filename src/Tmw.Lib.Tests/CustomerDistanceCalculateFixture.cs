using Tmw.Lib.Data;
using Tmw.Lib.Model;

namespace Tmw.Lib.Tests;

public class CustomerDistanceCalculateFixture
{
    [Fact]
    public void CalculateDistance_ReturnsCorrectDistanceWithKnownCoordinates()
    {
        var repository = new CustomerSampleRepository();
        var customers = repository.GetAllCustomers();
        var cust1 = customers[0] with
        {
            Location = new Coordinates(53.339428, -6.257664)
        };
        var baseLocation = new Coordinates(53.2451022, -6.238335);

        // act
        var distance = cust1.Location.DistanceTo(baseLocation);

        Assert.Equal(10.57, distance, 2);
    }
}
