using Systems.EntitySystem.Targeting;

namespace Systems.EntitySystem.Interfaces
{
    /// <summary>
    /// Add this interface to your concrete Entity Implementation
    /// </summary>
    public interface ITarget
    {
        Target target { get; }
    }
}
