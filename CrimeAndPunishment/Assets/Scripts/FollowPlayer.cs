using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //wip script, pls ignore <3
    public GameObject player;               // reference to the player

    private Rigidbody2D playerRb;           // The player's rigidbody
    private Camera camera;                  // Keeping this here because we can probably normalize the movement stuff according to some camera values

    public float camLead;                   // How far ahead of the player to go on the x axis
    public float yAxisBuffer;               // How far around the screen the player can move on the y axis without affecting the cam position
    public float yAxisParentToPlayerBuffer; // Limit to add to the yAxisBuffer to manage the parenting of the camera


    float targetPositionX;                  // The target position of the cam on the x axis
    public float camSpeed;                  // Movement speed of the camera
    Vector2 positionToAdd;                  // Used for y axis and x axis movement

    float normalizedYVelocityWhenParented;  // Used to parent and unparent the cam

    float camXEase = 0;
    float velXChangeTime = 0;
    public float timeToMaxCamSpeed;
    public float percentOfTimeToMaxSpeed;
    float prevXDirection = 0;
    [Range(0,5)]
    public float heightAbovePlayer = 2;


    void Start()
    {
        camera = this.GetComponent<Camera>();   //reference this object's camera component
        player = FindObjectOfType<PlayerMovement>().gameObject; // Get the player's gameObject
        this.transform.parent = player.transform;   // Parent this object to the player briefly to set default position stuff
        this.transform.localPosition = new Vector3(0, .4f); // Set the default position
        this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, -10);  // Make sure the z axis is correct
        this.transform.parent = FindObjectOfType<TrackPlayerX>().transform; // Set the parent for the cam to the object that tracks the player's x position
        playerRb = player.GetComponent<Rigidbody2D>();  // get the player's rigidbody
    }

    void LateUpdate()
    {
        // This was here from trevor, I don't wanna delete it in case my method sucks and we end up needing this
        //Vector2 posInScreenSpace = camera.WorldToViewportPoint(player.transform.position);
        //Debug.Log(posInScreenSpace);
        //float playerYScreenSpaceCorrected = posInScreenSpace.y - 0.5f;
        //float rangeY = 0.2f;

        // X
        TrackX();

        // Y
        TrackY();

    }

    // Moves the camera a certain distance in front of the player depending on the direction it's moving
    private void TrackX()
    {

        // Store the current x direction as 1 or -1
        float currentXDirection = Mathf.Abs(playerRb.velocity.x) / playerRb.velocity.x;
        // set the current velocity to 0 if it's 0 since the above formula will give NaN
        if (playerRb.velocity.x == 0)
        {
            currentXDirection = 0;
        }

        // If the player changes direction
        if (prevXDirection != currentXDirection && currentXDirection != 0)
        {
            velXChangeTime = Time.time;
            prevXDirection = currentXDirection;
        }

        // If the player is standing still
        if (playerRb.velocity.x == 0)
        {
            prevXDirection = 0;
        }

        // If the player starts moving again
        if (prevXDirection == 0 && currentXDirection != 0)
        {
            velXChangeTime = Time.time;
            prevXDirection = currentXDirection;
        }



        float timeSinceXChange = Time.time - velXChangeTime;
        percentOfTimeToMaxSpeed = timeSinceXChange / timeToMaxCamSpeed;
        float dirVector = playerRb.velocity.x / player.GetComponent<PlayerMovement>().MaxSpeed;     // Find the sign of the velocity

        positionToAdd = new Vector2(dirVector * camLead, 0);                                        // Find the vector 2 to add to the current position
        targetPositionX = player.transform.position.x + positionToAdd.x;                            // Store the position to move to on the x axis

        // Clamp the percent of time to max speed at 100%
        if (percentOfTimeToMaxSpeed > 1)
        {
            percentOfTimeToMaxSpeed = 1;
        }

        // Check if the camera is already where it should be
        if (targetPositionX != this.transform.position.x)
        {
            this.transform.position += new Vector3(percentOfTimeToMaxSpeed * (targetPositionX - this.transform.position.x) * Time.deltaTime * camSpeed, 0);   // Move towards the target position
        }
    }

    // Track the player along after the y axis. 
    // Makes the player the parent of the camera's parent if the player gets too far away from the camera.
    // If the player is in range of the camera it gradually moves towards it.
    private void TrackY()
    {
        // The distance the camera will keep from the player
        float upperLimit = player.transform.position.y + yAxisBuffer;
        float lowerLimit = player.transform.position.y - yAxisBuffer;
        // The limit of the distance between the player and the camera before the player becomes the parent.
        float unparentUpperLimit = upperLimit + yAxisParentToPlayerBuffer;
        float unparentLowerLimit = lowerLimit - yAxisParentToPlayerBuffer;

        // Check if the player is out of the unparent range, and that the parent is currently null
        if ((this.transform.position.y > unparentUpperLimit || this.transform.position.y < unparentLowerLimit) && transform.parent.parent == null)
        {
            transform.parent.parent = player.transform;                                                 // Make the camera's parent's parent the player so the jittering stops
            transform.parent.transform.localPosition = new Vector3(transform.parent.localPosition.x, transform.parent.localPosition.y, -10);
            transform.localPosition= new Vector3(transform.localPosition.x, transform.localPosition.y, -10);
            normalizedYVelocityWhenParented = playerRb.velocity.y / Mathf.Abs(playerRb.velocity.y);     // Save the sign of the velocity so we can unparent when it changes.
        }
        // Check if the player's velocity changed since it was parented
        else if(playerRb.velocity.y / Mathf.Abs(playerRb.velocity.y) != normalizedYVelocityWhenParented)
        {
            normalizedYVelocityWhenParented = 2;                                                                    // Set the variable to 2 since it'll make the if statement above always true until the parent thing happens again
            transform.parent.parent = null;                                                                         // Stop the camera's parent's parent from being parented to the player
            float targetPositionY = (transform.position.y < lowerLimit ? lowerLimit - transform.position.y :         // Find the position to move towards
                                    transform.position.y > upperLimit ? upperLimit - transform.position.y :
                                    0);

            //float targetPositionY = player.transform.position.y + positionToAdd.y;                                 
            this.transform.position += new Vector3(0, (player.transform.position.y + heightAbovePlayer + positionToAdd.y - this.transform.position.y) * Time.deltaTime * camSpeed);   //  Move the camera
        }

    }

    #region Trevor



    //if(Mathf.Abs(playerXScreenSpaceCorrected) >= rangeX )
    //{
    //    if(playerXScreenSpaceCorrected > 0)
    //    {
    //        camera.transform.position = new Vector3(this.transform.position.x + 0.01f, this.transform.position.y, this.transform.position.z);
    //    } else
    //    {
    //        camera.transform.position = new Vector3(this.transform.position.x -0.01f, this.transform.position.y, this.transform.position.z);

    //    }
    //}

    #endregion
}

