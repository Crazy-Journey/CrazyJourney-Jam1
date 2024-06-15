using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent : MonoBehaviour
{

    public enum EntityType
    {
        Player,
        Enemy
    }

    [SerializeField]
    private float currentLife;
    
    [SerializeField]
    private float maxlife;

    [SerializeField]
    private EntityType type;

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
        //hpBar.UpdateBar(currentLife, maxlife);

        if (currentLife <= 0)
        {
            Die(bulletOwner);
        }
    }

    private void Die(GameObject bulletOwner)
    {
        print("muerto");
        if(type == EntityType.Player)
        {
            // Obtenemos el piso del player que nos ha matado
            int bulletOwnerId = bulletOwner.GetComponentInChildren<PlayerId>().GetPlayerId();
            int targetPiso = PlayerDataManager.THIS.GetPlayer(bulletOwnerId).GetPiso();

            // Seteamos nuestro piso al piso del player que nos ha matado
            int myId = GetComponentInChildren<PlayerId>().GetPlayerId();
            PlayerDataManager.THIS.GetPlayer(myId).SetPiso(targetPiso);

            // Spawneamos en el nuevo piso
            GetComponentInChildren<SpawnSystem>().SpawnOnPiso();

            currentLife = maxlife;

            // TAMBIEN PERDEMOS UN POCO DE PODER // TO DO
        }

        if(type == EntityType.Enemy)
        {
            // LE DAMOS NUESTRO DROP AL JUGADOR QUE NOS HA MATADO // TO DO
            Destroy(gameObject);
        }


        
    }
}
