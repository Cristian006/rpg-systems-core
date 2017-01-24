namespace Systems.EntitySystem.Interfaces
{
    /// <summary>
    /// Add this interface to your concrete Entity Implementation
    /// </summary>
    public interface IDestructible
    {
        void TakeDamage(int damage);
        void Death();
    }
}