using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager gm = FindObjectOfType<GameManager>();

        if (gm.InWinState() == 1)
        {
            gm.ShowEndGame("You win! You successfully killed the pawn lady, collected the jewelry and remained unseen.");
        }
        else if (gm.InWinState() == 2)
        {
            gm.ShowEndGame("You failed to collect the jewlery, try again...");
        }
        else if (gm.InWinState() == 3)
        {
            gm.ShowEndGame("Lizaveta witnessed the murder, you've been caught...");
        }
    }
}
