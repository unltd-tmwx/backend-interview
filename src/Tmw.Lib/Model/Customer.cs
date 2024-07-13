namespace Tmw.Lib.Model;

public record Customer(
    Guid Id,
    string Name,
    int Age,
    int AcceptedOffers,
    int CancelledOffers,
    int AverageReplyTime,
    Coordinates Location,
    double Score = 0.0
    );

public record Coordinates(double Latitude, double Longitude)
{
    // nicked from internet
    public double DistanceTo(Coordinates other)
    {
        double earthRadiusKm = 6371;

        double dLat = DegreesToRadians(other.Latitude - Latitude);
        double dLon = DegreesToRadians(other.Longitude - Longitude);

        double lat1 = DegreesToRadians(Latitude);
        double lat2 = DegreesToRadians(other.Latitude);

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return earthRadiusKm * c;
    }

    private double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}
