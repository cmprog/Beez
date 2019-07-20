using System.Collections.Generic;
using System.Reflection;

public sealed class StatisticsSet
{
    private readonly Dictionary<string, double> mValuesByKey = new Dictionary<string, double>();

    private StatisticsSet()
    {
        foreach (var lPropertyInfo in typeof(StatisticKeys).GetProperties(BindingFlags.Static | BindingFlags.Public))
        {
            var lPropertyValue = (string) lPropertyInfo.GetValue(null);
            this.mValuesByKey.Add(lPropertyValue, 0.0);
        }
    }

    public int GetInt32(string key)
    {
        return (int)this.GetDouble(key);
    }

    public double GetDouble(string key)
    {
        if (!this.mValuesByKey.TryGetValue(key, out var lValue))
        {
            throw new System.ArgumentException("Invalid statistics key.", nameof(key));
        }

        return lValue;
    }

    public void Increment(string key)
    {
        this.Increment(key, 1.0);
    }

    public void Increment(string key, double amount)
    {
        this.mValuesByKey[key] = this.GetDouble(key) + amount;
    }

    public static StatisticsSet CreateNew()
    {
        return new StatisticsSet();
    }
}