using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBrokerBehaviour : MonoBehaviour
{

    Animator anim;
    Transform targetPosition;
    [SerializeField]
    int speed;
    [SerializeField]
    string name;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        targetPosition = GameObject.FindWithTag("Tracker").transform;
        GetComponentInChildren<TextMesh>().text = name;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    public void Die()
    {
        anim.Play("GrannyDead");
        GetComponent<PawnBrokerBehaviour>().enabled = false;
        GetComponent<BoxCollider2D>().size = new Vector3(GetComponent<BoxCollider2D>().size.x, GetComponent<BoxCollider2D>().size.y / 2.5f);
        FindObjectOfType<GameManager>().IndicateDeath();
    }

    void FollowPath()
    {
        if (transform.position != targetPosition.position)
        {
            float direction = targetPosition.position.x + 0.1 < transform.position.x ? -1 : 
                              targetPosition.position.x - 0.1 > transform.position.x ? 1 : 0;

            GetComponentInChildren<TextMesh>().transform.localScale = new Vector3(transform.localScale.x < 0 ? -1 : 1,  1, 1);

            // Change facing
            if (direction != 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);
                anim.Play("GrannyWalk");
            }
            else
            {
                anim.Play("GrannyIdle");
            }

            transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);

        }
    }

}
