using JetBrains.Annotations;
using NUnit.Framework;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class CsvReader : MonoBehaviour
{
    TextAsset read;//読まれるファイルを管理　ファイルは全て Asset/Resources/ に保管すること
    List<string[]> list = new();

    //2次元配列のListを投げる関数
    public List<string[]> ReadCsv(string file)
    {
        read = Resources.Load<TextAsset>(file) as TextAsset;
        StringReader reader = new StringReader(read.text);
        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine().ToString();
            list.Add(line.Split(','));
        }
        reader.Close();
        return list;
    }
}
