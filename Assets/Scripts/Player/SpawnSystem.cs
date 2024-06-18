using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{

    [SerializeField] Transform objectTr;
    [SerializeField] PlayerId playerId;

    private Transform pisosContainer;
    private List<Piso> pisos = new List<Piso>();


    void Start()
    {
        GetPisos();

        SpawnOnPiso();
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

    public void SpawnOnPiso()
    {
        // Obtenemos el piso objetivo a partir del data del player
        int targetPiso = PlayerDataManager.THIS.GetPlayer(playerId.GetPlayerId()).GetPiso();

        // Obtenemos los spawners del piso objetivo
        Spawner[] spawners = pisos[targetPiso].GetComponentsInChildren<Spawner>();
        
        // Obtenemos el spawner correspondiente al player según su id
        objectTr.position = spawners[playerId.GetPlayerId()].transform.position;
    }


}
