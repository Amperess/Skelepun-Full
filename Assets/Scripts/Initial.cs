using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Initial : MonoBehaviour {
    //keeps track of which scene on and last on
    public static int curScene = 0;
    public static int lastScene = 0;
    //last location player was at and should be returned to
    public static Vector3 lastPos = new Vector3(-27, 6, 0);
    //which monster has been triggered
    public static int monsterNum = 0;
    //can the player move? should only be turned off during dialogue or tutorial mode
    public static bool canMove;
    public static bool firstTime = true;
    public static bool winner = true;
    public static bool soul1, soul2, soul3, jellyCute, jellyNo, boss;

    void Start()
    {

    }
    void Update()
    {
        //keeps track of which scene currently on to check buttons
        curScene = SceneManager.GetActiveScene().buildIndex;

        //allows you to quit the game, leave on or there is no escape
        if (Input.GetKey("escape"))
            if (curScene == 4)
                Application.Quit();
            else
                SceneManager.LoadScene(4);

        //when returning from settings, put the player back in the right place. no teleportation allowed
        if (curScene == 3)
        {
            if (Input.GetKey(KeyCode.Backspace))
                SceneManager.LoadScene(lastScene);
        }

        //on the main stage, open controls but keep track of where player was
        if (curScene == 1)
        {
            if (Input.GetKey(KeyCode.C))
            {
                SceneManager.LoadScene(3);
                lastPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            }
        }
    }
}
