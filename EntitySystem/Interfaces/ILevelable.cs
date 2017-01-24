using Systems.EntitySystem.Leveling;

namespace Systems.EntitySystem.Interfaces
{
    /// <summary>
    /// Add this interface to your concrete Entity Implementation
    /// </summary>
    public interface ILevelable
    {
        EntityLevel Level { get; set; }
    }
}

