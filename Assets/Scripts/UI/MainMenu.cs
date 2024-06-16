using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void GoToTutorial()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void GoToOverworld()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void GoToCredits()
    {
        SceneManager.LoadSceneAsync(4);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
