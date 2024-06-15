using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PisosData : MonoBehaviour
{

    [System.Serializable]
    public class DataEnemy
    {
        public int maxEnemies;
        public float life;
        public float powerDrop;
        public int coinDrop;
        public float speed;

        public float minSpawnRate;
        public float maxSpawnRate;
    }

    [System.Serializable]
    public class DataPiso
    {
        public DataEnemy enemyPower;
        public DataEnemy enemyCoin;
    }

    [SerializeField] 
    private List<DataPiso> pisosData = new List<DataPiso>(7);


    [SerializeField]
    private Transform pisosContainer;



    // Start is called before the first frame update
    void Start()
    {
        

        setStats();
    }

   
    private void setStats()
    {
        for(int i = 0; i < pisosData.Count; i++)
        {
            if (i >= pisosContainer.childCount - 1) return;

            var child = pisosContainer.GetChild(i);

            var enemySpawnerList =  child.GetComponentsInChildren<EnemySpawner>(true);


            if (enemySpawnerList[0].getEnemyType() == 0) 
            {
                enemySpawnerList[0].InsertData(pisosData[i].enemyPower);
                enemySpawnerList[1].InsertData(pisosData[i].enemyCoin);
            }
            else
            {
                enemySpawnerList[0].InsertData(pisosData[i].enemyCoin);
                enemySpawnerList[1].InsertData(pisosData[i].enemyPower);
            }
        }

    }
}
