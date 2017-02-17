namespace Systems.ItemSystem.Interfaces
{
    interface IThrowable
    {
        ThrowableType ThrowType { get; set; }
        float Force { get; set; }
    }
}
