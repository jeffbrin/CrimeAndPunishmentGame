using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    public bool hasAxe;
    public GameObject pawnBroker;
    public bool npcInAttackRange;

    public PlayerLogic(bool hasAxe)
    {
        this.hasAxe = hasAxe;
    }


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
                GetComponent<PlayerUI>().ShowText("[F] to use axe.");
                break;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PawnBrokerBehaviour>() != null)
        {
            pawnBroker = null;
        }
        else if (collision.gameObject.tag == "NPC")
        {
            npcInAttackRange = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PawnBrokerBehaviour>() != null)
        {
            pawnBroker = collision.gameObject;
        }
        else if (collision.gameObject.tag == "NPC")
        {
            npcInAttackRange = true;
        }
    }

    public void Attack()
    {
        Debug.Log(npcInAttackRange);
        if (pawnBroker != null)
        {
            pawnBroker.GetComponent<PawnBrokerBehaviour>().Die();
            Debug.Log("Kill");
        }
        if (npcInAttackRange)
        {
            Debug.Log("YUHYUH");
            FindObjectOfType<GameManager>().ResetStage("Attacking people is pretty suspicious behaviour...");
        }
    }

}
