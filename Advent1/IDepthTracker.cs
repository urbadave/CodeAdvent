namespace CodeAdvent.Advent1
{
    public interface IDepthTracker
    {
        string FilePath { get; set; }

        int CountDepthIncrease();
        int CountWindowIncrease(int size);
    }
}