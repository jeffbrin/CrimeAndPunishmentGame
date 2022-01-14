using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{

    TextMesh messageText;

    // Start is called before the first frame update
    void Awake()
    {
        messageText = GetComponentsInChildren<TextMesh>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowText(string text)
    {
        messageText.text = text;
        StartCoroutine(RemoveMessageText(3));
    }

    public void ShowThought(string thought)
    {

    }

    IEnumerator RemoveMessageText(int wait)
    {
        yield return new WaitForSeconds(wait);
        messageText.text = string.Empty;
        StopCoroutine("RemoveMessageText");
    }
}
