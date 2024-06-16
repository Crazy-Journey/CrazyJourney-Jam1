using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] Rigidbody2D objectRb;

    [SerializeField] PlayerAnimations playerAnimations;

    [SerializeField] private float jumpForce = 10;
    
    [SerializeField] bool debugInfo;

    public bool canJump;


    private void Start()
    {
        playerAnimations = transform.parent.GetChild(GetComponentInParent<PlayerId>().GetPlayerId() == 0 ? 1 : 0).GetComponent<PlayerAnimations>();
    }

    private void FixedUpdate()
    {
        if (Physics2D.Raycast(transform.position - Vector3.up * 0.1f, Vector3.down, 0.2f, LayerMask.GetMask("Floor", "FloorTilemap")))
        {
            if (!canJump)
            {
                canJump = true;
                playerAnimations.OnGround();
            }      

            if (debugInfo)
                Debug.DrawRay(transform.position - Vector3.up * 0.01f, Vector3.down * 0.2f, Color.green);
        }
        else
        {
            canJump = false;

            if (debugInfo)
                Debug.DrawRay(transform.position - Vector3.up * 0.01f, Vector3.down * 0.2f, Color.red);    
        }


    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        
        if (ctx.started && canJump)
        {
            objectRb.velocity = new Vector2(objectRb.velocity.x, 0.0f);
            objectRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerAnimations.OnJump();

            SoundManager.instance.playSound((int)SoundManager.CLIPS.JUMP);

        }
    }


}
