using Tmw.Lib.Model;

namespace Tmw.Lib.Services;

public interface ICustomerScoringService
{
    Customer[] GetCustomersByScore(Coordinates baseCoordinates);
}
