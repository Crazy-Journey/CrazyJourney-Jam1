using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    [SerializeField]
    private float life;

    [SerializeField]
    private int type; 

    public void ReciveDamage(float damage)
    {
        life -= damage; 

        if (life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //dependiendo del tipo hacer X cosa 
        //...



        Destroy(gameObject);
    }
}
