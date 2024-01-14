using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    // Adjust this variable based on the tag you assign to the player GameObject.
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the specified player tag.
        if (other.CompareTag(playerTag))
        {
            // Call the HandlePickup method on the GameManager.
            GameManager gameManager = FindObjectOfType<GameManager>(); // Assumes there's only one GameManager in the scene.
            if (gameManager != null)
            {
                gameManager.HandlePickup(other.gameObject);
            }

            // Optionally, you can disable the pickup point or perform other actions.
            gameObject.SetActive(false);
        }
    }
}
