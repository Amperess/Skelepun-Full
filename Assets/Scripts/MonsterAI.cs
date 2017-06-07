using UnityEngine;
using System.Collections;

public class MonsterAI : MonoBehaviour {

	private Vector3 intialPos = new Vector3(4.5f, 0, 0);
    public static Transform pPos, mPos;
    public static GameObject mObj, proj;
    private int attRange = 6;
    private bool attacking;
    public static float dist;
    int scale = 1;
    bool isOnRight;

	void Start () {
        pPos = GameObject.FindGameObjectWithTag("Player").transform;
        switch (Initial.monsterNum)
        {
            case 1:
                mObj = GameObject.FindWithTag("Soul");
                proj = GameObject.Find("Shadow");
                break;
            case 2:
                mObj = GameObject.FindWithTag("Jelly");
                proj = GameObject.Find("Lightning");
                break;
            case 3:
                mObj = GameObject.FindWithTag("MiniBoss");
                proj = GameObject.Find("Lightning");
                break;
            default:
                mObj = GameObject.FindWithTag("MiniBoss");
                proj = GameObject.Find("Lightning");
                break;
        }
        mPos = mObj.GetComponent<Transform>();
    }
    public static void setToBoss()
    {
        pPos = GameObject.FindGameObjectWithTag("Player").transform;
        mObj = GameObject.FindWithTag("Boss");
        proj = GameObject.Find("Smoke");
        mPos = mObj.GetComponent<Transform>();
    }
	
	void Update () {
        if (!Monster.isDead && Initial.monsterNum != 4)
        {
            dist = Vector3.Distance(pPos.position, mPos.position);
            if (dist > attRange)
            {
                if (pPos.position.x > mPos.position.x)
                    mPos.Translate(new Vector3(.03f, 0, 0));
                else if (pPos.position.x < mPos.position.x)
                    mPos.Translate(new Vector3(-.03f, 0, 0));
            }
            else if (dist < attRange - 2)
            {
                if (pPos.position.x < mPos.position.x)
                    mPos.Translate(new Vector3(.03f, 0, 0));
                else if (pPos.position.x > mPos.position.x)
                    mPos.Translate(new Vector3(-.03f, 0, 0));
            }
            else if (!attacking)
            {
                StartCoroutine(Attack());
            }
            mPos.Translate(new Vector3(0, scale * 0.02f, 0));
            if (mPos.position.y >= 0.6 || mPos.position.y <= -0.6)
                scale *= -1;
            if (pPos.position.x > mPos.position.x && !isOnRight)
            {
                mObj.GetComponent<SpriteRenderer>().flipX = !mObj.GetComponent<SpriteRenderer>().flipX;
                isOnRight = true;
            }

            else if (pPos.position.x < mPos.position.x && isOnRight)
            {
                mObj.GetComponent<SpriteRenderer>().flipX = !mObj.GetComponent<SpriteRenderer>().flipX;
                isOnRight = false;
            }
            mPos.position = new Vector3(Mathf.Clamp(mPos.position.x, -7f, 7f),
                mPos.position.y, mPos.position.z);
        } 
    }
    IEnumerator Attack()
    {
        attacking = true;
        proj.transform.position = mPos.position;
        proj.GetComponent<SpriteRenderer>().sortingOrder = 1;
        yield return new WaitForSeconds(.5f);
        proj.transform.position = 
            ((pPos.position - mPos.transform.position) * 0.5f) + mPos.transform.position;
        yield return new WaitForSeconds(.5f);
        proj.transform.position = pPos.position;
        Diane.Damage(Monster.Attack());
        if (Monster.justCrit)
        {
            AudPlayer.playAudio("critAudio");
            Monster.justCrit = false;
            StartCoroutine(BMovement.popUp(true, true));
        }
        else if (Diane.justBlocked)
        {
            StartCoroutine(BMovement.popUp(true, false));
            Diane.justBlocked = false;
        }    
        else
            AudPlayer.playAudio("hitAudio");
        yield return new WaitForSeconds(.25f);
        proj.GetComponent<SpriteRenderer>().sortingOrder = 0;
        yield return new WaitForSeconds(1);
        attacking = false;
    }
}
