using UnityEngine;

public class PlayerController : BaseChara
{
    Vector2 input;
    protected override void SetUp()
    {
        base.timer = 0;
        base.status.hp = 10;
        base.status.atk = 1;
        transform.position = Data.playerSpawnPos;
        base.lastpos = transform.position;
    }

    protected override void ORUpdate()
    {
        base.isWalking = false;
        input = SaveInput();
        if(input != Vector2.zero && !TouchWall(lastpos, input))
        {
            base.lastpos = transform.position;
            base.nextpos = base.lastpos + input;
            base.isWalking = true;
            base.timer = base.maxTime;
        }
    }
    Vector2 SaveInput()
    {
        Vector2[,] maps = new Vector2[3, 3]
        {
            { -Vector2.right, Vector2.up,   Vector2.up },
            { -Vector2.right, Vector2.zero, Vector2.right },
            { -Vector2.up,   -Vector2.up,   Vector2.right }
        };
        int x = 1, y = 1;
        if ( Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            y -= 1;
        }
        if ( Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            y += 1;
        }
        if ( Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            x -= 1;
        }
        if(Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
        {
            x += 1;
        }

        return maps[y, x];
    }
}
