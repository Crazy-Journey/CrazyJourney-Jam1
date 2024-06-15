using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectElevator : MonoBehaviour
{

    [SerializeField]
    private GameObject playerContainer;

    [SerializeField]
    private GameObject actionMapContainer;

    private InputActionMap actionMap;

    [Header("Raycast")]

    [SerializeField]
    private Transform RaycastOrigin;

    [SerializeField]
    private float RaycastDistance;

    [SerializeField]
    private bool ElevatorDetected;

    [SerializeField]
    private LayerMask elevatorMask;

    
    private bool isInElevator = false;

    private GameObject lastElevator;

    private void Awake()
    {
        actionMap = actionMapContainer.GetComponent<PlayerInput>().currentActionMap;

    }


    public void ExitElevator()
    {
        //reactivar input
        actionMap.Enable(); 
        isInElevator = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        detectElevator();   
    }
    private void detectElevator()
    {

        RaycastHit2D hit1 = Physics2D.Raycast(RaycastOrigin.position,new Vector2(1,0), RaycastDistance, elevatorMask);
        RaycastHit2D hit2 = Physics2D.Raycast(RaycastOrigin.position,new Vector2(-1, 0), RaycastDistance, elevatorMask);

        if(hit1.rigidbody != null)
        {
            lastElevator = hit1.rigidbody.gameObject;
            ElevatorDetected = true;
        }
        else if (hit2.rigidbody != null)
        {
            lastElevator = hit2.rigidbody.gameObject;
            ElevatorDetected = true;
        }
        else
        {
            ElevatorDetected =false;    
        }
    }

    public void InteractElevator(InputAction.CallbackContext context)
    {

        if(!isInElevator && ElevatorDetected && context.started)
        {
            print("entra ascensor");
            isInElevator = true;

            //desactivar input
            actionMap.Disable();

            playerContainer.transform.position = lastElevator.transform.position;

        }

    }

}
