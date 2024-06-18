using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerId : MonoBehaviour
{
    public static int nextId;

    public static bool canConnect = true;

    public int playerId;

    private void Awake()
    {
        if (nextId >= 2 || !canConnect)
        {
            Destroy(gameObject);
            return;
        }

        StartCoroutine(ConectionCoroutine());

        playerId = nextId;
        nextId++;
    }

    public int GetPlayerId()
    {
        return playerId;
    }

    public void ResetIds() {
        nextId = 0;
    }

    IEnumerator ConectionCoroutine() {
        canConnect = false;
        yield return new WaitForSeconds(0.1f);
        canConnect = true;

    }

    public bool CanConnect() { return canConnect; }
}
