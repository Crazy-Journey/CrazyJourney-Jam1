using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{

    [SerializeField] Transform objectTr;


    private Transform pisosContainer;
    private List<Piso> pisos = new List<Piso>();


    void Start()
    {
        GetPisos();

        SpawnOnPiso(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetPisos()
    {
        pisosContainer = GameObject.FindGameObjectWithTag("ContenedorPisos").transform;

        for (int i = 0; i < pisosContainer.childCount; i++)
        {
            if (pisosContainer.childCount <= i)
                break;

            pisos.Add(pisosContainer.GetChild(i).gameObject.GetComponent<Piso>());
        }
    }

    public void SpawnOnPiso(int p)
    {
        Spawner[] spawners = pisos[p].GetComponentsInChildren<Spawner>();

        foreach (Spawner spawner in spawners)
        {
            if (!spawner.IsUsed())
            {
                objectTr.position = spawner.transform.position;
                spawner.SetSpawnUsed(true);
                break;
            }
        }
    }


}
