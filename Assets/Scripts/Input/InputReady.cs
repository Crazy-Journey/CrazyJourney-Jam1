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
    private void Awake()
    {
        if(instance == null)
        instance = this;  
        else
            Destroy(this);
    }


    public void PlayerConnected(PlayerInput input)
    {
        count++;    
        print(count);

        if(count == 2) 
        {
            PlayersReady = true;        
        }


    }


}
