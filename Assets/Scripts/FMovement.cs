using UnityEngine;
using System.Collections;


public class FMovement : MonoBehaviour
{
    //sets the x and y velocities
    private float vx = 10.0f;
    private float vy = 250.0f;
    private float MIN_V = -10.0f;
    private float MAX_V = 10.0f;
    //amount of jumps allowed, game is a no fly zone
    private int jump = 2;
    //keeps height of sprite for proper raycast length
    private float distToGround;
    //is the charcter crouching
    bool isCrouching = false;
    //direct access to the animator
    Animator anim;
    //colliders to be affected by crouch, rigid to be force affected
    public BoxCollider2D b;
    public BoxCollider2D bCr;
    public Rigidbody2D rgb;
    public CircleCollider2D c;

    void Start()
    {
        if (Initial.soul1)
        {
            Destroy(GameObject.FindWithTag("Soul1"));
            Debug.Log("Destroyed it soul1");
        }
        if (Initial.soul2)
        {
            Destroy(GameObject.FindWithTag("Soul2"));
            Debug.Log("Destroyed it soul2");
        }
        if (Initial.soul3)
        {
            Destroy(GameObject.FindWithTag("Soul3"));
            Debug.Log("Destroyed it Soul 3");
        }
        if (Initial.jellyCute)
        {
            Destroy(GameObject.FindWithTag("MiniBoss"));
            Debug.Log("Destroyed it Miniboss");
        }
        if (Initial.jellyNo)
        {
            Destroy(GameObject.FindWithTag("Jelly"));
            Debug.Log("Destroyed it Jelly");
        }
        if (Initial.boss)
        {
            Destroy(GameObject.FindWithTag("Boss"));
            Debug.Log("Destroyed it Boss");
        }

        //put player back where they are supposed to be
        transform.position = Initial.lastPos;
        //set this as the last scene accessed
        Initial.lastScene = 1;
        //set standing colliders
        b.enabled = true;
        bCr.enabled = false;
        //set distance to the ground inside the sprite
        distToGround = (b.bounds.extents.y+c.bounds.extents.y);
        //gets the animator componenet
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //can the player move? then go for it
        if (Initial.canMove)
        {
            anim.SetBool("isCrouch", isCrouching);
            //always check if they are on the ground and reset jumps
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distToGround - 0.1f);

            //restrict to min and max velocities
            if (GetComponent<Rigidbody2D>().velocity.x > MAX_V)
                GetComponent<Rigidbody2D>().velocity = new Vector2(MAX_V, GetComponent<Rigidbody2D>().velocity.y);

            if (GetComponent<Rigidbody2D>().velocity.x < MIN_V)
                GetComponent<Rigidbody2D>().velocity = new Vector2(MIN_V, GetComponent<Rigidbody2D>().velocity.y);

            anim.SetBool("isRunning", false);
            //if player is on the ground, reset jumps to max
            if (hit.collider != null && jump == 0 && hit.transform.tag == "Background")
            {
                jump = 2;
            }
            if (hit.collider != null && hit.transform.tag == "Background" && anim.GetBool("isInAir"))
            {
                anim.SetBool("isInAir", false);
            }
            //get key input and move player accordingly
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rgb.AddForce(Vector2.left * vx);
                anim.SetBool("isFacingLeft", true);
                anim.SetBool("isRunning", true);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rgb.AddForce(Vector2.right * vx);
                anim.SetBool("isFacingLeft", false);
                anim.SetBool("isRunning", true);
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                if (!isCrouching)
                {
                    if (jump > 0)
                    {
                        anim.SetBool("isInAir", true);
                        rgb.AddForce(Vector2.up * vy);
                        jump--;
                    }
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                isCrouching = true;
                b.enabled = false;
                bCr.enabled = true;
                //play crounch animation
            }
            //if the character is crounching under something, avoid accidental decapitation
            if (isCrouching && !Input.GetKey(KeyCode.DownArrow))
            {
                RaycastHit2D canStandHit = Physics2D.Raycast(transform.position, Vector2.up, distToGround / 2);
                if (canStandHit.collider == null)
                {
                    isCrouching = false;
                    b.enabled = true;
                    bCr.enabled = false;
                }
            }

            //no rotating until player falls over
            if (transform.rotation.eulerAngles.z != 0)
            {
                transform.rotation = Quaternion.identity;
            }
        }
    }
}

