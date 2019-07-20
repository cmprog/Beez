using System.Collections.Generic;

public sealed class AttributeSet
{
    private readonly Dictionary<string, PlayerAttribute> mAttributesByKey;

    public AttributeSet()
    {
        this.mAttributesByKey = new Dictionary<string, PlayerAttribute>();

        this.mAttributesByKey.Add(AttributeKeys.BeeSpeed, new PlayerAttribute(0.1));
        this.mAttributesByKey.Add(AttributeKeys.BeeTurn, new PlayerAttribute(5.0));
        this.mAttributesByKey.Add(AttributeKeys.SalvageRate, new PlayerAttribute(0.1));
        this.mAttributesByKey.Add(AttributeKeys.PollenCapacity, new PlayerAttribute(5.0));
        this.mAttributesByKey.Add(AttributeKeys.HoneyBatchCount, new PlayerAttribute(1.0));
        this.mAttributesByKey.Add(AttributeKeys.PollenRequirement, new PlayerAttribute(10.0));
        this.mAttributesByKey.Add(AttributeKeys.HoneyYield, new PlayerAttribute(1.0));
        this.mAttributesByKey.Add(AttributeKeys.PollenValue, new PlayerAttribute(1.0));
        this.mAttributesByKey.Add(AttributeKeys.PollenRegen, new PlayerAttribute(0.0));
        this.mAttributesByKey.Add(AttributeKeys.FlowerBonus, new PlayerAttribute(0.1));
    }

    public PlayerAttribute Get(string key)
    {
        if (!this.mAttributesByKey.TryGetValue(key, out var lAttribute))
        {
            throw new System.ArgumentException("Invalid attribute key.", nameof(key));
        }

        return lAttribute;
    }

    public double GetDouble(string key)
    {
        if (!this.mAttributesByKey.TryGetValue(key, out var lAttribute))
        {
            throw new System.ArgumentException("Invalid attribute key.", nameof(key));
        }

        return lAttribute.CalculateValue();
    }

    public float GetSingle(string key)
    {
        return (float)this.GetDouble(key);
    }
}