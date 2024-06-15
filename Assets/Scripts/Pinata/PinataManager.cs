using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataManager : MonoBehaviour
{
     public int powerDrop, coinDrop;

    private EnemySpawner spawner;

    public void setSpawner(EnemySpawner _spawner)
    { 
        spawner = _spawner;
    }


    private void OnDestroy()
    {
        spawner.PinataDied();
    }
}
