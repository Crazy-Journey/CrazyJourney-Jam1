using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PausaManager : MonoBehaviour
{

    public static PausaManager instance;


    public GameObject HudInGame;
    public GameObject HudPause;


    bool pause = false;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }

    public void swicthPause(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            swicthPause();
        }
    }

   public void swicthPause()
    {
        
        pause = !pause;
        Time.timeScale = pause ? 0 : 1;

        HudInGame.SetActive(!HudInGame.activeInHierarchy);
        HudPause.SetActive(!HudPause.activeInHierarchy);
        
    }


}
