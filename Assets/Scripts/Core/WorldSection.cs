using System.Collections.Generic;

public sealed class WorldSection
{

    public WorldSection(int gameSeed, int xPos, int yPos)
    {
        this.PositionX = xPos;
        this.PositionY = yPos;

        var lGenerator = new WorldSectionStateGenerator(gameSeed, xPos, yPos);
        
        var lFlowerCount = lGenerator.NextValue(1, 4);
        var lFlowerArray = new Flower[lFlowerCount];
        this.Flowers = lFlowerArray;

        for (var lIndex = 0; lIndex < lFlowerCount; lIndex++)
        {
            lFlowerArray[lIndex] = new Flower(lGenerator);
        }
    }

    public int PositionX { get; }

    public int PositionY { get; }

    public IEnumerable<Flower> Flowers { get; }
}