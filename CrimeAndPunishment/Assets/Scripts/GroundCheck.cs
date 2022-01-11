using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = transform.parent.gameObject.GetComponent<PlayerMovement>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        playerMovement.grounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerMovement.grounded = false;
    }
}
