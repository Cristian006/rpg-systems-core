namespace Systems.StatSystem.Interfaces
{
    /// <summary>
    /// Add this interface to your concrete Entity Implementation
    /// </summary>
    public interface IStatCollector
    {
        StatCollection Stats { get; set;}
    }
}