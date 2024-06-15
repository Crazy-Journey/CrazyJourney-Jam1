using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerId : MonoBehaviour
{
    public static int nextId;

    public int playerId;

    private void Awake()
    {
        if (nextId >= 2)
        {
            Destroy(gameObject);
            return;
        }
        playerId = nextId;
        nextId++;
    }

    public int GetPlayerId()
    {
        return playerId;
    }
}
