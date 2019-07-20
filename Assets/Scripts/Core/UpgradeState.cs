using System.Collections.Generic;

public sealed class UpgradeState
{
    private readonly Dictionary<string, List<UpgradeDefinition>> mUpgradesByCategory = new Dictionary<string, List<UpgradeDefinition>>
    {
        {"Bee", new List<UpgradeDefinition> {
            new UpgradeDefinition {
                Name = "Bee 1",
                Description = "[TODO]",
                Cost = 1.0,
                Effects = new List<UpgradeEffectDefinition>
                {
                    new UpgradeEffectDefinition
                    {
                        AttributeKey = AttributeKeys.BeeSpeed,
                        Bonus = new AttributeBonus(0.1)
                    },
                    new UpgradeEffectDefinition
                    {
                        AttributeKey = AttributeKeys.BeeTurn,
                        Bonus = new AttributeBonus(5.0)
                    },
                    new UpgradeEffectDefinition
                    {
                        AttributeKey = AttributeKeys.PollenCapacity,
                        Bonus = new AttributeBonus(25)
                    }
                },
            },
            new UpgradeDefinition {
                Name = "Bee 1",
                Description = "[TODO]",
                Cost = 1.0,
                Effects = new List<UpgradeEffectDefinition>
                {
                    new UpgradeEffectDefinition
                    {
                        AttributeKey = AttributeKeys.BeeSpeed,
                        Bonus = new AttributeBonus(0.1)
                    },
                    new UpgradeEffectDefinition
                    {
                        AttributeKey = AttributeKeys.BeeTurn,
                        Bonus = new AttributeBonus(5.0)
                    },
                    new UpgradeEffectDefinition
                    {
                        AttributeKey = AttributeKeys.PollenCapacity,
                        Bonus = new AttributeBonus(20.0)
                    }
                },
            },
        }},
        { "Hive", new List<UpgradeDefinition> {
            new UpgradeDefinition {
                Name = "Hive 1",
                Description = "[TODO]",
                Cost = 1.0,
                Effects = new List<UpgradeEffectDefinition>
                {
                    new UpgradeEffectDefinition
                    {
                        AttributeKey = AttributeKeys.PollenRequirement,
                        Bonus = new AttributeBonus(-1.0)
                    },
                    new UpgradeEffectDefinition
                    {
                        AttributeKey = AttributeKeys.HoneyYield,
                        Bonus = new AttributeBonus(1)
                    }
                },
            },
        }},
        { "Flowers", new List<UpgradeDefinition> {
            new UpgradeDefinition {
                Name = "Flower 1",
                Description = "[TODO]",
                Cost = 1.0,
                Effects = new List<UpgradeEffectDefinition>
                {
                    new UpgradeEffectDefinition
                    {
                        AttributeKey = AttributeKeys.PollenValue,
                        Bonus = new AttributeBonus(0.5)
                    },
                    new UpgradeEffectDefinition
                    {
                        AttributeKey = AttributeKeys.FlowerBonus,
                        Bonus = new AttributeBonus(0.1)
                    }
                },
            },
        }},
    };

    private readonly Dictionary<string, int> mNextIndexByCategory = new Dictionary<string, int>();
    private int mNextUpgradeDefinitionId;

    private UpgradeState()
    {
        this.Categories = new List<string>(this.mUpgradesByCategory.Keys);
    }

    public IList<string> Categories { get; }

    private int GetNextCategoryIndex(string category)
    {
        int lIndex;
        // The default value is zero, so whether this succeeds or fails is okay
        this.mNextIndexByCategory.TryGetValue(category, out lIndex);
        return lIndex;
    }

    public bool Purchase(string category, GameState gameState)
    {
        var lDefinition = this.NextUpgradeForDefinition(category);
        if (lDefinition == null) return false;

        if (lDefinition.Cost > gameState.Hive.HoneyAmount) return false;

        gameState.Hive.HoneyAmount -= lDefinition.Cost;
        foreach (var lEffect in lDefinition.Effects)
        {
            var lAttribute = gameState.Attributes.Get(lEffect.AttributeKey);

            if (lEffect.Rank.HasValue)
            {
                lAttribute.Add(lEffect.Bonus, lEffect.Rank.Value);
            }
            else
            {
                lAttribute.Add(lEffect.Bonus);
            }
        }

        var lDefinitionIndex = this.GetNextCategoryIndex(category);
        this.mNextIndexByCategory[category] = (lDefinitionIndex + 1);
        return true;
    }

    public UpgradeDefinition NextUpgradeForDefinition(string category)
    {
        int lIndex = this.GetNextCategoryIndex(category);

        List<UpgradeDefinition> lDefinitionList;
        if (!this.mUpgradesByCategory.TryGetValue(category, out lDefinitionList))
        {
            throw new System.ArgumentOutOfRangeException(nameof(category));
        }

        if (lIndex >= lDefinitionList.Count) return null;
        return lDefinitionList[lIndex];
    }

    public static UpgradeState CreateNew()
    {
        return new UpgradeState();
    }
}
