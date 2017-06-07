using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    private static int lay = -4;
    GameObject easterEgg;

    // Use this for initialization
    void Start () {
        if (Initial.winner == true)
        {
            GameObject.Find("WinnerBG").GetComponent<Renderer>().sortingOrder = 1;
            GameObject.Find("LoserBG").GetComponent<Renderer>().sortingOrder = 0;
            GameObject.Find("WinnerAudio").GetComponent<AudioSource>().Play();
            
        }
        else
        {
            GameObject.Find("WinnerBG").GetComponent<Renderer>().sortingOrder = 0;
            GameObject.Find("LoserBG").GetComponent<Renderer>().sortingOrder = 1;
            GameObject.Find("LoserAudio").GetComponent<AudioSource>().Play();
        }
	}
	
	// Update is called once per frame
	void Update () {
        GameObject easterEgg = GameObject.FindWithTag("Easter Egg");
        if (Input.GetMouseButtonDown(0))
        {
            lay++;
            easterEgg.GetComponent<Renderer>().sortingOrder = lay;
        }      
    }
}
