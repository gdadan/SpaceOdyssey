using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletsManager : MonoBehaviour
{
    public static BulletsManager instance;

    public TMP_Text synergysText;

    public List<Bullet> bullet1 = new List<Bullet>(); //weapon1
    public List<Bullet> bullet2 = new List<Bullet>(); //weapon2
    public List<Bullet> bullet3 = new List<Bullet>(); //weapon3
    public List<Engine> engine1 = new List<Engine>(); //weapon4
    public List<Engine> engine2 = new List<Engine>(); //weapon5 

    public int?[] bulletIndexs = new int?[5]; // int?[] -> 배열에 null값 포함.

    public Dictionary<string, int> synergys;

    int index;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        index = InventoryManager.instance.equipSlot.key;

        for (int i = 0; i < bulletIndexs.Length; i++) // 전부 null로 초기화
        {
            bulletIndexs[i] = null;
        }

        Calculateinstance(index);
        CalculateSynergy();
        SynergyManager.instance.Synergy();
    }

    public void Calculateinstance(int index)
    {
        if (index < 5)
        {
            int B_Index = index - 1;
            bulletIndexs[0] = B_Index;
        }

        else if (index < 10)
        {
            int B_Index = index - 5;
            bulletIndexs[1] = B_Index;
        }

        else if (index < 15)
        {
            int B_Index = index - 10;
            bulletIndexs[2] = B_Index;
        }

        else if (index < 17)
        {
            int B_Index = index - 15;
            bulletIndexs[3] = B_Index;
        }

        else if (index < 18)
        {
            int B_index = index - 17;
            bulletIndexs[4] = B_index;
        }
    }


    public void CalculateSynergy() //시너지 계산
    {
        synergys = new Dictionary<string, int>();

        for (int i = 0; i < bulletIndexs.Length; i++)
        {
            if (bulletIndexs[i].HasValue)
            {
                switch (i)
                {
                    case 0:
                        CheckBulletSynergy(bullet1[(int)bulletIndexs[i]]);
                        break;
                    case 1:
                        CheckBulletSynergy(bullet2[(int)bulletIndexs[i]]);
                        break;
                    case 2:
                        CheckBulletSynergy(bullet3[(int)bulletIndexs[i]]);
                        break;
                    case 3:
                        CheckEngineSynergy(engine1[(int)bulletIndexs[i]]);
                        break;
                    case 4:
                        CheckEngineSynergy(engine2[(int)bulletIndexs[i]]);
                        break;

                }
            }
        }

        string ss = "";

        foreach (string s in synergys.Keys)
        {
            ss = string.Format(ss + s + " {0}\n", synergys[s]);
        }

        synergysText.text = ss;

    }

    public void CheckBulletSynergy(Bullet bullet) // 시너지 결과값 보여주기
    {
        //시너지 1
        if (synergys.ContainsKey(bullet.synergy1))
        {
            int cCount = 0;
            synergys.TryGetValue(bullet.synergy1, out cCount);

            cCount++;

            synergys[bullet.synergy1] = cCount;
        }

        else
        {
            synergys.Add(bullet.synergy1, 1);
        }

        //시너지2
        if (synergys.ContainsKey(bullet.synergy2))
        {
            int cCount = 0;
            synergys.TryGetValue(bullet.synergy2, out cCount);

            cCount++;

            synergys[bullet.synergy2] = cCount;
        }

        else
        {
            synergys.Add(bullet.synergy2, 1);
        }


        //시너지3
        if (synergys.ContainsKey(bullet.synergy3))
        {
            int cCount = 0;
            synergys.TryGetValue(bullet.synergy3, out cCount);

            cCount++;

            synergys[bullet.synergy3] = cCount;
        }

        else
        {
            synergys.Add(bullet.synergy3, 1);
        }

        synergys.Remove("");
    }

    void CheckEngineSynergy(Engine engine)
    {
        //시너지1
        if (synergys.ContainsKey(engine.synergy1))
        {
            int cCount = 0;
            synergys.TryGetValue(engine.synergy1, out cCount);

            cCount++;

            synergys[engine.synergy1] = cCount;
        }

        else
        {
            synergys.Add(engine.synergy1, 1);
        }

        // 시너지2
        if (synergys.ContainsKey(engine.synergy2))
        {
            int cCount = 0;
            synergys.TryGetValue(engine.synergy2, out cCount);

            cCount++;

            synergys[engine.synergy2] = cCount;
        }

        else
        {
            synergys.Add(engine.synergy2, 1);
        }

        //시너지3
        if (synergys.ContainsKey(engine.synergy3))
        {
            int cCount = 0;
            synergys.TryGetValue(engine.synergy3, out cCount);

            cCount++;

            synergys[engine.synergy3] = cCount;
        }

        else
        {
            synergys.Add(engine.synergy3, 1);
        }

        synergys.Remove("");
    }

    public void ChangeBullet1(int bulletIndex)
    {
        bulletIndexs[0] = bulletIndex;
        CalculateSynergy();
    }

    public void ChangeBullet2(int bulletIndex)
    {
        bulletIndexs[1] = bulletIndex;
        CalculateSynergy();
    }

    public void ChangeBullet3(int bulletIndex)
    {
        bulletIndexs[2] = bulletIndex;
        CalculateSynergy();
    }

    public void ChangeEngine1(int bulletIndex)
    {
        bulletIndexs[3] = bulletIndex;
        CalculateSynergy();
    }

    public void ChangeEngine2(int bulletIndex)
    {
        bulletIndexs[4] = bulletIndex;
        CalculateSynergy();
    }
}
