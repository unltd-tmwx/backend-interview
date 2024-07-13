using Tmw.Lib.Data.sample_data;
using Tmw.Lib.Model;

namespace Tmw.Lib.Data;

public class CustomerSampleRepository : ICustomerRepository
{
    public Customer[] GetAllCustomers()
    {
        var json = SampleDataHelper.ReadEmbeddedCustomerData();
        var customerDtos = SampleDataHelper.DeserializeCustomerData(json);
        return customerDtos.Select(dto => dto.Map()).ToArray();
    }
}
