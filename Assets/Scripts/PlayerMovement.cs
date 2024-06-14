using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private Vector2 movementInput;

    [SerializeField] Rigidbody2D objectRb;

    // Update is called once per frame
    void Update()
    {
        print(movementInput);
        objectRb.velocity = new Vector3(movementInput.x * speed, objectRb.velocity.y, 0f);
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();
}
