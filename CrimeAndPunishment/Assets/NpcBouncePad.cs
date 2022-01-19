using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBouncePad : MonoBehaviour
{

    public bool endPoint;
    public float force, secondaryForce;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<InspectorGuy>() != null)
        {
            if (endPoint)
                other.gameObject.GetComponent<InspectorGuy>().Stop();
            else
            {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(force * Vector2.up, ForceMode2D.Impulse);
                force = secondaryForce;
            }
        }
    }
}
