using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {

    GameObject phoneAud, DRunner;
    SpriteRenderer[] dia = new SpriteRenderer[9];
    bool isGoing = true;
    bool skip;

	void Start () {
        if (Initial.firstTime)
        {
            phoneAud = GameObject.Find("RingAudio");
            phoneAud.GetComponent<AudioSource>().Play();
            DRunner = GameObject.Find("Dialogue");
            int count = 0;
            foreach (Transform child in DRunner.transform)
            {
                dia[count] = child.GetComponent<SpriteRenderer>();
                count++;
            }
            StartCoroutine(GoDia());
            Initial.firstTime = false;
        }
    }
    IEnumerator GoDia()
    {
        foreach (SpriteRenderer sr in dia)
        {
            if (skip)
            {
                isGoing = false;
                yield break;
            }
            sr.sortingOrder = 4;
            yield return new WaitForSeconds(5);
            sr.sortingOrder = -1;
            if (sr.name == "DD5")
            isGoing = false;
        }
    }

    void Update () {
        if (!Initial.canMove)
            if (!isGoing)
                Initial.canMove = true;
        if (Input.GetKeyUp(KeyCode.Space))
            skip = true;
    }
}
