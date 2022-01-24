using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool playerInFront = false;
    public TextMesh childText;
    bool isOpened = false;
    public BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        childText = GetComponentInChildren<TextMesh>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInFront)
        {
            isOpened = !isOpened;
            GetComponent<SpriteRenderer>().color = isOpened ? Color.grey : Color.white;
            collider.enabled = !isOpened;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            playerInFront = true;
            childText.text = "[E] to " + (isOpened ? "close" : "open") + " door.";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInFront = false;
            childText.text = string.Empty;
        }
    }
}
