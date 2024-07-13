using Tmw.Lib.Model;

namespace Tmw.Lib.Services;

internal record CustomerScoring(
    Customer Customer,
    double AgeScore,
    double DistanceFromBase,
    double DistanceScore,
    double AcceptedOffersScore,
    double CancelledOffersScore,
    double AverageReplyTimeScore)
{
    internal double TotalScore => 
        (AgeScore * WeightingCategories.DemographicAge)
        + (DistanceScore * WeightingCategories.DemographicDistance)
        + (AcceptedOffersScore * WeightingCategories.BehaviourAcceptedOffers)
        + (CancelledOffersScore * WeightingCategories.BehaviourCancelledOffers)
        + (AverageReplyTimeScore * WeightingCategories.BehaviourReplyTime);
};
