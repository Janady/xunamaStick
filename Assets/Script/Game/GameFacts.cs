using UnityEngine;

public class GameFacts
{
    private int level;
    private int count;
    public GameFacts(int level, int possible)
    {
        this.level = level;
        this.count = possible;
    }

    public int Level
    {
        get
        {
            return level;
        }
    }

    public int Count
    {
        get
        {
            return count;
        }
    }
}