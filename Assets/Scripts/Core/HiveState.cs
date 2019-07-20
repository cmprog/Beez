using System;

public sealed class HiveState
{
    private HiveState()
    {

    }

    public StatisticsSet GenerateHoney(StatisticsSet stats, AttributeSet attributes)
    {
        var lPollenRequirement = attributes.GetDouble(AttributeKeys.PollenRequirement);

        var lMaximumBatchCount = Math.Floor(attributes.GetDouble(AttributeKeys.HoneyBatchCount));
        var lTheoreticalBatchCount = Math.Floor(stats.GetDouble(StatisticKeys.RemainingPollen) / lPollenRequirement);
        var lBatchCount = (long) Math.Min(lMaximumBatchCount, lTheoreticalBatchCount);

        var lPollenRequired = lBatchCount * lPollenRequirement;
        var lHoneyGenerated = lBatchCount * attributes.GetDouble(AttributeKeys.HoneyYield);

        stats.Decrement(StatisticKeys.RemainingPollen, lPollenRequired);
        stats.Increment(StatisticKeys.HoneyAvailable, lHoneyGenerated);

        var lResult = StatisticsSet.CreateEmpty();
        lResult.Set(StatisticKeys.HoneyBatchCount, lBatchCount);
        lResult.Set(StatisticKeys.PollenProcessed, lPollenRequired);
        lResult.Set(StatisticKeys.HoneyProduced, lHoneyGenerated);
        return lResult;
    }

    public static HiveState CreateNew()
    {
        return new HiveState();
    }
}