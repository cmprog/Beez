using System;

public sealed class HiveState
{
    private HiveState()
    {

    }

    public double PollenAmount { get; set; }

    public double HoneyAmount { get; set; }

    /// <summary>
    /// The amount of pollen the hive can process per day.
    /// </summary>
    public double PollenProcessingRate { get; set; }

    /// <summary>
    /// The amount of pollen required to yield a single unit of honey.
    /// </summary>
    public double RequiredPollenPerHoney { get; set; }

    /// <summary>
    /// An additive bonus to the amout of honey yielded per batch.
    /// </summary>
    public double HoneyYieldBonus { get; set; }

    /// <summary>
    /// A multiplier for each honey yielding cycle.
    /// </summary>
    public double HoneyYieldMultiplier { get; set; }

    public HoneyGenerationResult GenerateHoney()
    {
        var lMaximumBatchCount = Math.Floor(this.PollenProcessingRate);
        var lTheoreticalBatchCount = Math.Floor(this.PollenAmount / this.RequiredPollenPerHoney);
        var lBatchCount = (long) Math.Min(lMaximumBatchCount, lTheoreticalBatchCount);

        var lPollenRequired = lBatchCount * this.RequiredPollenPerHoney;
        var lHoneyGenerated = (lBatchCount + this.HoneyYieldBonus) * this.HoneyYieldMultiplier;

        this.PollenAmount -= lPollenRequired;
        this.HoneyAmount = lHoneyGenerated;

        var lResult = new HoneyGenerationResult();
        lResult.BatchesProcessed = lBatchCount;
        lResult.PollenUsed = lPollenRequired;
        lResult.HoneyGenerated = lHoneyGenerated;
        return lResult;
    }

    public static HiveState CreateNew()
    {
        var lHiveState = new HiveState();
        lHiveState.PollenProcessingRate = 1.0;
        lHiveState.RequiredPollenPerHoney = 10.0;
        lHiveState.HoneyYieldBonus = 0.0;
        lHiveState.HoneyYieldMultiplier = 1.0;
        return lHiveState;
    }

    public static HiveState Load()
    {
        return new HiveState();
    }
}