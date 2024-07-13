namespace Tmw.Lib.Services;

// This could be a configuration file, but for the sake of simplicity, I'm using a static class.
internal class WeightingCategories
{
    public const double DemographicAge = 0.1;
    public const double DemographicDistance = 0.1;
    public const double BehaviourAcceptedOffers = 0.3;
    public const double BehaviourCancelledOffers = 0.3;
    public const double BehaviourReplyTime = 0.2;
}
