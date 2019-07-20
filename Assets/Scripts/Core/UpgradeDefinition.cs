using System.Collections.Generic;

public sealed class UpgradeDefinition
{
    public string Name { get; set; }

    public string Description { get; set; }

    public double Cost { get; set; }

    public List<UpgradeEffectDefinition> Effects { get; set; }
}