using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5;
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (transform.position.x > 0)
        {
            speed = -speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.isGameOver) { 
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }
}
