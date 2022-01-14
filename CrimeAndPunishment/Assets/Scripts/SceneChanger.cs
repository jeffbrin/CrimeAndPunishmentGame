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
        if (playerInFront && Input.GetKeyDown(KeyCode.W))
        {
            FindObjectOfType<GameManager>().SceneChange();
            FindObjectOfType<SpawnLocationTracker>().PreviousScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(targetScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && targetScene != "Default")
        {
            ToggleDisplayName();
            playerInFront = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && targetScene != "Default")
        {
            ToggleDisplayName();
            playerInFront = false;
        }
    }

    private void ToggleDisplayName()
    {
        if (textMeshComponent.text == string.Empty)
        {
            textMeshComponent.text = $"{displayName} - [W] To Enter";
        }
        else
        {
            textMeshComponent.text = string.Empty;
        }
    }

   public string TargetScene
    {
        get { return targetScene; }
    }
}
