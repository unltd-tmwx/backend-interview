using Tmw.Lib.Model;

namespace Tmw.Lib.Data;

public interface ICustomerRepository
{
    Customer[] GetAllCustomers();
}
