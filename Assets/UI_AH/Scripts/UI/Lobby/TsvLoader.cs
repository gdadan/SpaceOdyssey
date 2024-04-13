using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �о���� ��ũ��Ʈ
public class TsvLoader : MonoBehaviour
{
    string[] rowtext; //��

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

    //���ҽ��ȿ� �ִ� ������ �ҷ����� �Լ�
    public void Create(string file)
    {
        TextAsset data = Resources.Load(file) as TextAsset;

        rowtext = data.text.Split("\n");
    }

    //ù��° ���� ���� �ɰ��� ���ϴ� ���� ���� �������� �Լ�
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

    //Int �� �������� �Լ�
    public int GetInt(int num, string column)
    {
        return int.Parse(GetString(num, column));
    }

    //String �� �������� �Լ�
    public string GetString(int num, string column)
    {
        string[] Text = rowtext[num].Split("\t");

        int _column = GetColumn(column);

        return Text[_column];
    }
}
