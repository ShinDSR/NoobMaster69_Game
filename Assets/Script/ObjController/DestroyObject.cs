using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private float rightBoundary = 32.0f;
    private float leftBoundary = -32.0f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > rightBoundary || transform.position.x < leftBoundary) //Destroy Condition
        {
            Destroy(gameObject);   //Destroy Game Object with 'Destroy'
        }
    }
}
