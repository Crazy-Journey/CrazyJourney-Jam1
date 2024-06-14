using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private Vector2 movementInput;

    [SerializeField] Transform objectTr;

    // Update is called once per frame
    void Update()
    {
        print(movementInput);
        objectTr.Translate(new Vector3(movementInput.x, movementInput.y, 0f) * speed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();
}
