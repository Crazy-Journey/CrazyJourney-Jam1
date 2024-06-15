using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private PlayerId playerId;
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
        if (type == EntityType.Player)
        {
            if (playerId.GetPlayerId() == 0)
                hpBar = GameObject.Find("P1HPBar").GetComponent<UpdateHPbar>();

            else if (playerId.GetPlayerId() == 1)
                hpBar = GameObject.Find("P2HPBar").GetComponent<UpdateHPbar>();
        }
            
    }

    private void Update()
    {
        // sistema de regeneración de vida
        if(currentLife < maxlife)
        {
            currentLife += lifeRegen * Time.deltaTime;
            if (currentLife > maxlife) currentLife = maxlife;

            if (hpBar != null)
                hpBar.UpdateBar(currentLife, maxlife);
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

            //Subimos el ascensor de pisos
            GameObject elevator = GetComponentInChildren<DetectElevator>().lastElevator;

            if (elevator != null)
                elevator.GetComponentInChildren<ElevatorComponent>().SubirPiso();

            // Subimos el player un pisito
            int myId = GetComponentInChildren<PlayerId>().GetPlayerId();
            var data = PlayerDataManager.THIS.GetPlayer(myId);
            data.SetPiso(math.clamp(PlayerDataManager.THIS.GetPlayer(playerId.GetPlayerId()).GetPiso() - 1,0,7));
            PlayerDataManager.THIS.SetPlayer(myId,data);


            // Spawneamos en el nuevo piso
            GetComponentInChildren<SpawnSystem>().SpawnOnPiso();

            // Player pierde una parte de poder
            PlayerDataManager.PlayerData _player = PlayerDataManager.THIS.GetPlayer(myId);
            _player.PowerMultiplier(0.8f);
            PlayerDataManager.THIS.SetPlayer(myId, _player);

            // Reseteamos la vida
            currentLife = maxlife;

            if (hpBar != null)
                hpBar.UpdateBar(currentLife, maxlife);
        }

        if(type == EntityType.Enemy)
        {
            int bulletOwnerId = bulletOwner.GetComponentInChildren<PlayerId>().GetPlayerId();

            // Le damos nuestro drop al jugador que nos ha matado
            PlayerDataManager.PlayerData _player = PlayerDataManager.THIS.GetPlayer(bulletOwnerId);

            _player.ChangePower(+manager.powerDrop);
            _player.ChangeCoins(+manager.coinDrop);

            // Actualizamos UI del jugador
            if (bulletOwnerId == 0)
            {
                GameObject.Find("CoinsText1").GetComponent<TMP_Text>().text = _player.GetCoins().ToString();
                GameObject.Find("PowerText1").GetComponent<TMP_Text>().text = _player.GetPower().ToString();
            }

            else if (bulletOwnerId == 1)
            {
                GameObject.Find("CoinsText2").GetComponent<TMP_Text>().text = _player.GetCoins().ToString();
                GameObject.Find("PowerText2").GetComponent<TMP_Text>().text = _player.GetPower().ToString();
            }

            PlayerDataManager.THIS.SetPlayer(bulletOwnerId, _player);
            Destroy(gameObject);
        }


        
    }
}
