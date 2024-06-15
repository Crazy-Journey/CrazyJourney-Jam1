using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{

    private Transform contenedorPisos;

    private List<double> floorHeights = new List<double>();

    private Transform myTransform;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;

        contenedorPisos = GameObject.FindGameObjectWithTag("ContenedorPisos").transform;


        for (int i = 0; i < contenedorPisos.transform.childCount; i++)
        {
            floorHeights.Add(contenedorPisos.GetChild(i).gameObject.GetComponent<Piso>().GetElevatorHeightTarget());
        }
    }

    // Update is called once per frame
    void Update()
    {

        myTransform.position = new Vector3(myTransform.position.x,
            (float)
            (
            (floorHeights[PlayerDataManager.THIS.GetPlayer(0).GetPiso()] + 
            floorHeights[PlayerDataManager.THIS.GetPlayer(1).GetPiso()]) /2.0), 
            myTransform.position.z);

    }



}
