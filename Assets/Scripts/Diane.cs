using UnityEngine;
using System.Collections;

public class Diane : MonoBehaviour {
    //keeps track of stats throughout battles
    public static readonly int THP = 100, ATT = 5;
    public static readonly double CRT = 0.2, DEF = 0.2;
    public static int cHP = 100;
    public static bool isDead, justCrit, justBlocked;
	void Start () {
	
	}
	
	void Update () {
	
	}

    public static int Attack()
    {
        int rand = (int)(Random.value * 99) + 1;
        if (rand < CRT * 100)
        {
            justCrit = true;
            return (int)(ATT + (ATT * CRT));
            
        }
        else
        {
            return ATT;
        }
    }

    public static void Damage(int attVal)
    {
        int rand = (int)(Random.value * 99) + 1;
        if (rand < DEF * 100)
        {
            cHP -= ((int)attVal / 2);
            justBlocked = true;
            if (cHP <= 0)
                isDead = true;
        }
        else
        {
            cHP -= (attVal);
            if (cHP <= 0)
                isDead = true;
        }
    }
}
