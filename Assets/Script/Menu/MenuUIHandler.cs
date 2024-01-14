using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public void NewGameScene()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadGameScene()
    {
        MainManager.Instance.LoadData();
        SceneManager.LoadScene(MainManager.Instance.sceneIndex); //get saved Scene
    }

    public void QuitGame()
    {
        //EditorApplication.ExitPlaymode();
        Application.Quit(); //for real game
    }
}
