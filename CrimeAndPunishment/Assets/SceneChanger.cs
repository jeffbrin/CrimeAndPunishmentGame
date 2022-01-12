using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    string targetScene, displayName;
    bool playerInFront = false;
    TextMesh textMeshComponent;

    // Start is called before the first frame update
    void Start()
    {
        textMeshComponent = GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInFront && Input.GetKeyDown(KeyCode.UpArrow))
        {
            SceneManager.LoadScene(targetScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ToggleDisplayName();
            playerInFront = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ToggleDisplayName();
            playerInFront = false;
        }
    }

    private void ToggleDisplayName()
    {
        if (textMeshComponent.text == string.Empty)
        {
            textMeshComponent.text = displayName;
        }
        else
        {
            textMeshComponent.text = string.Empty;
        }
    }
}
