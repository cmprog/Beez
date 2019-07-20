public static class StatisticKeys
{
    public static string RemainingPollen { get; } = "Pollen.Remaining";

    public static string SalvagedPollen { get; } = "Pollen.Salvaged";

    public static string HiveDropOffCount { get; } = "Hive.DropOffCount";

    /// <summary>
    /// The number of physical pollen collistions the player has performed.
    /// </summary>
    public static string PollenPickupCount { get; } = "Pollen.PickupCount";

    /// <summary>
    /// The total value of pollen collected by the player.
    /// </summary>
    public static string PollenCollected { get; } = "Pollen.TotalValue";

    public static string PollenProcessed { get; } = "Pollen.Processed";

    /// <summary>
    /// The number of total flower collections the player has performed.
    /// </summary>
    public static string TotalFlowersCollected { get; } = "Flower.BonusCount";

    public static string HoneyBatchCount { get; } = "Honey.BatchCount";

    public static string HoneyProduced { get; } = "Honey.TotalValue";

    public static string HoneyAvailable { get; } = "Honey.Available";

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
