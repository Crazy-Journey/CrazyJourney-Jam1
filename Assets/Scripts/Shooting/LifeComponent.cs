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

    [SerializeField] private float lifeRegen;

    [SerializeField]
    private EntityType type;

    [SerializeField] UpdateHPbar hpBar;

    private PinataManager manager;


    public void SetLifeRegen(float _lifeRegen)
    {
        lifeRegen = _lifeRegen;
    }

    public void setMaxLife(float _maxlife)
    {
        float lifeOffset = _maxlife - maxlife;
        currentLife += lifeOffset; // Esto es para que al ganar vida máxima también ganes vida actual, es decir si estás a 47/50 y ganas 3, estarás a 50/53.

        maxlife = _maxlife;

    }

    private void Start()
    {
        currentLife = maxlife;
        manager = GetComponent<PinataManager>();
    }

    private void Update()
    {
        // sistema de regeneración de vida
        if(currentLife < maxlife)
        {
            currentLife += lifeRegen * Time.deltaTime;
            if (currentLife > maxlife) currentLife = maxlife;
        }  
    }

    public void ReciveDamage(float damage, GameObject bulletOwner)
    {
        currentLife -= damage;

        if(hpBar != null)
            hpBar.UpdateBar(currentLife, maxlife);

        if (currentLife <= 0)
        {
            Die(bulletOwner);
        }
    }

    private void Die(GameObject bulletOwner)
    {
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

            // Player pierde una parte de poder
            PlayerDataManager.PlayerData _player = PlayerDataManager.THIS.GetPlayer(myId);
            _player.PowerMultiplier(0.5f);
            PlayerDataManager.THIS.SetPlayer(myId, _player);

            currentLife = maxlife;
        }

        if(type == EntityType.Enemy)
        {
            int bulletOwnerId = bulletOwner.GetComponentInChildren<PlayerId>().GetPlayerId();

            // Le damos nuestro drop al jugador que nos ha matado
            PlayerDataManager.PlayerData _player = PlayerDataManager.THIS.GetPlayer(bulletOwnerId);

            _player.ChangePower(+manager.powerDrop);
            _player.ChangeCoins(+manager.coinDrop);
            PlayerDataManager.THIS.SetPlayer(bulletOwnerId, _player);
            Destroy(gameObject);
        }


        
    }
}
