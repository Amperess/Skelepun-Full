using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Battle : MonoBehaviour {

    public static GameObject bossObj, monObj, pObj, smoke, skele;
    //Transform target;
    Vector3 initialPTrans;
    bool bossRise, smokeGo, skeleRise, skeleLunge;

    void Start()
    {
        Monster.Initialize(Initial.monsterNum);
        bossObj = GameObject.FindWithTag("Boss");
        pObj = GameObject.FindWithTag("Player");
        smoke = GameObject.Find("Smoke");
        skele = GameObject.Find("Skeleton");
        initialPTrans = pObj.transform.position;
        switch (Initial.monsterNum)
        {
            case 1:
                monObj = GameObject.FindWithTag("Soul");
                AudPlayer.playAudio("soulAudio");
                break;
            case 2:
                monObj = GameObject.FindWithTag("Jelly");
                AudPlayer.playAudio("soulAudio");
                break;
            case 3:
                monObj = GameObject.FindWithTag("MiniBoss");
                AudPlayer.playAudio("miniAudio");
                bossObj.GetComponent<Renderer>().sortingOrder = 4;
                break;
            default:
                Debug.Log("This is running the default");
                monObj = GameObject.FindWithTag("MiniBoss");
                AudPlayer.playAudio("miniAudio");
                bossObj.GetComponent<Renderer>().sortingOrder = 4;
                break;
        }
        monObj.GetComponent<Renderer>().sortingOrder = 2;
    }
    void Update()
    {
        if (Monster.isDead && Initial.monsterNum == 3)
        {
            StartCoroutine(KingEnter());
        } 
        else if(Monster.isDead)
        {
            SceneManager.LoadScene(1);
        }
        if (Diane.isDead)
        {
            Initial.winner = false;
            SceneManager.LoadScene(4);
        }
        if(bossRise)
        {
            float speed = 3;
            float step = speed * Time.deltaTime;
            bossObj.transform.position = Vector3.MoveTowards(bossObj.transform.position, 
                new Vector3(6, 0, 0), step);
            bossRise = false;
        }
        else if(smokeGo)
        {
            smoke.GetComponent<SpriteRenderer>().sortingOrder = 3;
            float speed = 4;
            float step = speed * Time.deltaTime;
            smoke.transform.position = Vector3.MoveTowards(smoke.transform.position,
                initialPTrans, step);
            smokeGo = false;
        }
        else if (skeleRise)
        {
            float speed = 6;
            float step = speed * Time.deltaTime;
            skele.transform.position = Vector3.MoveTowards(skele.transform.position,
                new Vector3(0, -2, 0), step);
            skeleRise = false;
        }
        else if(skeleLunge)
        {
            float speed = 6;
            float step = speed * Time.deltaTime;
            skele.transform.position = Vector3.MoveTowards(skele.transform.position,
                new Vector3(1.6f, -2, 0), step);
            skeleLunge = false;
        }
    }

    IEnumerator KingEnter()
    {
        MonsterAI.proj.GetComponent<SpriteRenderer>().sortingOrder = 0;
        BMovement.canMove = false;
        monObj.GetComponent<Renderer>().sortingOrder = 0;
        yield return new WaitForSeconds(1);
        monObj.GetComponent<Renderer>().sortingOrder = 2;
        yield return new WaitForSeconds(1);
        monObj.GetComponent<Renderer>().sortingOrder = 0;
        yield return new WaitForSeconds(1);
        monObj.GetComponent<Renderer>().sortingOrder = 2;
        yield return new WaitForSeconds(1);
        monObj.GetComponent<Renderer>().sortingOrder = 0;
        Initial.monsterNum = 4;
        Monster.Initialize(Initial.monsterNum);
        pObj.transform.position = initialPTrans;
        if (pObj.transform.localScale.x < 0)
            pObj.transform.localScale = new Vector3(pObj.transform.localScale.x * -1,
                pObj.transform.localScale.y, pObj.transform.localScale.z);
        bossRise = true;
        yield return new WaitForSeconds(5);
        smokeGo = true;
        AudPlayer.playAudio("critAudio");
        yield return new WaitForSeconds(4);
        smoke.GetComponent<SpriteRenderer>().sortingOrder = 0;
        Diane.cHP = 5;
        GameObject.Find("Staff").GetComponent<Animator>().Play("Super");
        yield return new WaitForSeconds(1);
        GameObject.Find("Skull").GetComponent<SpriteRenderer>().sortingOrder = 0;
        GameObject.Find("Skeleton").transform.position = new Vector3(0, -6.85f, 0);
        skeleRise = true;
        yield return new WaitForSeconds(1);
        skeleLunge = true;
        AudPlayer.playAudio("critAudio");
        yield return new WaitForSeconds(3);
        Monster.cHP = 0;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(4);
    }
}
