public sealed class Pollen
{
    public Pollen(WorldSectionStateGenerator generator)
    {
        this.Id = generator.NextEntityId();
        // The position is a relative value. The UI can scale
        // these values to the 'true' range.
        this.PositionX = generator.NextValue(-1.0f, 1.0f);
        this.PositionY = generator.NextValue(-1.0f, 1.0f);
    }

    public SectionEntityId Id { get; }

    public float PositionX { get; }

    public float PositionY { get; }
}
