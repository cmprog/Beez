using System.IO;

public sealed class GameState
{
    private GameState()
    {
        
    }

    public int Seed { get; private set; }

    public AttributeSet Attributes { get; private set; }

    public WorldState World { get; private set; }

    public HiveState Hive { get; private set; }

    public UpgradeState Upgrades { get; private set; }

    public StatisticsSet Statistics { get; private set; }

    public DayFinalizationResult FinializeDay(DaySummaryInput input)
    {
        this.Statistics.Increment(StatisticKeys.TotalDays);

        var lSalvagedPollen = input.RemainingBeePollen * this.Attributes.GetDouble(AttributeKeys.SalvageRate);
        var lTotalCollectedPollen = input.CollectedPollen += lSalvagedPollen;
        this.Hive.PollenAmount += lTotalCollectedPollen;

        var lHoneyGenerationResult = this.Hive.GenerateHoney();
        this.Statistics.Increment(StatisticKeys.TotalHoneyBatches, lHoneyGenerationResult.BatchesProcessed);
        this.Statistics.Increment(StatisticKeys.TotalHoneyGenerated, lHoneyGenerationResult.HoneyGenerated);
                
        var lResult = new DayFinalizationResult();
        lResult.DayNumber = this.Statistics.GetInt32(StatisticKeys.TotalDays);
        lResult.CollectedPollen = input.CollectedPollen;
        lResult.SalvagedPollen = lSalvagedPollen;
        lResult.BatchesProcessed = lHoneyGenerationResult.BatchesProcessed;
        lResult.PollenUsed = lHoneyGenerationResult.PollenUsed;
        lResult.GeneratedHoney = lHoneyGenerationResult.HoneyGenerated;
        return lResult;        
    }

    public static GameState CreateNew()
    {
        var lGameState = new GameState();
        lGameState.Seed = 42; // UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        lGameState.Attributes = new AttributeSet();
        lGameState.World = WorldState.CreateNew(lGameState.Seed);
        lGameState.Hive = HiveState.CreateNew();
        lGameState.Statistics = StatisticsSet.CreateNew();
        lGameState.Upgrades = UpgradeState.CreateNew();
        return lGameState;
    }

    public static GameState Load(Stream dataStream)
    {
        // TODO
        return new GameState();
    }

    public void SaveTo(Stream dataStream)
    {
        // TODO
    }
}
