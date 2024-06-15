using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    [SerializeField]
    private float currentLife;
    
    [SerializeField]
    private float maxlife;

    [SerializeField]
    private int type;

    private void Start()
    {
        currentLife = maxlife;
    }
    public void ReciveDamage(float damage)
    {
        currentLife -= damage; 

        if (currentLife <= 0)
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
