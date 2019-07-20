using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public sealed class StatisticsSet
{
    private readonly Dictionary<string, double> mValuesByKey = new Dictionary<string, double>();

    private static readonly string[] sKeyArray;

    static StatisticsSet()
    {
        var lPropertyInfoList = typeof(StatisticKeys)
            .GetProperties(BindingFlags.Static | BindingFlags.Public)
            .ToList();

        sKeyArray = new string[lPropertyInfoList.Count];

        for (var lIndex = 0; lIndex < sKeyArray.Length; lIndex++)
        {
            var lPropertyInfo = lPropertyInfoList[lIndex];
            sKeyArray[lIndex] = (string)lPropertyInfo.GetValue(null);
        }
    }

    private StatisticsSet()
    {
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

    public string GetString(string key)
    {
        return this.GetDouble(key).ToString();
    }

    public string GetString(string key, string format)
    {
        return this.GetDouble(key).ToString(format);
    }

    public void Set(string key, double value)
    {
        this.mValuesByKey[key] = value;
    }

    public void SetFrom(StatisticsSet other)
    {
        foreach (var lKey in other.mValuesByKey.Keys)
        {
            this.Set(lKey, other.GetDouble(lKey));
        }
    }

    public void SetFrom(StatisticsSet other, string key)
    {
        this.Set(key, other.GetDouble(key));
    }

    public void Decrement(string key, double amount)
    {
        this.mValuesByKey[key] = this.GetDouble(key) - amount;
    }

    public void Increment(string key)
    {
        this.Increment(key, 1.0);
    }

    public void Increment(string key, double amount)
    {
        this.mValuesByKey[key] = this.GetDouble(key) + amount;
    }

    public void IncrementFrom(StatisticsSet other)
    {
        foreach (var lKey in other.mValuesByKey.Keys)
        {
            this.IncrementFrom(other, lKey);
        }
    }

    public void IncrementFrom(StatisticsSet other, string key)
    {
        this.Set(key, other.GetDouble(key));
    }

    public static StatisticsSet CreateDefault()
    {
        var lSet = new StatisticsSet();

        foreach (var lKey in sKeyArray)
        {
            lSet.mValuesByKey.Add(lKey, 0.0);
        }

        return lSet;
    }

    public static StatisticsSet CreateEmpty()
    {
        return new StatisticsSet();
    }
}