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
    [SerializeField] Animator pAnimator;


    void FixedUpdate()
    {
        objectRb.velocity = new Vector3(movementInput.x * speed, objectRb.velocity.y, 0f);

        if (objectRb.velocity != Vector2.zero)
            pAnimator.SetBool("Moving", true);

        else if (objectRb.velocity == Vector2.zero)
            pAnimator.SetBool("Moving", false);
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();
}
