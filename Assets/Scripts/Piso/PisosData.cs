using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisosData : MonoBehaviour
{
    [System.Serializable]
    public class DataEnemy
    {
        public int maxEnemies;
        public float life;
        public int powerDrop;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
