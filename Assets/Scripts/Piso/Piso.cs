using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piso : MonoBehaviour
{

    [SerializeField] private int id;

    [SerializeField] Transform elevatorTarget; 
    private float elevatorHeightTarget;

    private void Start()
    {
        elevatorHeightTarget = elevatorTarget.position.y;
    }

    public int GetId() { return id; }

    public float GetElevatorHeightTarget() {  return elevatorHeightTarget; }
}
