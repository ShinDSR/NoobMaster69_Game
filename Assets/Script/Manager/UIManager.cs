using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Canvas GUI;
    public Canvas OverDisplay;
    public Canvas powerUp;

    //variable for player score.
    private int playerScore;

    public TextMeshProUGUI nameTxt; // Name
    public TextMeshProUGUI scoreTxt; // Score

    // Start is called before the first frame update
    void Start()
    {
        scoreTxt.text = "score : " + playerScore;
    }

    // Function to update the player's score on the UI.
    private void UpdateScoreUI(int playerScore)
    {
        scoreTxt.text = "score : " + playerScore;
    }

    // Function GameOver
    public void GameOver()
    {
        GUI.gameObject.SetActive(false);
        OverDisplay.gameObject.SetActive(true);
    }

    public void GameMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PowerActive()
    {
        powerUp.gameObject.SetActive(true);
    }

    public void PowerDeactive()
    {
        powerUp.gameObject.SetActive(false);
    }

}
