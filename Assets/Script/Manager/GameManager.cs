using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*     * UI Elements     */
    public Canvas GUI;
    public Canvas OverDisplay;
    public Canvas powerUp;

    public TextMeshProUGUI nameTxt; // Name
    public TextMeshProUGUI scoreTxt; // Score
    public TextMeshProUGUI checkpointTxt;
    /*     * -----------------------------------------     */

    /*     * Spawner     */
    //public GameObject car;
    public GameObject[] cars;
    public Transform[] spawnerLocations; // Use Transform for storing positions

    //public GameObject Woods;
    public GameObject[] woods;
    public Transform[] WoodsLocations;

    //public GameObject Birds;
    public GameObject[] birds;
    public Transform[] BirdsLocations;
    /*     * -----------------------------------------     */

    /*     * Script Variable     */
    private int playerScore; //variable for player score.
    private int scorePerScene; //variable scene score.

    public bool isGameOver; // gameOver Condition
    /*     * -----------------------------------------     */

    private AudioSource audioSource;
    public AudioClip pickupSound;
    public AudioClip gameOverSound;
    public AudioClip checkpointSound;
    public AudioClip powerUpSound;
    public AudioClip finishSound;


    // Start is called before the first frame update
    void Start()
    {
        nameTxt.text = MainManager.Instance.playerName;
        playerScore = MainManager.Instance.m_Points;

        scorePerScene = MainManager.Instance.scenePoints;

        GUI.gameObject.SetActive(true);
        scoreTxt.text = "score : " + playerScore;

        isGameOver = false; // set game status

        audioSource = GetComponent<AudioSource>();

        if (!isGameOver)
        {
        InvokeRepeating("SpawnCars", 0, 3); //spawner function
        InvokeRepeating("SpawnWoods", 0, 4);
        InvokeRepeating("SpawnBirds", 0, 1);
        }

        saveOnLoadScene();
    }

    // Function to spawnCars
    /*
    private void SpawnCars()
    {
        int x = Random.Range(0, cars.Length);
        Instantiate(cars[x], new Vector3(-13, 2, -8), cars[x].transform.rotation);
    }
    */

    private void SpawnCars()
    {
        foreach (Transform spawnLocation in spawnerLocations)
        {
            int x = Random.Range(0, cars.Length);
            Instantiate(cars[x], spawnLocation.position, cars[x].transform.rotation);
        }
    }

    private void SpawnWoods()
    {
        foreach (Transform spawnLocation in WoodsLocations)
        {
            int x = Random.Range(0, woods.Length);
            Instantiate(woods[x], spawnLocation.position, woods[x].transform.rotation);
        }
    }
    
    private void SpawnBirds()
    {
        foreach (Transform spawnLocation in BirdsLocations)
        {
            int x = Random.Range(0, birds.Length);
            Instantiate(birds[x], spawnLocation.position, birds[x].transform.rotation);
        }
    }

    private void saveOnLoadScene()
    {
        MainManager.Instance.m_Points = playerScore;
        MainManager.Instance.SaveData(); // Save new data
    }

    // Function to handle pickup.
    public void HandlePickup(GameObject player) 
    {
        audioSource.PlayOneShot(pickupSound);
        playerScore += 10;
        UpdateScoreUI();
    }

    // Function to update the player's score on the UI.
    private void UpdateScoreUI()
    {
        // Debug.Log("Player's Score: " + playerScore);

        scoreTxt.text = "score : " + playerScore;
        MainManager.Instance.m_Points = playerScore;
    }

    // Function GameOver
    public void GameOver()
    {
        GUI.gameObject.SetActive(false);
        OverDisplay.gameObject.SetActive(true);
        audioSource.PlayOneShot(gameOverSound);

        //Stop spawner function
        CancelInvoke("SpawnCars"); 
        CancelInvoke("SpawnWoods");
        CancelInvoke("SpawnBirds");

        //Debug.Log("GameOver!");
        isGameOver = true;
    }

    public void GameMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NewGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartGame()
    {
        MainManager.Instance.playerPosition = Vector3.zero; // Reset to Default Position or set this to default spawn position
        MainManager.Instance.m_Points = scorePerScene;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //Debug.Log(MainManager.Instance.playerPosition);
    }

    public void StageCompleted()
    {
        StartCoroutine(PlaySoundAndLoadNextScene());
    }

    IEnumerator PlaySoundAndLoadNextScene()
    {
        // Play the finish sound
        if (finishSound != null)
        {
            audioSource.PlayOneShot(finishSound);
            yield return new WaitForSeconds(finishSound.length); // Wait for the sound to finish
        }

        MainManager.Instance.scenePoints = playerScore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        MainManager.Instance.playerPosition = Vector3.zero; // Reset to Default Position
    }

    public void PowerActive()
    {
        powerUp.gameObject.SetActive(true);
        audioSource.PlayOneShot(powerUpSound);
    }

    public void PowerDeactive()
    {
        powerUp.gameObject.SetActive(false);
    }

    public void onCheckpoint()
    {
        audioSource.PlayOneShot(checkpointSound);
        checkpointTxt.gameObject.SetActive(true);
        StartCoroutine("TimerTxt");
    }

    private IEnumerator TimerTxt()
    {
        yield return new WaitForSeconds(3.0f); // Wait Second before poweroff = false
        checkpointTxt.gameObject.SetActive(false);
    }

}
