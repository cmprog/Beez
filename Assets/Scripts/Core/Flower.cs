using System.Collections.Generic;

public sealed class Flower
{
    public Flower(WorldSectionStateGenerator generator)
    {
        this.Id = generator.NextEntityId();
        this.PositionX = generator.NextPosition();
        this.PositionY = generator.NextPosition();

        var lPollenCount = generator.NextValue(2, 10);
        var lPollenArray = new Pollen[lPollenCount];
        this.Pollen = lPollenArray;

        for (var lIndex = 0; lIndex < lPollenCount; lIndex++)
        {
            lPollenArray[lIndex] = new Pollen(generator);
        }
    }

    public SectionEntityId Id { get; }

    public float PositionX { get; }

    public float PositionY { get; }

    public IEnumerable<Pollen> Pollen { get; }
}
