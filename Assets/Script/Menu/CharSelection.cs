using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;
    public TMP_InputField input; // assign this input in editor
    public string playerName; // variable for name

    // function to detect ValueChanged -> set this script in editor too
    public void OnValueChanged()
    {
        playerName = input.text;
        //Debug.Log(playerName);
    }

    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void StartGame()
    {
        MainManager.Instance.playerName = playerName;
        MainManager.Instance.playerPosition = Vector3.zero; // set this to default spawn position
        MainManager.Instance.m_Points = 0; // because new game so its 0
        MainManager.Instance.selectedCharacter = selectedCharacter;
        MainManager.Instance.scenePoints = 0;
        MainManager.Instance.SaveData(); // Save new data

        SceneManager.LoadScene(3); // start game in first scene -> set the number
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
    }

    public void BackScene()
    {
        SceneManager.LoadScene(0);
    }
}
