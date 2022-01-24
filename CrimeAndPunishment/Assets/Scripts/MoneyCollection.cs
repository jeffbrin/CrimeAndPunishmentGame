using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollection : MonoBehaviour
{

    string text = "[E] to collect the jewelry";
    
    private void Update()
    {
        if (GetComponentInChildren<TextMesh>().text == text && Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<GameManager>().GetMoney();
            PawnBrokerBehaviour[] pawnBrokers = Resources.FindObjectsOfTypeAll<PawnBrokerBehaviour>();
            foreach (PawnBrokerBehaviour pbb in pawnBrokers)
            {
                Debug.Log(pbb);
                pbb.gameObject.SetActive(true);
            }
            text = "Get away unseen.";
            GetComponentInChildren<TextMesh>().text = text;
        }
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
        }
    }
}
