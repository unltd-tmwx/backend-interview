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
}
