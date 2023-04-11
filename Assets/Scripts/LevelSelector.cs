using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadLevel(int levelNum)
    {
        //Loads Specific Level
        SceneManager.LoadScene("Level " + levelNum);
    }

    public void LoadMainMenu()
    {
        //Loads Main Menu
        SceneManager.LoadScene("MainScene");
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            ObjectiveCompletion.LevelEnded += LoadNextLevel;
        }
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            ObjectiveCompletion.LevelEnded -= LoadNextLevel;
        }
    }

    private void LoadNextLevel()
    {
        var buildIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(buildIndex);
        var sceneToLoad = buildIndex == 5 ? 0 : buildIndex + 1;
        Debug.Log(sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
