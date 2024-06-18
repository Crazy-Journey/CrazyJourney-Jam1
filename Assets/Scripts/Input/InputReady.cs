using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReady : MonoBehaviour
{
    public bool PlayersReady = false;

    public static InputReady instance;
    int count = 0;


    public GameObject panelReady1;
    public GameObject panelReady2;


    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        if(instance == null)
        instance = this;  
        else
            Destroy(this);


        playerInputManager = GetComponent<PlayerInputManager>();

        StartCoroutine(InitialInputDisabled());
    }


    public void PlayerConnected(PlayerInput input)
    {
        PlayerId pId = Object.FindObjectOfType<PlayerId>();
        if (pId != null && !pId.CanConnect()) return;

        count++;

        if (count == 1)
        {
            panelReady1.SetActive(false);
        }

        if(count == 2) 
        {
            panelReady2.SetActive(false);
            PlayersReady = true;        
        }
    }

    IEnumerator InitialInputDisabled() {
        playerInputManager.enabled = false;
        yield return new WaitForSeconds(1f);
        playerInputManager.enabled = true;
        playerInputManager.EnableJoining();
    }

}
