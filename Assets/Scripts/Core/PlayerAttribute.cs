using System.Collections.Generic;
using System.Linq;

public class PlayerAttribute : BaseAttribute
{
    private readonly Dictionary<int, List<AttributeBonus>> mBonusesByRank;

    public PlayerAttribute(double startingValue)
        : base(startingValue)
    {
        this.mBonusesByRank = new Dictionary<int, List<AttributeBonus>>();
    }

    public void Add(AttributeBonus rawBonus)
    {
        this.Add(rawBonus, 0);
    }

    public void Add(AttributeBonus rawBonus, int rank)
    {
        if (!this.mBonusesByRank.TryGetValue(rank, out var lBonusCollection))
        {
            lBonusCollection = new List<AttributeBonus>();
            this.mBonusesByRank.Add(rank, lBonusCollection);
        }

        lBonusCollection.Add(rawBonus);
    }

    public void Remove(AttributeBonus rawBonus)
    {
        var lKey = 0;
        var lRemoveRank = false;

        foreach (var lKeyValuePair in this.mBonusesByRank)
        {
            if (lKeyValuePair.Value.Remove(rawBonus))
            {
                // We can't remove in the enumerator
                lKey = lKeyValuePair.Key;
                lRemoveRank = lKeyValuePair.Value.Count == 0;
                break;                
            }
        }

        if (lRemoveRank)
        {
            this.mBonusesByRank.Remove(lKey);
        }
    }

    public double CalculateValue()
    {
        return this.mBonusesByRank.Keys.OrderBy(x => x).Aggregate(this.Value, this.ApplyBonuses);
    }

    private double ApplyBonuses(double value, int rank)
    {
        var lBonusValue = 0.0;
        var lBonusMultiplier = 0.0;

        foreach (var lAttribute in this.mBonusesByRank[rank])
        {
            lBonusValue += lAttribute.Value;
            lBonusMultiplier += lAttribute.Multipler;
        }

        value += lBonusValue;
        return value * (1.0 + lBonusMultiplier);
    }
}
