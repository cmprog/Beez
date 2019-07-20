public sealed class WorldState
{
    private readonly int mSeed;

    private WorldState(int seed)
    {
        this.mSeed = seed;
    }

    public WorldSection GetSection(int xPos, int yPos)
    {
        return new WorldSection(this.mSeed, xPos, yPos);
    }

    public static WorldState CreateNew(int seed)
    {
        return new WorldState(seed);
    }

    public static WorldState Load(int seed)
    {
        return new WorldState(seed);
    }
}
