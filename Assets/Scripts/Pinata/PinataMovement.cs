using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinataMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float directionResetTimer = 7;
    [SerializeField] private float bounceTimer = 3;

    [SerializeField] Rigidbody2D objectRb;

    private bool bounced = false;

    Vector2 moveDirection = new Vector2(0f, 0f);


    private void Start()
    {
        moveDirection.Set(0f - transform.position.x, 0f);
    }


    void Update()
    {
        RandomDirectionChange();

        objectRb.velocity = moveDirection.normalized * speed;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("storyFloorH"))
        {
            Debug.Log("ouchH");
            Vector2 bounceDirection = new Vector2(0f, 0f);

            if (collision.gameObject.transform.position.y > transform.position.y)
                bounceDirection = Vector2.down;
            else if (collision.gameObject.transform.position.y < transform.position.y)
                bounceDirection = Vector2.up;

            moveDirection = bounceDirection;
            directionResetTimer = bounceTimer;
        }

        if (collision.gameObject.CompareTag("storyFloorV"))
        {
            Debug.Log("ouchV");
            Vector2 bounceDirection = new Vector2(0f, 0f);

            if (collision.gameObject.transform.position.x > transform.position.x)
                bounceDirection = Vector2.left;
            else if (collision.gameObject.transform.position.x < transform.position.x)
                bounceDirection = Vector2.right;

            moveDirection = bounceDirection;
            directionResetTimer = bounceTimer;
        }
    }
}
