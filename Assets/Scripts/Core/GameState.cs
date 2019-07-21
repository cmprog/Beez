using System.IO;
using UnityEngine;

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

    public void FinializeDay(StatisticsSet dayStats)
    {
        this.Statistics.Increment(StatisticKeys.TotalDays);

        var lRemainingPollen = dayStats.GetDouble(StatisticKeys.RemainingPollen);
        var lCollectedPollen = dayStats.GetDouble(StatisticKeys.PollenCollected);
        
        var lSalvagedPollen = lRemainingPollen * this.Attributes.GetDouble(AttributeKeys.SalvageRate);
        var lTotalCollectedPollen = lCollectedPollen += lSalvagedPollen;        
        this.Statistics.Increment(StatisticKeys.RemainingPollen, lTotalCollectedPollen);

        var lHoneyGenStats = this.Hive.GenerateHoney(this.Statistics, this.Attributes);
        this.Statistics.IncrementFrom(lHoneyGenStats);

        dayStats.SetFrom(lHoneyGenStats);
        dayStats.Set(StatisticKeys.SalvagedPollen, lSalvagedPollen);
    }

    public static GameState CreateNew()
    {
        var lGameState = new GameState();

        if (Debug.isDebugBuild)
        {
            lGameState.Seed = 42;
        }
        else
        {
            lGameState.Seed = Random.Range(int.MinValue, int.MaxValue);
        }

        lGameState.Attributes = new AttributeSet();
        lGameState.World = WorldState.CreateNew(lGameState.Seed);
        lGameState.Hive = HiveState.CreateNew();
        lGameState.Statistics = StatisticsSet.CreateDefault();
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
