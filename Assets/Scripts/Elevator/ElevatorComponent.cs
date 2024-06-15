using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ElevatorComponent : MonoBehaviour
{
    [Header("Pisos")]


    [SerializeField]
    private int currentFloor;

    public int getCurrentFloor() {  return currentFloor; }  

    [SerializeField]
    private int Nfloors;


    private List<int> floorCosts = new List<int>();

    [SerializeField]
    [Tooltip("Objeto que contiene todos los pisos(deben estar en orden)")]
    private Transform PisosContainer;

    private List<Piso> pisos = new List<Piso>();


    private Transform movingPlayer = null;
    private DetectElevator playerDetectElevator = null;
    private bool isMoving = false;
    public void startMoving() { isMoving = true; }
    public bool IsMoving() {  return isMoving; }    

    public void setMovingPlayer(Transform newMovingPlayer)
    {
        movingPlayer = newMovingPlayer; 
    }

    public void setPlayerDetectElevator(DetectElevator newDetect)
    {
        playerDetectElevator = newDetect;
    }

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



    private int formulaEscalado(int n)
    {
        return (n+1)*100;
    }

    

    // Start is called before the first frame update
    void Start()
    {

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

    IEnumerator MoveElevator()
    {
        float distance = pisos[currentFloor].GetElevatorHeightTarget() - transform.parent.position.y;

        for (int i = 0; i < 50;i++)
        {

            transform.parent.position +=  new Vector3( 0,distance / 50,0);  

            if(movingPlayer != null)
            {
                movingPlayer.position +=  new Vector3( 0,distance / 50,0);  

            }
         
            yield return new WaitForSecondsRealtime(0.01f);
        }


        if(playerDetectElevator != null)
        {
            playerDetectElevator.ExitElevator();
        }


        movingPlayer = null;    
        playerDetectElevator = null;
        isMoving = false;   

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
