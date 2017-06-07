using UnityEngine;
using System.Collections;

public class BMovement : MonoBehaviour {
    //sets the x velocity
    private float vx = 10.0f;
    private float MIN_V = -10.0f;
    private float MAX_V = 10.0f;
    //rigid to be force affected
    Rigidbody2D rgb;
    //direct access to staff sprite
    public static GameObject staff, critPop, blockPop;
    public static bool isFacingLeft = false;
    public static Transform sPos;
    public static Transform mPos;
    public static Transform pPos;
    public static bool canMove = true;

    void Start () {
        //find staff sprite renderer
        staff = GameObject.Find("Staff");
        rgb = GetComponent<Rigidbody2D>();
        pPos = GameObject.FindGameObjectWithTag("Player").transform;
        sPos = GameObject.FindGameObjectWithTag("Staff").transform;
        critPop = GameObject.Find("Crit");
        blockPop = GameObject.Find("Block");
        switch (Initial.monsterNum)
        {
            case 1:
                mPos = GameObject.FindWithTag("Soul").GetComponent<Transform>();
                break;
            case 2:
                mPos = GameObject.FindWithTag("Jelly").GetComponent<Transform>();
                break;
            case 3:
                mPos = GameObject.FindWithTag("MiniBoss").GetComponent<Transform>();
                break;
            default:
                mPos = GameObject.FindWithTag("MiniBoss").GetComponent<Transform>();
                break;
        }
    }
	
	void Update () {
        if (canMove)
        {
            //restrict to min and max velocities
            if (GetComponent<Rigidbody2D>().velocity.x > MAX_V)
                GetComponent<Rigidbody2D>().velocity = new Vector2(MAX_V, GetComponent<Rigidbody2D>().velocity.y);

            if (GetComponent<Rigidbody2D>().velocity.x < MIN_V)
                GetComponent<Rigidbody2D>().velocity = new Vector2(MIN_V, GetComponent<Rigidbody2D>().velocity.y);
            //if player is on the ground, reset jumps to max

            //get key input and move player accordingly
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //keeps the player from constantly flipping depending on the dir facing
                if (!isFacingLeft)
                    //flips local scale to mirror the sprite
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                    transform.localScale.z);
                //sets the sprite direction correctly
                isFacingLeft = true;
                //adds force to move character in that direction
                rgb.AddForce(Vector2.left * vx);
            }
            //same as above
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (isFacingLeft)
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,
                        transform.localScale.z);
                isFacingLeft = false;
                rgb.AddForce(Vector2.right * vx);
            }
            //plays the attack animation
            if (Input.GetKeyUp(KeyCode.Space))
            {
                staff.GetComponent<Animator>().Play("WhackR");
                if (Vector3.Distance(sPos.position, mPos.position) < 2.3
                    && Vector3.Distance(sPos.position, mPos.position) > 1.3)
                {
                    Monster.Damage(Diane.Attack());
                    if (Diane.justCrit)
                    {
                        AudPlayer.playAudio("critAudio");
                        Diane.justCrit = false;
                        StartCoroutine(popUp(false, true));
                    }
                    else if (Monster.justBlocked)
                    {
                        Monster.justBlocked = false;
                        StartCoroutine(popUp(false, false));
                    }
                    else
                        AudPlayer.playAudio("hitAudio");

                }
            }
        }
    }
    public static IEnumerator popUp(bool onPlayer, bool crit)
    {
        if(onPlayer)
        {
            if(crit)
            {
                critPop.transform.position = pPos.position;
                critPop.GetComponent<SpriteRenderer>().sortingOrder = 1;
                yield return new WaitForSeconds(.5f);
                critPop.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
            else
            {
                blockPop.transform.position = pPos.position;
                blockPop.GetComponent<SpriteRenderer>().sortingOrder = 1;
                yield return new WaitForSeconds(.5f);
                blockPop.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
            
        }
        else
        {
            if (crit)
            {
                critPop.transform.position = mPos.position;
                critPop.GetComponent<SpriteRenderer>().sortingOrder = 1;
                yield return new WaitForSeconds(.5f);
                critPop.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
            else
            {
                blockPop.transform.position = mPos.position;
                blockPop.GetComponent<SpriteRenderer>().sortingOrder = 1;
                yield return new WaitForSeconds(.5f);
                blockPop.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
        }
        
    }
}
