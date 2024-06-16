using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerId playerId = collision.GetComponent<PlayerId>();
        if (playerId != null)
        {
            GameManager.THIS.winnerwinnerChickenDinner = playerId.GetPlayerId();
            SceneManager.LoadSceneAsync(3);
        }
    }
}
