using Libs.Event;

public class GameLevel
{
    private int total;
    private int current;
    private readonly string[] desc = {
        "通三关, 赢口红", // 第一关
        "干得漂亮, 再接再厉",
        "最后一关, 冲鸭！！"
    };
    public GameLevel(bool trial)
    {
        total = trial ? 2 : 3;
        current = 0;
    }

    public bool pass()
    {
        if (current >= total)
        {
            return true;
        }
        return false;
    }
    public void next()
    {
        current++;
    }
    public int level
    {
        get
        {
            return current;
        }
    }
    public int totalLevel
    {
        get
        {
            return total;
        }
    }

    public override string ToString()
    {
        string ret = "";
        if (!pass())
        {
            ret = "第" + (current + 1) + "关";
        }
        else
        {
            ret = "顺利通关";
        }
        return ret;
    }
    public string description()
    {
        return desc[current];
    }
}