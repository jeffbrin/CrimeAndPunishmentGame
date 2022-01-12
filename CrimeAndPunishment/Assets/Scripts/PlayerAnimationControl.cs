using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    Animator anim;
    PlayerMovement playerMovement;
    public bool falling = false;
    public bool jumping = false;
    public Vector2 boxColliderIdleScale;
    public Vector2 boxColliderMovingScale;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // Correct for the change in height of the running vs idle animations
        GetComponent<BoxCollider2D>().size = boxColliderMovingScale;

        if (playerMovement.grounded)
        {
            if (Input.GetKey(playerMovement.rightKey) || Input.GetKey(playerMovement.leftKey))
            {
                if (Input.GetKey(playerMovement.sprintKey))
                {
                    anim.Play("PlayerRun");
                    Debug.Log("Run");
                }
                else
                {
                    anim.Play("PlayerWalk");
                    Debug.Log("Walk");
                }
            }
            else
            {
                anim.Play("PlayerIdle");
                Debug.Log("Idle");
                // Correct for the change in height of the running vs idle animations
                GetComponent<BoxCollider2D>().size = boxColliderIdleScale;
            }
        }
        if (!playerMovement.grounded && GetComponent<Rigidbody2D>().velocity.y < 0 && !falling)
        {
            anim.Play("PlayerFall");
            Debug.Log("Fall");
            falling = true;
        }

        if (playerMovement.grounded && falling)
        {
            falling = false;
        }
    }

    public void Jump()
    {
        anim.gameObject.SetActive(false);
        anim.gameObject.SetActive(true);
        anim.Play("PlayerJump");
        Debug.Log("Jump");
    }
}
