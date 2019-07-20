// Original concept taking from
// https://gamedevelopment.tutsplus.com/tutorials/using-the-composite-design-pattern-for-an-rpg-attributes-system--gamedev-243
public class BaseAttribute
{
    private readonly double mBaseValue;
    private readonly double mBaseMultiplier;

    public BaseAttribute(double value)
        : this(value, 0.0)
    {

    }

    public BaseAttribute(double value, double multiplier)
    {
        this.Value = value;
        this.Multipler = multiplier;
    }

    public double Value { get; }

    public double Multipler { get; }
}
