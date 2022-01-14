using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{

    [SerializeField, Range(0,100)]
    int hoverRadius = 2, hoverSpeed;
    float startingY;
    bool rising = true;
    [SerializeField]
    string name;

    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (rising)
        {
            transform.position += new Vector3(0, (float)hoverSpeed / 100 * Time.deltaTime, 0);
            if (transform.position.y >= startingY + (double)hoverRadius / 100)
                rising = false;
        }
        else
        {
            transform.position -= new Vector3(0, (float)hoverSpeed / 100 * Time.deltaTime, 0);
            if (transform.position.y <= startingY - (double)hoverRadius / 100)
                rising = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().color = Color.grey;
            GetComponentInChildren<TextMesh>().text = name + " [E] to pick up.";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponentInChildren<TextMesh>().text = string.Empty;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                collision.gameObject.GetComponent<PlayerLogic>().GetItem(name);
                Destroy(gameObject);
            }
        }
    }


}
