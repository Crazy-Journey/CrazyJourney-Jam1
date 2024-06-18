using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField]
    private GameObject colliderContainer;


    public GameObject elevatorHint;
    public TMP_Text  elevatorText;


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
        if (currentFloor == 0) return;

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
        if (n == 0) return 10;
        if (n == 1) return 15;
        if (n == 2) return 20;
        if (n == 3) return 30;
        if (n == 4) return 50;


        return (int)(Mathf.Pow(2,n) * 10f);
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


    private void Update()
    {
        if (currentFloor < pisos.Count)
            elevatorText.text = floorCosts[currentFloor].ToString();
    }
    IEnumerator MoveElevator()
    {
        if (pisos.Count <= currentFloor) yield break;
        Piso p = pisos[currentFloor];
        if (p == null) yield break;

        float distance = p.GetElevatorHeightTarget() - transform.parent.position.y;

        colliderContainer.GetComponent<Collider2D>().isTrigger = true;

        StartCoroutine(Camera.main.GetComponent<CameraManager>().CameraShake(2, 4, 1));

        for (int i = 0; i < 50; i++) {

            transform.parent.position += new Vector3(0, distance / 50, 0);

            if (movingPlayer != null) {
                movingPlayer.position += new Vector3(0, distance / 50, 0);

            }

            yield return new WaitForSecondsRealtime(0.01f);
        }


        if (playerDetectElevator != null) {
            playerDetectElevator.ExitElevator();
        }


        movingPlayer = null;
        playerDetectElevator = null;
        isMoving = false;

        colliderContainer.GetComponent<Collider2D>().isTrigger = false;

    }
    public int getFloorCost(int i)
    {
        if (i < 0 || i > Nfloors)
        {
            Debug.Log("Indice invalido al obtener el cost de un piso, indice era :" + i);
            return -1;
        }
        return floorCosts[i];
    }
}
