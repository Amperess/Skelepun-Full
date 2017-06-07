using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{ 
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (transform.parent.gameObject.tag.Equals("Soul1"))
            {
                Initial.monsterNum = 1;
                Initial.soul1 = true;
                Debug.Log("Triggered by Soul1");
            }
            else if (transform.parent.gameObject.tag.Equals("Soul2"))
            {
                Initial.monsterNum = 1;
                Initial.soul2 = true;
                Debug.Log("Triggered by Soul2");
            }
            else if (transform.parent.gameObject.tag.Equals("Soul3"))
            {
                Initial.monsterNum = 1;
                Initial.soul3 = true;
                Debug.Log("Triggered by Soul3");
            }
            else if (transform.parent.gameObject.tag.Equals("Jelly"))
            {
                Initial.monsterNum = 2;
                Initial.jellyNo = true;
                Debug.Log("Triggered by Jelly");
            }
            else if (transform.parent.gameObject.tag.Equals("MiniBoss"))
            {
                Initial.monsterNum = 3;
                Initial.jellyCute = true;
                Initial.boss = true;
                Debug.Log("Triggered by MiniBoss");
            }
            Initial.lastPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            SceneManager.LoadScene(2);
        }
    }
}
