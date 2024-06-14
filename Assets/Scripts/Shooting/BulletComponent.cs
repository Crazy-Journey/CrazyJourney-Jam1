using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    static public float speed = 5f;

    private Rigidbody2D myRb;

    private GameObject owner;

    private float damage;




    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    public void setVelocity(Vector2 velocity)
    {
        myRb.velocity = velocity.normalized * speed;
    }

    public void setVelocity(Vector2 velocity,float customSpeed)
    {
        myRb.velocity = velocity.normalized * customSpeed;
    }


    public void setOwner(GameObject _owner)
    {
        owner = _owner; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("aaa");
        if (collision.gameObject != owner)
        {
            if(collision.gameObject.layer != LayerMask.NameToLayer("Floor") ||
                (collision.gameObject.layer == LayerMask.NameToLayer("Floor") &&
                myRb.velocity.y <=0))
            {
                Destroy(gameObject);
            }
        }
    }
  
}
