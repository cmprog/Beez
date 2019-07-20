public static class StatisticKeys
{
    /// <summary>
    /// The number of physical pollen collistions the player has performed.
    /// </summary>
    public static string PollenPickupCount { get; } = "Pollen.PickupCount";

    /// <summary>
    /// The total value of pollen collected by the player.
    /// </summary>
    public static string TotalPollenCollected { get; } = "Pollen.TotalValue";

    /// <summary>
    /// The number of total flower collections the player has performed.
    /// </summary>
    public static string TotalFlowersCollected { get; } = "Flower.BonusCount";

    public static string TotalHoneyBatches { get; } = "Honey.BatchCount";

    public static string TotalHoneyGenerated { get; } = "Honey.TotalValue";

    /// <summary>
    /// The total number of ellapsed days.
    /// </summary>
    public static string TotalDays { get; } = "Days.TotalElapsed";

    /// <summary>
    /// The total distance traveled by the player.
    /// </summary>
    public static string TotalDistanceTraveled { get; } = "Distance.TotalTraveled";

    /// <summary>
    /// The furthest absolute distance traveled by the player in a single day.
    /// </summary>
    public static string FurthestDistanceTraveled { get; } = "Distance.FurthestTraveled";
}
