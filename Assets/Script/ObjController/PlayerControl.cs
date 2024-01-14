using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;

    private GameManager gmScript; // use GameManaget Object -> Script
    private Rigidbody playerRb; // use this Object RigidBody

    private bool onGround = true; //condition for jumping
    private bool hasPowerUp = false; //condition when pickup powerup

    float boundaries = 24; // Player boundaries max movement

    public Vector3 spawn;
    private Vector3 checkpoint; // Checkpoint position

    private AudioSource audioSource;
    public AudioClip jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); //initiate component Rigidbody

        audioSource = GetComponent<AudioSource>();
        gmScript = GameObject.Find("GameManager").GetComponent<GameManager>(); // Load GameManager Script

        GameObject spawnpoint = GameObject.Find("SpawnPoint");

        if (MainManager.Instance.playerPosition == Vector3.zero)
        {
            spawn = spawnpoint.transform.position;
        } else
        {
            spawn = MainManager.Instance.playerPosition;
        }

        // checkpoint = MainManager.Instance.playerPosition; // Load Playerposition

        this.transform.position = spawn; // move player to latest position or in default position (new)
    }

    private void Update()
    {
        float hInput = Input.GetAxis("Horizontal"); // get H Axis Input
        float vInput = Input.GetAxis("Vertical"); // get V Axis Input

        //run this player movement if not gameOver
        if (!gmScript.isGameOver)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * vInput);
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * hInput);

            float xBoundaries = Mathf.Clamp(transform.position.x, -boundaries, boundaries); // player boundaries calculation
            transform.position = new Vector3(xBoundaries, transform.position.y, transform.position.z); // player position when in boundaries

            //using space to jump
            if (Input.GetKeyDown(KeyCode.Space) && onGround)
            {
                playerRb.AddForce(Vector3.up * 3, ForceMode.Impulse);
                audioSource.PlayOneShot(jumpSound);
                onGround = false;
            }
        }
    }


    /*
     * Function to trigger Game Over or Reset Game when touching/colide other object,
     * make it run when touch ground/enemies/car/water
     * Or can use it for power Up
    */
    private void OnCollisionEnter(Collision collision)
    {
        onGround = true; // set onGround=true when hit other
                         // Debug.Log(collision.gameObject.tag);

        //if object hit 'Ground'
        if (collision.gameObject.CompareTag("Ground") /* name == "base"*/)
        {
            gmScript.GameOver();
        }

        //if object hit Enemy but have powerUp
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Destroy(collision.gameObject);
        }

        //if object hit Enemy but didnt have powerup
        if (collision.gameObject.CompareTag("Enemy") && !hasPowerUp)
        {
            gmScript.GameOver();
        }

        //if hit finishline
        if (collision.gameObject.CompareTag("Finish"))
        {
            gmScript.StageCompleted();
        }

    }



// Function when this object triggered with something
private void OnTriggerEnter(Collider other)
    {
        //if hit PowerUp
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            // powerIndicator.gameObject.SetActive(true);
            //Debug.Log(hasPowerUp);

            //StartCoroutine(PowerUpTimer());
            gmScript.PowerActive();
            StartCoroutine("PowerUpTimer");
        }

        //if hit checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            Destroy(other.gameObject);
            gmScript.onCheckpoint();

            MainManager.Instance.playerPosition = this.transform.position;
            MainManager.Instance.SaveData();
        }


    }

    // Powerup Timer Function
    private IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(4.0f); // Wait Second before poweroff = false
        hasPowerUp = false;
        gmScript.PowerDeactive();

        //Debug.Log(hasPowerUp);
        // powerIndicator.gameObject.SetActive(false);
    }


    /*https://discussions.unity.com/t/how-to-get-a-character-to-move-with-a-moving-platform/1720/3
     */
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Wood"))
        {
            transform.parent = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Wood"))
        {
            transform.parent = null;
        }
    }
}
