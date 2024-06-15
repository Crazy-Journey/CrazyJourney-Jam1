using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinataMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float directionResetTimer = 7;
    [SerializeField] private float bounceTimer = 1;
    [SerializeField] private float bounceBack = 5;

    [SerializeField] Rigidbody2D objectRb;

    private bool bounced = false;

    Vector2 moveDirection = new Vector2(0f, 0f);


    private void Start()
    {
        moveDirection.Set(0f - transform.position.x, 0f);
    }


    void Update()
    {
        if (!bounced)
            RandomDirectionChange();

        objectRb.velocity = moveDirection.normalized * speed;

        if (bounced)
            HandleBounce();
    }

    void RandomDirectionChange()
    {
        directionResetTimer -= Time.deltaTime;

        if (directionResetTimer <= 0)
        {
            float anglechange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(anglechange, transform.forward);
            moveDirection =  rotation * moveDirection;

            directionResetTimer = Random.Range(2f, 4f);
        }
    }

    void HandleBounce()
    {
        objectRb.velocity = Vector2.zero;
        bounceTimer -= Time.deltaTime;

        if (bounceTimer <= 0)
        {
            bounceTimer = 1;
            bounced = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("storyFloor"))
        {
            ContactPoint2D contact = collision.contacts[0];
            Vector2 bounceDirection = new Vector2(transform.position.x - contact.point.x, transform.position.y - contact.point.y);
            objectRb.AddForce(bounceDirection * bounceBack);
            bounced = true;
            moveDirection = bounceDirection;
        }
    }
}
