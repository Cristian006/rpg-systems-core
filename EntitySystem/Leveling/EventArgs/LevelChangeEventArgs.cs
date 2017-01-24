namespace Systems.EntitySystem.Leveling.EventArgs
{
    public class LevelChangeEventArgs : System.EventArgs
    {
        public int NewLevel { get; private set; }
        public int OldLevel { get; private set; }

        public LevelChangeEventArgs(int newLevel, int oldLevel)
        {
            NewLevel = newLevel;
            OldLevel = oldLevel;
        }
    }
}