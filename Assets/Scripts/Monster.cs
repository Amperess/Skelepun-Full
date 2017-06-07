using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
    //more stats, default but will be set according to the monster
    public static int THP, ATT, cHP;
    public static double crt, def;
    public static bool isDead, justCrit, justBlocked;
    void Start () {
    }
	
	void Update () {
	
	}
    public static void Initialize(int monNum)
    {
        switch(monNum)
        {
            case 1:
                //first monster type
                THP = 40;
                ATT = 10;
                cHP = THP;
                crt = 0.2;
                def = 0.1;
                isDead = false;
                break;
            case 2:
                //minor jelly
                THP = 45;
                ATT = 10;
                cHP = THP;
                crt = 0.2;
                def = 0.1;
                isDead = false;
                break;
            case 3:
                //mini boss
                THP = 20;
                ATT = 5;
                cHP = THP;
                crt = 0.1;
                def = 0.05;
                isDead = false;
                break;
            case 4:
                //major jelly
                THP = 100;
                ATT = 10;
                cHP = THP;
                crt = 0.35;
                def = 0.35;
                isDead = false;
                break;
            default:
                THP = 50;
                ATT = 5;
                cHP = THP;
                crt = 0.1;
                def = 0.05;
                isDead = false;
                break;
        }
    }

    public static int Attack()
    {
        int rand = (int)(Random.value * 99) + 1;
        if (rand < crt * 100)
        {
            justCrit = true;
            return (int)(ATT + (ATT * crt));
        }
        else
        {
            return ATT;
        }
    }

    public static void Damage(int attVal)
    {
        int rand = (int)(Random.value*99)+1;
        if (rand < def * 100)
        {
            cHP -= ((int)attVal / 2);
            if (cHP <= 0)
                isDead = true;
            justBlocked = true;
        }
        else
        {
            cHP -= (attVal);
            if (cHP <= 0)
                isDead = true;
        }
    }
}
