using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayerX : MonoBehaviour
{
    // This class is just used to keep an obect at the same x position as the player. 
    // It's needed since we can't parent the cam to the child if we want the cam tracking the way it is.

    Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }
}
