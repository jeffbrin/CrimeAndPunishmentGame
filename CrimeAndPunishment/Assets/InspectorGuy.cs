using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorGuy : MonoBehaviour
{

    public float speed;
    Transform player;

    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

        if (DistanceFrom(player) < 4)
            FindObjectOfType<GameManager>().ResetStage("You've been caught! Be stealthier this time.");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private float DistanceFrom(Transform other)
    {
        return Mathf.Sqrt(Mathf.Pow(transform.position.x - other.position.x, 2) + Mathf.Pow(transform.position.y - other.position.y, 2));
    }

    public void Stop()
    {
        speed = 0;
        GetComponent<Animator>().enabled = false;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
