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

    public GameObject lastElevator;

    [SerializeField]
    private PlayerId playerId;

    private bool started = false;

    private void Awake()
    {
        actionMap = actionMapContainer.GetComponent<PlayerInput>().currentActionMap;

        actionMap.Disable();
    }

    private void Start()
    {
            
    }

    public void ExitElevator()
    {
        //reactivar input
        actionMap.Enable();
       
        //reactivar collider 
        playerContainer.GetComponent<CapsuleCollider2D>().isTrigger = false;

        isInElevator = false;


        var data =  PlayerDataManager.THIS.GetPlayer(playerId.GetPlayerId());
        data.SetPiso(lastElevator.GetComponentInChildren<ElevatorComponent>().getCurrentFloor());
        PlayerDataManager.THIS.SetPlayer(playerId.GetPlayerId(), data);



        print(transform.position);
        if(transform.parent.position.x <= 0)
            transform.parent.position += new Vector3(2,0,0);
        else
            transform.parent.position += new Vector3(-2.5f,0,0);
        print(transform.parent.position);


        print("AAAA");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!started)
        {
            if (InputReady.instance.PlayersReady)
            {
                actionMap.Enable();
                started = true;
            }

        }

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
            ElevatorDetected = false;    
        }
    }

    public void InteractElevator(InputAction.CallbackContext context)
    {

        if(!isInElevator && ElevatorDetected && context.started)
        {

            ElevatorComponent elevatorComponent = lastElevator.GetComponentInChildren<ElevatorComponent>();

            int myId = GetComponentInParent<PlayerId>().GetPlayerId();
            int myCoins = PlayerDataManager.THIS.GetPlayer(myId).GetCoins();
            int elevatorCost = elevatorComponent.getFloorCost(PlayerDataManager.THIS.GetPlayer(myId).GetPiso());
            if (myCoins < elevatorCost) //si no hay dinero, salimos de aquí
                return;            

            if (elevatorComponent.IsMoving()) 
                return;

            // Player pierde las coins del coste
            PlayerDataManager.PlayerData _player = PlayerDataManager.THIS.GetPlayer(myId);
            _player.ChangeCoins(-elevatorCost);
            PlayerDataManager.THIS.SetPlayer(myId, _player);


            elevatorComponent.startMoving();


            print("entra ascensor");
            isInElevator = true;

            //desactivar input
            actionMap.Disable();

            //cambiar por transicion
            playerContainer.transform.position = lastElevator.transform.position;

            playerContainer.GetComponent<CapsuleCollider2D>().isTrigger = true;


            elevatorComponent.setMovingPlayer(playerContainer.transform);
            elevatorComponent.setPlayerDetectElevator(this);
            elevatorComponent.BajarPiso();

        }

    }

}
