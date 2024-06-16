using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private int type;
    public int getEnemyType () { return type; } 


    [Header("Spawn")]
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform centerSpawn;

    [SerializeField]
    private float spawnWidth;
    [SerializeField]
    private float spawnHeight;

    [SerializeField]
    private int maxEnemies;

    [Header("Stats")]
    [SerializeField]
    private float enemyLife;

    [SerializeField]
    private float enemyPowerDrop;

    [SerializeField]
    private int enemyCoinDrop;

    [SerializeField]
    private float enemySpeed;

    
    [Header("Time")]


    [SerializeField]
    private float minSpawnRate;
    [SerializeField]
    private float maxSpawnRate;


    private float spawnRate;
  
    private float elapsedTime = 0;


    //contator de enemigos
    private int enemiesCount = 0;

    private void Start()
    {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemies();
    }

    public void PinataDied()
    {
        enemiesCount--;
    }

    private void spawnEnemies()
    {
        if (!InputReady.instance.PlayersReady) return;

        elapsedTime += Time.deltaTime;

        if(elapsedTime > spawnRate && enemiesCount < maxEnemies )
        {
            elapsedTime = 0;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);

            //generate enemy
            GameObject newEnemy = Instantiate(enemyPrefab,getRandomSpawnPos(),Quaternion.identity);

            //set stats

            newEnemy.GetComponent<LifeComponent>().setMaxLife(enemyLife);

            newEnemy.GetComponent<PinataManager>().coinDrop = enemyCoinDrop;
            newEnemy.GetComponent<PinataManager>().powerDrop = enemyPowerDrop;

            newEnemy.GetComponent<PinataMovement>().setSpeed(enemySpeed);


            newEnemy.GetComponent<PinataManager>().setSpawner(this);


            enemiesCount++;

        }
        
    }

    private Vector3 getRandomSpawnPos()
    {
        Vector3 pos = centerSpawn.position; 

        pos.x += Random.Range(-spawnWidth, spawnWidth);
        pos.y += Random.Range(-spawnHeight, spawnHeight);

        return pos;
    }

    public void InsertData(PisosData.DataEnemy dataEnemy)
    {
        maxEnemies = dataEnemy.maxEnemies;
        enemyLife = dataEnemy.life; 
        enemySpeed = dataEnemy.speed;
        enemyCoinDrop = dataEnemy.coinDrop;
        enemyPowerDrop = dataEnemy.powerDrop;

        minSpawnRate = dataEnemy.minSpawnRate;
        maxSpawnRate = dataEnemy.maxSpawnRate;
    }
}
