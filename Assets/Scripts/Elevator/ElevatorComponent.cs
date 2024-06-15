using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorComponent : MonoBehaviour
{

    [SerializeField]
    private int currentFloor;

    [SerializeField]
    private int Nfloors;

    [SerializeField]
    List<int> floorCosts = new List<int>();



    public void SubirPiso(int cantidad = 1)
    {
        currentFloor -= cantidad;

    }

    public void BajarPiso()
    {
        currentFloor += 1;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
