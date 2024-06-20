using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerId playerId = collision.GetComponent<PlayerId>();
        if (playerId != null)
        {
            GameManager.THIS.winnerwinnerChickenDinner = playerId.GetPlayerId();
            playerId.ResetIds();
            SceneManager.LoadSceneAsync(3);
        }
    }
}
