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

    [SerializeField] UpdateHPbar hpBar;

    private PinataManager manager;

    private void Start()
    {
        currentLife = maxlife;
        manager = GetComponent<PinataManager>();
    }
    public void ReciveDamage(float damage, GameObject bulletOwner)
    {
        currentLife -= damage;
        hpBar.UpdateBar(currentLife, maxlife);

        if (currentLife <= 0)
        {
            Die(bulletOwner);
        }
    }

    private void Die(GameObject bulletOwner)
    {
        //dependiendo del tipo hacer X cosa 
        //...
        //bulletOwner.GetComponent<>


        Destroy(gameObject);
    }
}
