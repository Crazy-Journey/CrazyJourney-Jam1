using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    static public float speed = 5f;

    private Rigidbody2D myRb;


    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();

    }

    public void setVelocity(Vector2 velocity)
    {
        Debug.Log("aaaaaaaaa");
        myRb.velocity = velocity.normalized * speed;
        Debug.Log(myRb.velocity);
    }

    public void setVelocity(Vector2 velocity,float customSpeed)
    {
        myRb.velocity = velocity.normalized * customSpeed;
    }
}
