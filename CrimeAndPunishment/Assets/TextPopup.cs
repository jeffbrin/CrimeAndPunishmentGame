using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    [TextArea]
    public string text;
    public bool destroyOnExit;
    public KeyCode buttonToDestroy;
    

    private void Update()
    {
        if (GetComponentInChildren<TextMesh>().text == text && Input.GetKeyDown(buttonToDestroy))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponentInChildren<TextMesh>().text = text;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponentInChildren<TextMesh>().text = string.Empty;
            if (destroyOnExit)
                Destroy(gameObject);
        }
    }
}
