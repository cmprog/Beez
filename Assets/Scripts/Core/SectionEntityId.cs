using System;

/// <summary>
/// A unique identifier for each spawned entity in the world.
/// </summary>
public struct SectionEntityId : IEquatable<SectionEntityId>
{
    private readonly int mSectionPosX;
    private readonly int mSectionPosY;
    private readonly int mEntityId;

    public SectionEntityId(int sectionPosX, int sectionPosY, int entityId)
    {
        this.mSectionPosX = sectionPosX;
        this.mSectionPosY = sectionPosY;
        this.mEntityId = entityId;
    }

    public bool Equals(SectionEntityId other)
    {
        throw new NotImplementedException();
    }

    public override int GetHashCode()
    {
        return HashCode.Compute(this.mSectionPosX, this.mSectionPosY, this.mEntityId);
    }

    public override bool Equals(object obj)
    {
        return (obj is SectionEntityId) && this.Equals((SectionEntityId)obj);
    }

    public override string ToString()
    {
        return string.Format("({0}, {1}, {2})", this.mSectionPosX, this.mSectionPosY, this.mEntityId);
    }

    public static SectionEntityId Parse(string text)
    {
        text = text.Trim('(', ')');
        var lTokens = text.Split(',');
        if (lTokens.Length != 3) throw new FormatException();
        var lSectionPosX = int.Parse(lTokens[0].Trim());
        var lSectionPosY = int.Parse(lTokens[1].Trim());
        var lEntityId = int.Parse(lTokens[2].Trim());
        return new SectionEntityId(lSectionPosX, lSectionPosY, lEntityId);
    }
}