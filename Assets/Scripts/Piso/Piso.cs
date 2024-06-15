using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piso : MonoBehaviour
{

    [SerializeField] private uint id;

    [SerializeField] Transform elevatorTarget; 
    private float elevatorHeightTarget;

    private void Start()
    {
        elevatorHeightTarget = elevatorTarget.position.y;
    }

    public uint GetId() { return id; }

    public float GetElevatorHeightTarget() {  return elevatorHeightTarget; }
}
