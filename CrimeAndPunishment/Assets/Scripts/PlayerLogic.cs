using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    public bool hasAxe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            FindObjectOfType<GameManager>().ResetStage("Hitting people will arouse suspicion...");
        }
    }

    public void GetItem(string itemName)
    {
        switch (itemName)
        {
            case "Axe":
                hasAxe = true;
                break;
        }
    }
}
