using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class TileSetter : MonoBehaviour
{
    public CsvReader csvReader;
    public GameObject[] wallChanger;
    public GameObject floor;
    public GameObject Goal;
    public GameObject Parent;
    public GameObject Player;
    int[,] map;
    int xLength, yLength;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!TryGetComponent<CsvReader>(out csvReader))
        {
            Debug.LogError("Error:CsvReader is NULL.");//マップが作れなければ即終了
            Application.Quit();
        }
        SetTile(1);
        PlayerSpawn();
    }


    void SetTile(int floorNum) //Sceneを変えた時に呼び出される
    {
        List<string[]> mapStr = csvReader.ReadCsv("Map" + floorNum.ToString());
        xLength = mapStr[0].Length;    //X方向のマップ要素数(可変)
        yLength = mapStr.Count;        //Y方向のマップ要素数(可変)
        map = ToIntMap(mapStr);
        Data.TopLeft = new Vector2(-xLength / 2, yLength / 2); //左上を決める　マップが真ん中に来るようにする
        for(int y = 0; y < yLength; ++y)
        {
            for(int x = 0; x < xLength; ++x)
            {
                Vector2 pos = Data.TopLeft + new Vector2(x, -y);
                switch (map[y, x])
                {
                    case 0: //床
                        CreateFloor(pos);
                        break;
                    case 1: //壁
                        CreateFloor(pos);
                        int wallNum = ChangeWall(x, y);
                        CreateGO(wallChanger[wallNum], pos);
                        break;
                    case 2: //プレイヤーのスポーン地点
                        CreateFloor(pos);
                        Data.playerSpawnPos = pos;
                        break;
                    case 3: //ゴール(階段)
                        CreateFloor(pos);
                        CreateGO(Goal, pos);
                        break;
                    case -1: //-1は空白であるため、何も生成してはならない
                        break;
                    default: //例外処理　今回は実装なし
                        break;
                }
            }
        }
    }
    int[,] ToIntMap(List<string[]> mapStr_) //StringのListからint配列に変換する。比較を多用するため採用
    {
        int[,] ints = new int[20, 20];
        for (int y =  0; y < yLength; ++y)
        {
            for (int x = 0 ;x < xLength; ++x)
            {
                ints[y,x] = int.Parse(mapStr_[y][x]);
            }
            if(xLength < 20) //余りが出た時は-1で埋める
            {
                for(int x = xLength; x < 20; ++x)
                {
                    ints[y, x] = -1;
                }
            }
        }
        if(yLength < 20) //余りは-1で埋める
        {
            for( int y = yLength; y < 20; ++y)
            {
                if (xLength < 20) //余りが出た時は-1で埋める
                {
                    for (int x = xLength; x < 20; ++x)
                    {
                        ints[y, x] = -1;
                    }
                }
            }
        }

        return ints;
    }
    void CreateFloor(Vector2 pos) //指定した位置に床を生成する　床の生成を多用するため採用
    {
        GameObject go = Instantiate(floor) as GameObject;
        go.transform.position = pos;
        go.transform.SetParent(Parent.transform);
    }
    void CreateGO(GameObject go, Vector2 pos) //指定した位置にオブジェクトを生成する
    {
        go = Instantiate(go) as GameObject;
        go.transform.position = pos;
        go.transform.SetParent(Parent.transform);
    }
    int ChangeWall(int x, int y) //壁の番号を決める　Csvの段階では壁は一様に1と表記しているため
    {
        int n = 0;
        // 下に壁があれば+1、右にあれば+2、左にあれば+4
        //2進法の要領
        if (x > 0 && map[y, x - 1] == 1)            n += 4;
        if (x < xLength && map[y, x + 1] == 1)      n += 2;
        if (y < yLength && map[y + 1, x] == 1)      n += 1;
        return n;
    }

    void PlayerSpawn()
    {
        GameObject go = Instantiate(Player) as GameObject;
        go.transform.position = Data.playerSpawnPos;
    }
}
