using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public float damage;
    public float maxLife;
    public float lifeRegen;

    [SerializeField] PlayerId playerId;
    [SerializeField] LifeComponent lifeComponent;
    [SerializeField] ShootingComponent shootingComponent;

    void Start()
    {
        
    }

    // Modificamos las estadísticas en función del poder, y seteamos los componenetes que las usan
    void Update()
    {
        float power = PlayerDataManager.THIS.GetPlayer(playerId.GetPlayerId()).GetPower();

        damage = 10 + power;
        maxLife = 100 + power*10;
        lifeRegen = 1 + power / 10;

        shootingComponent.SetBulletDamage(damage);
        lifeComponent.setMaxLife(maxLife);
        lifeComponent.SetLifeRegen(lifeRegen);
    }
}
