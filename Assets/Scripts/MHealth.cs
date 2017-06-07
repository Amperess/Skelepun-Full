using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MHealth : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {
        GetComponent<Image>().fillAmount = (float)Monster.cHP / Monster.THP;
    }
}
