using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset = new Vector3(0, 5, -5);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Keep the camera centered on the player's Y and Z positions
        float playerY = player.transform.position.y;
        float playerZ = player.transform.position.z;

        // Update the camera's position
        transform.position = new Vector3(transform.position.x, playerY + offset.y, playerZ + offset.z);
    }
}
