using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform centerSpawn;

    [SerializeField]
    private float spawnWidth;
    [SerializeField]
    private float spawnHeight;

    [Header("Stats")]
    [SerializeField]
    private float enemyLife;

    [SerializeField]
    private int enemyPowerDrop;

    [SerializeField]
    private int enemyCoinDrop;

    [SerializeField]
    private float enemySpeed;

    
    [Header("Time")]
    [SerializeField]
    private float spawnRate;
  
    private float elapsedTime = 0;



    // Update is called once per frame
    void Update()
    {
        spawnEnemies();
    }

    private void spawnEnemies()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > spawnRate)
        {
            elapsedTime = 0;
            //generate enemy
            GameObject newEnemy = Instantiate(enemyPrefab,getRandomSpawnPos(),Quaternion.identity);

            //set stats

            newEnemy.GetComponent<LifeComponent>().setMaxLife(enemyLife);

            newEnemy.GetComponent<PinataManager>().coinDrop = enemyCoinDrop;
            newEnemy.GetComponent<PinataManager>().powerDrop = enemyPowerDrop;

            newEnemy.GetComponent<PinataMovement>().setSpeed(enemySpeed);

        }
        
    }

    private Vector3 getRandomSpawnPos()
    {
        Vector3 pos = centerSpawn.position; 

        pos.x += Random.Range(-spawnWidth, spawnWidth);
        pos.y += Random.Range(-spawnHeight, spawnHeight);

        return pos;
    }
}
