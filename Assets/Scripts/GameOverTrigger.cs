using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerId playerId = collision.GetComponent<PlayerId>();
        if (playerId != null)
        {
            GameManager.THIS.winnerwinnerChickenDinner = playerId.GetPlayerId() + 1;
        }
    }
}
