using UnityEngine;

public class Data //ƒQ[ƒ€“à‚Å‹¤—L‚·‚é•Ï”‚Ì‚Ü‚Æ‚Ü‚è
{
    public static int floorNum;
    public static Vector2 playerSpawnPos;
    public static Vector2 TopLeft;
    public static float Timefor1Turn = 0.4f;
    public static Status SavePlayerStatus;
}
public class Status
{
    public int hp, atk;
    public Status(int hp, int atk)
    {
        this.hp = hp;
        this.atk = atk;
    }
    public Status()
    {
        this.hp = 0;
        this.atk = 0;
    }
}
