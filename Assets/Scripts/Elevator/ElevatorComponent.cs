using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorComponent : MonoBehaviour
{
    [Header("Pisos")]


    [SerializeField]
    private int currentFloor;

    [SerializeField]
    private int Nfloors;

    [SerializeField]
    private List<int> floorCosts = new List<int>();

    [SerializeField]
    [Tooltip("Objeto que contiene todos los pisos(deben estar en orden)")]
    private Transform PisosContainer;

    [SerializeField]
    private List<GameObject> pisos = new List<GameObject>();


    #region Raycast

    [Header("Raycast")]

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
        for(int i = 0;i < Nfloors; i++)
        {
            floorCosts.Add(FormulaEscalado(i));
        }


        for (int i = 0; i < Nfloors; i++)
        {
            pisos.Add(PisosContainer.GetChild(0).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        detectPlayer();
    }
}
