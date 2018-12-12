using Libs.Event;

public class GameLevel
{
    private int total;
    private int current;
    public GameLevel(bool trial)
    {
        total = trial ? 2 : 3;
        current = 0;
    }

    public int next()
    {
        current++;
        if (current > total)
        {
            return 0;
        }
        return current;
    }
    public int level()
    {
        return current;
    }

    public override string ToString()
    {
        string ret = "";
        if (current <= total)
        {
            ret = "第" + current + "关";
        }
        else
        {
            ret = "顺利通关";
        }
        return ret;
    }
}