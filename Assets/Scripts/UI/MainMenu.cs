using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void GoToTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToOverworld()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene(4);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
