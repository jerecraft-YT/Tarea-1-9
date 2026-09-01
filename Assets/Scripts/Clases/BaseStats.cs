public class BaseStats
{
    private int maxLive;

    public BaseStats(int maxLive)
    {
        this.maxLive = maxLive;
    }

    public int MaxLive
    {
        get { 
            return maxLive;
        }
        set
        {
            maxLive = value < 0 ? 0 :value;
        }
    }
}
