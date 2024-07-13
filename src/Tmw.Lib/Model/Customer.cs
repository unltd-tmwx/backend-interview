namespace Tmw.Lib.Model;

public record Customer(
    Guid Id,
    string Name,
    int Age,
    int AcceptedOffers,
    int CancelledOffers,
    int AverageReplyTime,
    Coordinates Location
    );

public record Coordinates(double Latitude, double Longitude);
