using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//데이터 읽어오는 스크립트
public class TsvLoader : MonoBehaviour
{
    string[] rowtext; //행

    public static TsvLoader instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Create("EquipmentItem");
    }

    //리소스안에 있는 파일을 불러오는 함수
    public void Create(string file)
    {
        TextAsset data = Resources.Load(file) as TextAsset;

        rowtext = data.text.Split("\n");
    }

    //첫번째 행을 열로 쪼개서 원하는 열의 값을 가져오는 함수
    int GetColumn(string column)
    {
        string str = rowtext[0].Remove(rowtext[0].Length - 1);
        string[] title = str.Split("\t");

        for (int i = 0; i < title.Length; i++)
        {
            if (title[i] == column)
            {
                return i;
            }
        }
        return -1;
    }

    //Int 값 가져오는 함수
    public int GetInt(int num, string column)
    {
        return int.Parse(GetString(num, column));
    }

    //String 값 가져오는 함수
    public string GetString(int num, string column)
    {
        string[] Text = rowtext[num].Split("\t");

        int _column = GetColumn(column);

        return Text[_column];
    }
}
