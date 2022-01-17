using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    public bool hasAxe;
    public bool pawnLadyInRange;

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
            FindObjectOfType<GameManager>().ResetStage("Bumping into people will arouse suspicion...");
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PawnBrokerBehaviour>() != null && Input.GetKeyDown(GetComponent<PlayerAnimationControl>().attackKey))
        {
            collision.GetComponent<PawnBrokerBehaviour>().Die();
        }
        else if (collision.gameObject != null && collision.gameObject.tag == "NPC")
        {
            FindObjectOfType<GameManager>().ResetStage("Attacking people is pretty suspicious behaviour...");
        }
    }

}
