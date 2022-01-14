using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehaviour : MonoBehaviour
{

    string name;
    [SerializeField]
    int movementRadius;
    Vector3 startPosition;
    Vector3 startScale;
    bool movingRight = true;

    [SerializeField]
    int movementSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
            MoveRight();
        else
            MoveLeft();
    }

    void MoveRight()
    {
        transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        if (transform.position.x >= startPosition.x + movementRadius)
        {
            movingRight = false;
            transform.localScale = new Vector3(startScale.x * -1, startScale.y, startScale.z);
        }
        
    }

    void MoveLeft()
    {
        transform.position -= new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        if (transform.position.x <= startPosition.x - movementRadius)
        {
            movingRight = true;
            transform.localScale = startScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.x > transform.position.x && movingRight)
        {
            movingRight = false;
            transform.localScale = new Vector3(startScale.x * -1, startScale.y, startScale.z);
        }
        else if (collision.transform.position.x < transform.position.x && !movingRight)
        {
            movingRight = true;
            transform.localScale = startScale;
        }
    }
}
