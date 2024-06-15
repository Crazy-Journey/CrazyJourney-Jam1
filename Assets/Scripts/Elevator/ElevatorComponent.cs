using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ElevatorComponent : MonoBehaviour
{
    [Header("Pisos")]


    [SerializeField]
    private int currentFloor;

    [SerializeField]
    private int Nfloors;


    private List<int> floorCosts = new List<int>();

    [SerializeField]
    [Tooltip("Objeto que contiene todos los pisos(deben estar en orden)")]
    private Transform PisosContainer;

    private List<Piso> pisos = new List<Piso>();


    #region Raycast

    [Header("Raycast")]

    [SerializeField]
    private bool IsLeft;

    [SerializeField]
    private Transform RaycastOrigin;

    [SerializeField]
    private Vector2 RaycastDir;

    [SerializeField]
    private float RaycastDistance;

    [SerializeField]
    private bool PlayerDetected;

    [SerializeField]
    private LayerMask playerMask;
    #endregion



    public void SubirPiso(int cantidad = 1)
    {
        currentFloor -= cantidad;

        transition();
    }

    public void BajarPiso()
    {
        currentFloor += 1;
        transition();
    }

    private void transition()
    {
        StartCoroutine(MoveElevator());
    }

    private void detectPlayer()
    {

        RaycastHit2D hit = Physics2D.Raycast(RaycastOrigin.position, RaycastDir, RaycastDistance, playerMask);

        PlayerDetected = hit.rigidbody != null;       
    }

    private int formulaEscalado(int n)
    {
        return (n+1)*100;
    }

    

    // Start is called before the first frame update
    void Start()
    {

        RaycastDir = IsLeft ? new Vector2(-1, 0) : new Vector2(1, 0);

 
        for (int i = 0;i < Nfloors; i++)
        {
            floorCosts.Add(formulaEscalado(i));
        }


        for (int i = 0; i < Nfloors; i++)
        {
            if( PisosContainer.childCount <= i)
            {
                break;
            }

            pisos.Add(PisosContainer.GetChild(i).gameObject.GetComponent<Piso>());
        }


        transition();
    }

    // Update is called once per frame
    void Update()
    {
        detectPlayer();
    }

    IEnumerator MoveElevator()
    {
        float distance = pisos[currentFloor].GetElevatorHeightTarget() - transform.parent.position.y;

        for (int i = 0; i < 50;i++)
        {

            transform.parent.position +=  new Vector3( 0,distance / 50,0);  
         
            yield return new WaitForSecondsRealtime(0.01f);
        }

    }


    public float getFloorCost(int i)
    {
        if (i < 0 || i > Nfloors)
        {
            Debug.Log("Indice invalido al obtener el cost de un piso, indice era :" + i);
            return -1;
        }
        return floorCosts[i];
    }
}
