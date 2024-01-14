using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayers : MonoBehaviour
{

    private GameObject player;
    private Vector3 offset = new Vector3(1, 23, 0);
    private string playerTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        // Find the player GameObject by tag
        player = GameObject.FindGameObjectWithTag(playerTag);

        // Check if the player GameObject was found
        if (player == null)
        {
            //Debug.LogError("Player not found with tag: " + playerTag);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        // Keep the camera centered on the player's Y and Z positions
        if (player != null)
        {
            float playerY = player.transform.position.y;
            float playerZ = player.transform.position.z;

            // Update the camera's position
            transform.position = new Vector3(transform.position.x, playerY + offset.y, playerZ + offset.z);
        }
    }
}
