using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10;

    [SerializeField] Rigidbody2D objectRb;

    public bool canJump;

    private void Update()
    {
        if (Physics2D.Raycast(transform.position - Vector3.up * 0.1f, Vector3.down, 0.2f))
        {
            print("a");
            canJump = true;
        }
        else
        {
            print("b");
            canJump = false;
        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        print("c");
        if (canJump)
        {
            //objectRb.velocity = new Vector2(objectRb.velocity.x, 0.0f);
            objectRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


}
