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

    }

    private void detectPlayer()
    {

        RaycastHit2D hit = Physics2D.Raycast(RaycastOrigin.position, RaycastDir, RaycastDistance, playerMask);

        PlayerDetected = hit.rigidbody != null;       
    }

    private int FormulaEscalado(int n)
    {
        return (n+1)*100;
    }

    // Start is called before the first frame update
    void Start()
    {

        RaycastDir = IsLeft ? new Vector2(-1, 0) : new Vector2(1, 0);

 
        for (int i = 0;i < Nfloors; i++)
        {
            floorCosts.Add(FormulaEscalado(i));
        }


        for (int i = 0; i < Nfloors; i++)
        {
            if( PisosContainer.childCount <= i)
            {
                break;
            }

            pisos.Add(PisosContainer.GetChild(i).gameObject.GetComponent<Piso>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        detectPlayer();
    }
}
