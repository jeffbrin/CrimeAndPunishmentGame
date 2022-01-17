using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrackerPositionChanger : MonoBehaviour
{
    public float MoveToPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindWithTag("Tracker").transform.position = new Vector3(MoveToPosition, GameObject.FindWithTag("Tracker").transform.position.y);
            Destroy(gameObject);
        }
    }
}
