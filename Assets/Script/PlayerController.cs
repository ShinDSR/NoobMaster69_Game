using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the object
        MoveObject(movement);
    }

    void MoveObject(Vector3 movement)
    {
        // Calculate the new position
        Vector3 newPosition = transform.position + movement * moveSpeed * Time.deltaTime;

        // Move the object to the new position
        transform.position = newPosition;
    }
}
