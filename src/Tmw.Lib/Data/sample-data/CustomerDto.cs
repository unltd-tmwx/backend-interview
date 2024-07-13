using System.Text.Json.Serialization;
using Tmw.Lib.Model;
using static Tmw.Lib.Data.sample_data.CustomerDto;

namespace Tmw.Lib.Data.sample_data;

internal record CustomerDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("age")] int Age,
    [property: JsonPropertyName("acceptedOffers")] int AcceptedOffers,
    [property: JsonPropertyName("canceledOffers")] int CancelledOffers,
    [property: JsonPropertyName("averageReplyTime")] int AverageReplyTime,
    [property: JsonPropertyName("location")] CoordinatesDto Location
)
{
    internal record CoordinatesDto(
        [property: JsonPropertyName("latitude")] string Latitude,
        [property: JsonPropertyName("longitude")] string Longitude);

    internal Customer Map() => new(
       Id,
       Name,
       Age,
       AcceptedOffers,
       CancelledOffers,
       AverageReplyTime,
       new Coordinates(double.Parse(Location.Latitude), double.Parse(Location.Longitude))
       );
};
