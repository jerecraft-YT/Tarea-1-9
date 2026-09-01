public class BaseStats
{
    private int live;
    private int maxLive;

    public BaseStats(int liveAndMaxLive)
    {
        live = liveAndMaxLive;
        maxLive = liveAndMaxLive;
    }

    public BaseStats(int live,int maxLive)
    {
        this.live = live;
        this.maxLive = maxLive;
    }

    public void TakeDamage(int damage)
    {
        int expectedLive = live - damage;

        live = expectedLive <= 0 ? 0 : expectedLive;
    }

    public void TakeHeal(int heal)
    {
        int expectedLive = live + heal;

        live = expectedLive >= maxLive ? maxLive : expectedLive;
    }

    public int MaxLive
    {
        get { 
            return maxLive;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (live < value)
            {
                live = value;
            }
            maxLive = value;
        }
    }
   
    public int Live => live;
}
