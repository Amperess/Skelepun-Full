using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PHealth : MonoBehaviour {
    void Start()
    {

    }
    void Update()
    {
        GetComponent<Image>().fillAmount = (float)Diane.cHP/Diane.THP;
    }
}
