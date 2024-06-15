using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataManager : MonoBehaviour
{
    public int powerDrop, coinDrop;
    [SerializeField] GameObject particleEffect;

    private EnemySpawner spawner;

    public void setSpawner(EnemySpawner _spawner)
    { 
        spawner = _spawner;
    }


    private void OnDestroy()
    {
        spawner.PinataDied();
        GameObject effect = Instantiate(particleEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(effect, 10f);
    }
}
