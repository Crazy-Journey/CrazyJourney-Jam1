using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piso : MonoBehaviour
{

    [SerializeField] private int id;

    [SerializeField] Transform elevatorTarget; 
    private float elevatorHeightTarget;

    private void Awake()
    {
        elevatorHeightTarget = elevatorTarget.position.y;
        print(elevatorHeightTarget);
    }
 
    public int GetId() { return id; }

    public float GetElevatorHeightTarget() {  return elevatorHeightTarget; }
}
