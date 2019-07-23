using System;

public sealed class DayManager
{
    private readonly Random mRandom;
    private readonly AttributeSet mAttributes;

    public DayManager(AttributeSet attributes, int gameSeed, int dayIndex)
    {
        this.mAttributes = attributes;
        this.mRandom = new Random(HashCode.Compute(gameSeed, dayIndex));
    }

    public double NextTimeToEnemy(double distanceFromHive)
    {
        var lEnemyRateMax = this.mAttributes.GetDouble(AttributeKeys.EnemySpawnRateMax);
        var lEnemyRateMin = this.mAttributes.GetDouble(AttributeKeys.EnemySpawnRateMin);

        // TODO: account for distance from the hive

        // max is less than min because 5/s is faster than 2/s
        var lEnemyRateRange = lEnemyRateMin - lEnemyRateMax;
        return lEnemyRateMax + (this.mRandom.NextDouble() * lEnemyRateRange);
    }
}
