using UnityEngine;

public class BaseChara : MonoBehaviour
{
    protected float timer, maxTime;
    protected Status status = new(); //¡‚Ì‚Æ‚±‚ëg‚¤—\’è‚È‚µ
    protected Vector2 lastpos, nextpos;
    protected bool isWalking;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxTime = Data.Timefor1Turn;
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0) //timer‚É’l‚ª‘ã“ü‚³‚ê‚Ä‚¢‚é‚Æs“®‚Å‚«‚È‚¢
        {
            timer -= Time.deltaTime;
            if(isWalking)
            {
                float per = Mathf.Clamp01(1 - timer / maxTime);
                transform.position = Vector2.Lerp(lastpos, nextpos, per);
            }
        }
        else
        {
            ORUpdate();
        }
    }
    protected virtual void SetUp() { }
    protected virtual void ORUpdate() { }
    protected bool TouchWall(Vector2 pos, Vector2 dir)
    {
        Vector2 startpoint = pos;
        float dist = 1.0f;
        int targetlayer = 1 << 7;
        RaycastHit2D hit = Physics2D.Raycast(startpoint, dir, dist);
        Debug.Log(hit.collider.tag);
        if (hit.collider == null)
        {
            return false;
        }
        else
        {
            return hit.collider.gameObject.tag == "Wall";
        }
    }
    protected GameObject CatchGOfor(Vector2 pos, Vector2 dir, float dist = 1.0f)
    {
        Vector2 startpoint = pos;
        RaycastHit2D hit = Physics2D.Raycast(startpoint, dir, dist);
        return hit.collider.gameObject;
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
}
