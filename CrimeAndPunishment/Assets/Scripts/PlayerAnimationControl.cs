using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    Animator anim;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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
        }
    }
}
