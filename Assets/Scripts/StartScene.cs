using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {
    //does what it says, loads each respective menu duh
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadStage1 ()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadBattle()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadSettings()
    {
        SceneManager.LoadScene(3);
    }
    
}
