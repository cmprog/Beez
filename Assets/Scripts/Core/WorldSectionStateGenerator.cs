public sealed class WorldSectionStateGenerator
{
    private readonly System.Random mRandom;
    private readonly int mPositionX;
    private readonly int mPositionY;

    private int mNextEntityId;

    public WorldSectionStateGenerator(int gameSeed, int positionX, int positionY)
    {
        this.mRandom = new System.Random(HashCode.Compute(gameSeed, positionX, positionY));
        this.mPositionX = positionX;
        this.mPositionY = positionY;
    }

    public int NextValue(int minValue, int maxValue)
    {
        return this.mRandom.Next(minValue, maxValue);
    }

    public float NextValue(float minValue, float maxValue)
    {
        var lRandom = maxValue - minValue;
        var lOffset = (float)this.mRandom.NextDouble();
        return minValue + lOffset;
    }

    public float NextPosition()
    {
        return (float)this.mRandom.NextDouble() - 0.5f;
    }

    public SectionEntityId NextEntityId()
    {
        var lValue = this.mNextEntityId;
        this.mNextEntityId++;
        return new SectionEntityId(this.mPositionX, this.mPositionY, lValue);
    }
}
