using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPause : MonoBehaviour
{

    public void swicthPause(InputAction.CallbackContext context)
    {
        PausaManager.instance.swicthPause(context);

    }
}
