using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Tmw.Lib.Data.sample_data;

internal class SampleDataHelper
{
    internal static string ReadEmbeddedCustomerData()
    {
        var xx = Assembly.GetExecutingAssembly().GetManifestResourceNames();
        var thisAssembly = Assembly.GetExecutingAssembly().GetName();
        var name = thisAssembly.Name;
        using var stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream($"{name}.Data.sample_data.customers.json");
        using var reader = new StreamReader(stream, Encoding.UTF8);
        return reader.ReadToEnd();
    }

    internal static CustomerDto[] DeserializeCustomerData(string json)
    {
        return JsonSerializer.Deserialize<CustomerDto[]>(json) ?? [];
    }
}
