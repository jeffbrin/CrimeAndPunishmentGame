using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlyonaConvo : MonoBehaviour
{
    [TextArea]
    public string[] text = {
        "Good evening, Alyona Ivanovna",
        "Lord! What is it?...",
        "Who are you? \n What's your business?",
        "For pity's sake, Alyona Ivanovna...",
        "you know me... Raskolnikov...",
        "",
        "here, I've brought you that pledge...",
        "the one I promised you the other day...",
        "",
        "If you want it, take it",
        "otherwise I'll go somewhere else.",
        "I have no time.",
        "",
        "But why are you so pale?",
        "Look, your hands are trembling!",
        "Did you go for a swim, dearie, or what?",
        "",
        "Fever",
        "",
        "What is it?",
        "An article... a cigarette case... \n silver... take a look",
        "Look how he's wrapped it up!",
        "I can't waste one more moment! \n Press [F] to kill her."
    };
    public bool destroyOnExit;

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foreach(string s in text){
                GetComponentInChildren<TextMesh>().text = s;
                yield return new WaitForSeconds(2);
            }            
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
