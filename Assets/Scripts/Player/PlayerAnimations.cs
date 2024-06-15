using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;

    [SerializeField] Rigidbody2D objectRb;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (objectRb.velocity != Vector2.zero) animator.SetBool("Moving", true);
        else animator.SetBool("Moving", false);
    }

    public void OnJump()
    {
        animator.SetBool("Jumping", true);
    }

    public void OnGround()
    {
        animator.SetBool("Jumping", false);
    }

    public void OnDeath()
    {
        animator.SetTrigger("Death");
    }

    public void OnHit()
    {
        animator.SetTrigger("Hit");
    }
}
