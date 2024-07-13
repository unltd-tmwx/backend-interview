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
}