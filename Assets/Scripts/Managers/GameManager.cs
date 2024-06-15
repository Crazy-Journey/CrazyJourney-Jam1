using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        MainMenu,
        Loading,
        ConnectPads,
        Playing,
        Pause,
        GameOver
    }

    public static GameManager THIS;
    public GameStates currentState;

    private void Awake()
    {
        // Uso de singleton
        if (THIS == null)
        {
            THIS = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void SetState(GameStates newState)
    {
        currentState = newState;
        Debug.Log("** GameManager -->  " + currentState);

        switch (currentState)
        {
            // ***************************************************
            case GameStates.MainMenu:
                break;
            // ***************************************************
            case GameStates.Loading:
                break;
            // ***************************************************
            case GameStates.ConnectPads:
                break;
            // ***************************************************
            case GameStates.Playing:
                break;
            // ***************************************************
            case GameStates.Pause:
                break;
            // ***************************************************
            case GameStates.GameOver:
                break;
            // ***************************************************
        }
    }

}
