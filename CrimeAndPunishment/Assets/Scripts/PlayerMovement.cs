using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Movement related stuff
    public int airControl = 4;
    public float maxSpeed;
    public bool grounded;
    public float jumpHeight;
    public float jumpLength;
    [Range(1, 4)]
    public float deaccelertaion = 5;
    public float terminalVelocityY;
    public float gravity;
    float initialVelocity;
    [Range(0, 100)]
    public float regularSpeed = 0;
    public float sprintingSpeedMultiplier = 2;
    public float sprintingSpeedMultiplierAir = 1.5f;

    string lastMoveDirection = "";

    Vector3 regularScale;

    // Input key constants stored here to be changed if desired
    [Header("KeyBinds")]
    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode JumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        initialVelocity = 2 * jumpHeight / (jumpLength / 2);
        gravity = -initialVelocity / (jumpLength / 2);
        rb.gravityScale = gravity / Physics2D.gravity.y;
        regularScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(JumpKey) && grounded) Jump();
        Sprint();
        HorizontalMovement();
        ClampFallSpeed();
    }

    void FixedUpdate()
    {

    }

    // This obviously needs ground checks
    void Jump()
    {
        GetComponent<PlayerAnimationControl>().Jump();

        // Calculate initial velocity and gravity
        initialVelocity = 2 * jumpHeight / (jumpLength / 2);
        gravity = -initialVelocity / (jumpLength / 2);

        // Get the gravity scale according to global gravity
        rb.gravityScale = gravity / Physics2D.gravity.y;

        // Set the velocity
        rb.velocity = new Vector2(rb.velocity.x, initialVelocity);
    }

    // This increases the speed of the player when the shift key is held down
    void Sprint()
    {

        bool sprinting = Input.GetKey(sprintKey);
        maxSpeed = grounded ? 
            (sprinting ? regularSpeed * sprintingSpeedMultiplier : regularSpeed):
            (sprinting ? regularSpeed * sprintingSpeedMultiplierAir : regularSpeed);
    }

    void HorizontalMovement()
    {
        lastMoveDirection = Input.GetKeyDown(rightKey) ? "Right" :
                            Input.GetKeyDown(leftKey) ? "Left" :
                            lastMoveDirection;
        lastMoveDirection = Input.GetKeyUp(rightKey) ? "Left" :
                            Input.GetKeyUp(leftKey) ? "Right" :
                            lastMoveDirection;

        string direction = "Left";
        if (rb.velocity.x > 0)
        {
            direction = "Right";
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position + Vector3.right * (direction == "Left" ? -1 : 1), GetComponent<BoxCollider2D>().bounds.size.x / 2 + 0.1f);

        if (hit == null) return;

        if (Input.GetKey(rightKey) && rb.velocity.x < maxSpeed && lastMoveDirection == "Right")
        {
            if (grounded)
            {
                //if we are on the ground, just apply the velocity we want
                rb.velocity = new Vector2((Vector2.right * maxSpeed).x, rb.velocity.y);
            }
            else
            {
                //in midair, add a bit of force so we keep the kinda midair speed stuff
                rb.AddForce(Vector2.right * airControl);
            }

            // Face right
            try
            {
                GameObject camera = GetComponentInChildren<Camera>().gameObject;
                Transform XTracker = GetComponentInChildren<TrackPlayerX>().transform;
                XTracker.parent = null;
                transform.localScale = regularScale;
                GetComponentInChildren<TextMesh>().transform.localScale = new Vector3(0.2f, 0.2f, 1);
                XTracker.parent = transform;
            }
            catch
            {
                transform.localScale = regularScale;
                GetComponentInChildren<TextMesh>().transform.localScale = new Vector3(0.2f, 0.2f, 1);
            }

        }
        else if (Input.GetKey(leftKey) && rb.velocity.x > -maxSpeed && lastMoveDirection == "Left")
        {
            if (grounded)
            {

                rb.velocity = new Vector2((Vector2.left * maxSpeed).x, rb.velocity.y);

            }
            else
            {
                rb.AddForce(Vector2.left * airControl);
            }

            // Face left
            try
            {
                GameObject camera = GetComponentInChildren<Camera>().gameObject;
                Transform XTracker = GetComponentInChildren<TrackPlayerX>().transform;
                XTracker.parent = null;
                transform.localScale = new Vector3(regularScale.x * -1, regularScale.y, regularScale.z);
                GetComponentInChildren<TextMesh>().transform.localScale = new Vector3(-0.2f, 0.2f, 1);
                XTracker.parent = transform;
            }
            catch
            {
                transform.localScale = new Vector3(regularScale.x * -1, regularScale.y, regularScale.z);
                GetComponentInChildren<TextMesh>().transform.localScale = new Vector3(-0.2f, 0.2f, 1);
            }

        }
        else
        {
            if (grounded && !Input.GetKey(rightKey) && !Input.GetKey(leftKey))
            {
                //if grounded, use our own deacceleration, otherwise use unity's physic
                rb.velocity = new Vector2((rb.velocity.x / deaccelertaion), rb.velocity.y);
            }
        }
    }

    private void ClampFallSpeed()
    {
        if (rb.velocity.y < -terminalVelocityY)
        {
            rb.velocity = new Vector3(rb.velocity.x, -terminalVelocityY);
        }
    }

    

    #region Properties

    public float MaxSpeed
    {
        get { return maxSpeed; }
    }

    #endregion
}
