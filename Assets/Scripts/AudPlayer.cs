using UnityEngine;
using System.Collections;

public class AudPlayer : MonoBehaviour {

    GameObject audPlayer;
    static AudioSource miniAud, soulAud, bossAud, hitAud, critAud, stabAud;

	void Start () {
        audPlayer = GameObject.Find("AudioPlayer");
        foreach (Transform child in audPlayer.transform)
        {
            if (child.name == "miniAudio")
                miniAud = child.GetComponent<AudioSource>();
            else if (child.name == "soulAudio")
                soulAud = child.GetComponent<AudioSource>();
            else if (child.name == "bossAudio")
                bossAud = child.GetComponent<AudioSource>();
            else if (child.name == "critAudio")
                critAud = child.GetComponent<AudioSource>();
            else if (child.name == "hitAudio")
                hitAud = child.GetComponent<AudioSource>();
            else if (child.name == "stabAudio")
                stabAud = child.GetComponent<AudioSource>();
        }
            Debug.Log(soulAud);
    }
	
	void Update () {

	}
    public static void playAudio(string name)
    {
        if (name == "miniAudio")
            miniAud.Play();
        else if (name == "soulAudio")
            soulAud.Play();
        else if (name == "bossAudio")
            bossAud.Play();
        else if (name == "critAudio")
            critAud.Play();
        else if (name == "hitAudio")
            hitAud.Play();
        else if (name == "stabAudio")
            stabAud.Play();
        else
            Debug.Log("That's not a valid sound file dude.");
    }
}
