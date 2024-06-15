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

    public void setDamage(float newDamage)
    {
        damage = newDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != owner)
        {

            LifeComponent otherLife = collision.gameObject.GetComponent<LifeComponent>();

            if (otherLife != null)
            {
                otherLife.ReciveDamage(damage);
            }


            if(collision.gameObject.layer != LayerMask.NameToLayer("Floor") ||
                (collision.gameObject.layer == LayerMask.NameToLayer("Floor") &&
                myRb.velocity.y >=0))
            {
                Destroy(gameObject);
            }
        }
    }
  
}
