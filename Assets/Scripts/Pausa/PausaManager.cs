using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PausaManager : MonoBehaviour
{

    public static PausaManager instance;


    public GameObject HudInGame;
    public GameObject HudPause;

    [SerializeField] Button firstSelected;


    bool pause = false;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else 
            Destroy(gameObject);

       print(PlayerId.nextId);
    }

    private void Update() {
        print(PlayerId.canConnect);
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

        if (pause) firstSelected.Select();
    }

    public void ResetIdOnExit() {
        PlayerId pId = Object.FindObjectOfType<PlayerId>();
        if (pId != null)
            pId.ResetIds();

        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
